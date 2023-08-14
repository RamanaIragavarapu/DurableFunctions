using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using System.Dynamic;
using System.ComponentModel.Design;
using FunctionApp2;

namespace DemoGet
{
    public class EDetails
    {
        private const string DataBaseName = "Employee";
        private const string container = "EmpDetails";
        private readonly CosmosClient _cosmos;
        private Microsoft.Azure.Cosmos.Container myContainer;

        public EDetails(CosmosClient cosmosClient)
        {
            _cosmos = cosmosClient;
            myContainer = _cosmos.GetContainer("Employee", "EmpDetails");
        }
        [FunctionName("GetallInfo")]
        public static IActionResult GetAllDetails(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getAllDetailsAboutEmployees")] HttpRequest req,
            [CosmosDB(
            DataBaseName,
            container,
            Connection ="CosmosDBConnectionString",
            SqlQuery="SELECT * FROM c")]
            System.Collections.Generic.IEnumerable<employee> mov, ILogger log)
        {
            log.LogInformation("Getting the details of the employees");
            string mmessage = "Retrieving all the employee details";
            dynamic mymvdata = new ExpandoObject();
            mymvdata.message = mmessage;
            mymvdata.Data = mov;
            string json = JsonConvert.SerializeObject(mymvdata);
            return new OkObjectResult(json);
        }
    }
}