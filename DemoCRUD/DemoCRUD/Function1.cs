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

namespace DemoCRUD
{
    public  class GetAllInfo
    {
        private const string DataBaseName = "Student";
        private const string CollectionName = "Information";
        private const string DbUrl = "https://localhost:8081";
        private readonly CosmosClient _cosmosClient;
        private Container docContainer;
        public GetAllInfo(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            docContainer = _cosmosClient.GetContainer("Student", "Information");
        }
        [FunctionName("GetAllInfo")]
        public static IActionResult Getallemps(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Getallstudentdetails")] HttpRequest req,
            [CosmosDB(
            DataBaseName,
                CollectionName,
                Connection ="CosmosDBConnectionStr",
                SqlQuery = "SELECT id FROM 1")]
               System.Collections.Generic.IEnumerable<Student> stu,
            ILogger log)
        {
            log.LogInformation("Getting list of all employees ");
            string gmessage = "Retrieved all items successfully";
            dynamic gmydata = new ExpandoObject();
            gmydata.message = gmessage;
            gmydata.Data = stu;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(gmydata);
            return new OkObjectResult(json);
        }


    }
}
