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
using CRUDdemo;

namespace DemoGet
{
    public class MovieDetails
    {
        private const string DataBaseName = "Movie";
        private const string container = "Information";
        private readonly CosmosClient _cosmosClient;
        private Microsoft.Azure.Cosmos.Container documentContainer;

        public MovieDetails(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            documentContainer = _cosmosClient.GetContainer("Movie", "Information");
        }
        [FunctionName("GetallInfo")]
        public static IActionResult GetAllDetails(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getAllDetailsAboutMovie")] HttpRequest req,
            [CosmosDB(
            DataBaseName,
            container,
            Connection ="CosmosDBConnectionString",
            SqlQuery="SELECT * FROM c")]
            System.Collections.Generic.IEnumerable<Movie> mov, ILogger log)
        {
            log.LogInformation("Getting the details of the movie.");
            string message = "Retrieving all the movie details";
            dynamic mymvdata = new ExpandoObject();
            mymvdata.message = message;
            mymvdata.Data = mov;
            string json = JsonConvert.SerializeObject(mymvdata);
            return new OkObjectResult(json);
        }
    }
}