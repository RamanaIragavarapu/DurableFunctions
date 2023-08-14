using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System;

namespace DemoGet.DemoGet
{
    public class employee
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Qualification { get; set; }
        public string Branch { get; set; }
        public string phno { get; set; }

    }
    public class Createemployee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Qualification { get; set; }
        public string Branch { get; set; }
        public string phno { get; set; }
    }
    public class Updateemployee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Qualification { get; set; }
        public string Branch { get; set; }
        public string phno { get; set; }
    }
}