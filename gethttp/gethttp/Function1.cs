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
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using CrudDemo;

namespace CrudDemo
{
    public class StudentDetails
    {
        private const string DataBaseName="Student";
        private const string ContainerName="personalData";
        private readonly CosmosClient _cosmosClient;
        private static Container dataContainer;
        public StudentDetails(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            dataContainer = _cosmosClient.GetContainer("Student", "personalData");
        }
        [FunctionName("GetDetails")]
        public static async Task<IActionResult> Getall(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
            DataBaseName,
            ContainerName,
            Connection="CosmosDBConnectionString",
            SqlQuery= "SELECT * FROM c")]IEnumerable<Student> stu,
            ILogger log)
        {
            log.LogInformation("GETAll operation has been started");
            string message = "Retrieving all the student details";
            dynamic mydata = new ExpandoObject();
            mydata.message = message;
            mydata.data = stu;
            string json = JsonConvert.SerializeObject(mydata);

            return new OkObjectResult(json);
        }
    }
}
