using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace crud4
{
    public class shopping
    {
        public string id { get; set; }
        public DateTime datetime { get; set; } = DateTime.Now;
        public string ItemName { get; set; }
        public bool collected { get; set; }

        
    }
    public class CreateItem
    {
        public string ItemName { get; set; }
    }
}