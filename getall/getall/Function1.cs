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
using GetbyId;
using System.Collections.Generic;
using System.Collections;
using Raven.Database.Plugins.Builtins.Monitoring.Snmp.Objects.Database.Statistics;
using Microsoft.Azure.Documents.Client;
using GetbyId;

namespace DemoGet
{
    public class shoppingCart
    {
        private const string DataBaseName = "shop";
        private const string CollectionName = "shoppingcartItem";
        private readonly CosmosClient _cosmos;
        private Container documentContainer;

        public shoppingCart(CosmosClient cosmosClient)
        {
            _cosmos = cosmosClient;
            documentContainer = _cosmos.GetContainer("shop", "shoppingcartItem");
        }
        [FunctionName("GetAllItems")]
        public static IActionResult GetAllItems(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllItems")] HttpRequest req,
            [CosmosDB(
            DataBaseName,
            CollectionName,
            Connection ="CosmosDBConnectionString",
            SqlQuery ="SELECT * FROM c")]
            IEnumerable<shoppingcartItems> sci, ILogger log)
        {
            log.LogInformation("Getting the details of the movie.");
            string mmessage = "Retrieving all the movie details";
            dynamic mymvdata = new ExpandoObject();
            mymvdata.message = mmessage;
            mymvdata.Data = sci;
            string json = JsonConvert.SerializeObject(mymvdata);
            return new OkObjectResult(json);
        }

        [FunctionName("GetById")]
        public static IActionResult GetById(
                    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getItemById/{id}")] HttpRequest req,
                    [CosmosDB(
            DataBaseName,
            CollectionName,
            Connection ="CosmosDBConnectionString",
            Id ="{id}")]
                    shoppingcartItems shoppingcart, ILogger log,string id)
        {
            log.LogInformation("Getting items by id.");
            var item = shoppingcart;
            if (item != null)
            {
                return new OkObjectResult(item);
            }

            return new NotFoundResult();
        }
    }
}