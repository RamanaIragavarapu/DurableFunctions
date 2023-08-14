using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using System.Configuration;
using Raven.Database.Plugins.Builtins.Monitoring.Snmp.Objects.Database.Statistics;

namespace crud4
{
    public class shopitems
    {
        private const string DataBaseName = "shop";
        private const string CollectionName = "shoppingcartItem";
        private readonly CosmosClient _cosmosClient;
        private Container documentContainer;
        public shopitems(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            documentContainer = _cosmosClient.GetContainer("shop", "shoppingcartItem");
        }
        [FunctionName("GetItems")]
        public static IActionResult GetItems(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetAllItems")] HttpRequest req,
            [CosmosDB(
            DataBaseName,
            CollectionName,
            Connection="CosmosDBConnection",
            SqlQuery="SELECT * FROM c")]IEnumerable<shopping> sci,
            ILogger log)
        {
            log.LogInformation("Getting all items");
            return new OkObjectResult(sci);
        }

       [FunctionName("GetItemById")]
        public static IActionResult GetItemById(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetItemById/{id}")] HttpRequest req,
           [CosmosDB(
            DataBaseName,
            CollectionName,
            Connection="CosmosDBConnection",
            Id ="id")] shopping shopin ,
           string id,
           ILogger log)
        {
            log.LogInformation("Getting Item by id");
            if (shopin==null)
            {
                log.LogInformation($"the item with {id} not found");
                return new NotFoundResult();
            }
            return new OkObjectResult(shopin);
        }

        [FunctionName("CreateItem")]
        public async Task<IActionResult> CreateItem(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = "CreateItem")] HttpRequest req,
           [CosmosDB(
            DataBaseName,
            CollectionName,
            Connection="CosmosDBConnection"
            )] IAsyncCollector<object> shopin,
           ILogger log)
        {
            log.LogInformation("CreatingItem");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<CreateItem>(requestBody);

            var shopping = new shopping()
            {
                ItemName = data.ItemName
            };
            await shopin.AddAsync(new{id = shopping.id, shopping.datetime, shopping.ItemName, shopping.collected});
            return new OkObjectResult(shopping);
        }
    }
}

