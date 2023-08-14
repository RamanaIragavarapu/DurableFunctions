using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace CRUDDemo
{
    public static class ShoppingcartItems
    {
        private const string DataBaseName = "shop";
        private const string CollectionName = "shoppingcartItem";
        private static readonly CosmosClient _cosmosClient;
        private static Container documentContainer;
        static List<ShoppingcartItem> shoppingcartItems = new();
        [FunctionName("GetShoppingcartItem")]
        public static IActionResult GetShoppingcartItem(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GettingAllTheShoppingcartItem")] HttpRequest req,
            [CosmosDB(
            DataBaseName,
            CollectionName,
            Connection="cosmosDBconnectionstring",
            SqlQuery="SELECT * FROM c")]IEnumerable<ShoppingcartItem> sci,
            ILogger log)
        {
            log.LogInformation("Getting all the ShoppingcartItems.");
            return new OkObjectResult(shoppingcartItems);
        }
    }
}