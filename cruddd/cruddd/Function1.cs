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
using DemoGet.DemoGet;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Microsoft.Azure.Documents.Client;
using System.Data;
using System.Xml.Linq;

namespace DemoGet
{
    public class EDetails
    {
        private const string DBName = "Employee";
        private const string CollectionName = "EmpDetails";
        private readonly CosmosClient _cosmosClient;
        private Microsoft.Azure.Cosmos.Container documentContainer;

        public EDetails(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            documentContainer = _cosmosClient.GetContainer("Employee", "EmpDetails");
        }

        [FunctionName("GetallInfo")]
        public static IActionResult GetAllDetails(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getAllDetailsAboutEmployee")] HttpRequest req,
            [CosmosDB(
            DBName,
            CollectionName,
            Connection ="CosmosDBConnectionString",
            SqlQuery="SELECT * FROM c")]
            System.Collections.Generic.IEnumerable<employee> mov, ILogger log)
        {
            log.LogInformation("Getting the details of the employee.");
            //string mmessage = "Retrieving all the employee details";
            //dynamic mymvdata = new ExpandoObject();
            //mymvdata.message = mmessage;
            //mymvdata.Data = mov;
            //string json = JsonConvert.SerializeObject(mymvdata);
            return new OkObjectResult(mov);
        }
        [FunctionName("GetInfobyId")]
        public static IActionResult GetInfobyId(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getDetailsAboutEmployeebyId/{id}")] HttpRequest req,
           [CosmosDB(
            DBName,
            CollectionName,
            Connection ="CosmosDBConnectionString",
            SqlQuery="SELECT * FROM c")] IEnumerable<employee> mov,
            string id, ILogger log)
        {
            log.LogInformation("Getting the details of the employee.");
            var item = mov.Where(p => p.Id == id).FirstOrDefault();
            if (item == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(item);
        }
        [FunctionName("CreateInfo")]
        public static async Task<IActionResult> CreateInfo(
         [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateDetailsAboutEmployee")] HttpRequest req,
                [CosmosDB(
            DBName,
            CollectionName,
            Connection ="CosmosDBConnectionString")] IAsyncCollector<object> cmd, ILogger log)
        {
            log.LogInformation("Creating the details of the employee.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<Createemployee>(requestBody);

            var item = new employee()
            {
                Id = input.Id,
                Name = input.Name,
                Role = input.Role,
                Qualification = input.Qualification,
                Branch = input.Branch,
                phno = input.phno
            };
            await cmd.AddAsync(new { id = item.Id, item.Name, item.Role, item.Qualification, item.Branch, item.phno });
            return new OkObjectResult(item);
        }
        [FunctionName("UpdateStudent")]
        public static async Task<IActionResult> UpdateStudent(
            [HttpTrigger(AuthorizationLevel.Anonymous,"put",Route ="UpdtaingStudent")]HttpRequest req
            )
        {

        }

        /* 
     [FunctionName("UpdateInfo")]
     public static async Task<IActionResult> UpdateInfo(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateEmployeeDetailsById/{id}")] HttpRequest req,
        [CosmosDB(
      DBName,
     CollectionName,
      Connection ="CosmosDBConnectionString")]
      DocumentClient client, string id,string Qualification, ILogger log)
     {

              log.LogInformation("Updating the details of the employee.");
              Uri collectionUri = UriFactory.CreateDocumentCollectionUri("tododb", "tasks");
              var document = client.CreateDocumentQuery(collectionUri).Where(t => t.Id == id)
                              .AsEnumerable().FirstOrDefault();
              if (document == null)
              {
                  return new NotFoundResult();
              }

              */        //    document.SetPropertyValue("Qualification", updated.Qualification);
                        //    if (!string.IsNullOrEmpty(updated.Branch))
                        //    {
                        //        document.SetPropertyValue("TaskDescription", updated.Branch);
                        //    }

        //    await client.ReplaceDocumentAsync(document);
        //    employee todo2 = (dynamic)document;

        //    return new OkObjectResult(todo2);
        //}

        //    //var item = mov.Where(p => p.Id == id).FirstOrDefault();
        //    //if (item == null)
        //    //{
        //    //    return new NotFoundResult();
        //    //}
        //    string requestData = await new StreamReader(req.Body).ReadToEndAsync();
        //    var updated = JsonConvert.DeserializeObject<Updateemployee>(requestData);
        //   Uri Collectionuri = UriFactory.CreateDocumentCollectionUri("Employee", "EmpDetails");
        //   var document = Client.CreateDocumentQuery(Collectionuri).Where(q => q.Id == id).AsEnumerable().FirstOrDefault();
        //   if(document==null)
        //   {
        //       return new NotFoundResult();
        //   }
        //  var item = new employee()
        //  {
        //      Id = updated.Id,
        //      Name = updated.Name,
        //      Role = updated.Role,
        //      Qualification = updated.Qualification,
        //      Branch = updated.Branch,
        //      phno = updated.phno
        //  };
        //  //document.SetPropertyValue("Qualification", updated.Qualification);
        //  //if(!string.IsNullOrEmpty(updated.Branch))
        //  //{
        //  //   ;
        //  //}
        //  //document.SetPropertyValue("Id", updated.Id);
        //  //document.SetPropertyValue("Name", updated.Name);
        //  //document.SetPropertyValue("Role", updated.Role);
        //  //document.SetPropertyValue("Qualification", updated.Id);
        //  //document.SetPropertyValue("Branch", updated.Branch);
        //  //document.SetPropertyValue("phno", updated.phno);
        //  await Client.ReplaceDocumentAsync(i);
        //  //employee item =(dynamic) document;
        //  //item.Id = input.Id;
        //  //item.Name = input.Name;
        //  //item.Role = input.Role;
        //  //item.Qualification = input.Qualification;
        //  //item.Branch = input.Branch;
        //  //item.phno = input.phno;

        //  return new OkObjectResult(document);
        //}
        //[FunctionName("DeleteInfo")]
        //public static async Task<IActionResult> DeleteInfo(
        //     [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteEmployeeDetailsById/{id}/{Qualification}")] HttpRequest req,
        //     [CosmosDB(
        //        DBName,
        //       CollectionName,
        //        Connection ="CosmosDBConnectionString")]
        //     DocumentClient client, string id, ILogger log,string Qualification)
        //  {
        //    log.LogInformation($"Deleting Shopping Cart Item with ID: {id}");


        //    await doc.DeleteItemAsync<employee>(id, new Microsoft.Azure.Cosmos.PartitionKey(Qualification));
        //    string responseMessage = "Deleted sucessfully";
        //    return new OkObjectResult(responseMessage);

        //log.LogInformation("Deleting the details of the employee.");
        //Uri collectionUri = UriFactory.CreateDocumentCollectionUri("Employee", "EmpDetails");
        //var document = client.CreateDocumentQuery(collectionUri).Where(t => t.Id == id)
        //        .AsEnumerable().FirstOrDefault();
        //if (document == null)
        //{
        //    return new NotFoundResult();
        //}
        //await client.DeleteDocumentAsync(document.SelfLink);
        //return new OkResult();
        //var item = client.Where(p => p.Id == id).FirstOrDefault();
        //Uri collectionUri = UriFactory.CreateDocumentCollectionUri("Employee", "EmpDetails");
        //var document = client.CreateDocumentQuery(collectionUri).Where(t => t.Id == id)
        //        .AsEnumerable().FirstOrDefault();
        //if (document == null)
        //{
        //    return new NotFoundResult();
        //}
        //await documentContainer.DeleteItemAsync<employee>(id, new Microsoft.Azure.Cosmos.PartitionKey(Qualification));
        //return new OkResult();

    }
}


