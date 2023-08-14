using EmployeeDataBase;
using FunctionApp2;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: FunctionsStartup(typeof(Startup))]

namespace EmployeeDataBase
{ 
    public class Startup : FunctionsStartup
    {
        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("AppSettings.json",true)
            .AddEnvironmentVariables()
            .Build();
        public override void Configure(IFunctionsHostBuilder builder)
        {

            builder.Services.AddSingleton(s =>
            {
                var connectionString = Configuration["CosmosDBConnectionString"];
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException(
                        "please pass an valid cosmosDB connection in appsettings.json file");
                }
                return new CosmosClientBuilder(connectionString)
                .Build();
            });
        }
    }
}
