using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace CRUDDemo
{
    public class ShoppingcartItems
    {
        private const string DataBaseName = "shop";
        private const string CollectionName = "shoppingcartItem";
        private readonly CosmosClient _cosmosClient;
        private Container documentContainer;
        public ShoppingcartItems(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            documentContainer = _cosmosClient.GetContainer("shop" ,"shoppingcartItem");
        }
        static List<shoppingcartItem> shoppingcartItems = new();
        [FunctionName("GetShoppingcartItem")]
        public static async Task<IActionResult> GetShoppingcartItem(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GettingAllTheShoppingcartItem")] HttpRequest req,
            [CosmosDB(
            DataBaseName,
            CollectionName,
            Connection="CosmosDBConnection",
        SqlQuery="SELECT * FROM c"
            )] IEnumerable<shoppingcartItem> sci,
            ILogger log)
        {
            log.LogInformation("Getting all the ShoppingcartItems.");
            return new OkObjectResult(sci);
        }
        [FunctionName("GetShoppingcartItemById")]
        public static async Task<IActionResult> GetShoppingcartItembyId(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GettingTheShoppingcartItem/{Id}")] HttpRequest req,
            [CosmosDB(
            DataBaseName,
            CollectionName,
            Connection="CosmosDBConnection",
            Id ="{id}")] shoppingcartItem shp,
            ILogger log, string Id)
        {
            log.LogInformation("Getting the ShoppingcartItem By Id.");
            var item = shp;
            if (item == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(item);
        }
       /* [FunctionName("CreateShoppingcartItem")]
        public static async Task<IActionResult> CreateShoppingcartItem(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "CreatingTheShoppingcartItem")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Creating all the ShoppingcartItems.");
            string requestData = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<createshoppingcartItem>(requestData);
            var item = new shoppingcartItem
            {
                ItemName = data.ItemName
            };
            shoppingcartItems.Add(item);
            return new OkObjectResult(item);
        }
        [FunctionName("Function1")]
        public static async Task<IActionResult> UpdateShoppingcartItem(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "UpdatingTheShoppingcartItem/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            log.LogInformation("Updating the ShoppingcartItem By Id.");
            var shoppingcartItem = shoppingcartItems.FirstOrDefault(q => q.Id == id);
            if (shoppingcartItem == null)
            {
                return new NotFoundResult();
            }
            string RequestData = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<updateshoppingcartItem>(RequestData);
            shoppingcartItem.collected = data.collected;
            return new OkObjectResult(shoppingcartItem);
        }
        [FunctionName("DeleteShoppingcartItem")]
        public static async Task<IActionResult> DeleteShoppingcartItem(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeletingTheShoppingcartItem/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            log.LogInformation("Deleting all the ShoppingcartItem By Id.");
            var shoppingcartItem = shoppingcartItems.FirstOrDefault(q => q.Id == id);
            if (shoppingcartItem == null)
            {
                return new NotFoundResult();
            }
            shoppingcartItems.Remove(shoppingcartItem);
            return new OkResult();
        }*/
    }
}