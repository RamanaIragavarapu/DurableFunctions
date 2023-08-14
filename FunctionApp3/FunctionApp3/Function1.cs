using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp3
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

          
            var x= await context.CallActivityAsync<string>("F1", null);
            var y = await context.CallActivityAsync<string>("F2", x);
            var z = await context.CallActivityAsync<string>("F3", y);
            var result = await context.CallActivityAsync<string>("F4", z);
            outputs.Add(result);
            return outputs;
        }

        [FunctionName("F1")]
        public static string F1([ActivityTrigger] string name)
        {
            return "F1 executed";
        }
        [FunctionName("F2")]
        public static string F2([ActivityTrigger] string name)
        {
            return name+Environment.NewLine+"F2 Executed"+Environment.NewLine;
        }
        [FunctionName("F3")]
        public static string F3([ActivityTrigger] string name)
        {
            return name + Environment.NewLine + "F3 Executed" + Environment.NewLine;
        }
        [FunctionName("F4")]
        public static string F4([ActivityTrigger] string name)
        {
            return name + Environment.NewLine + "F4 Executed" + Environment.NewLine;
        }


        [FunctionName("Function1_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("Function1", null);

            log.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}