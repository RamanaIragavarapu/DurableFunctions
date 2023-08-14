using System;

namespace CRUDDemo
{
    public class shoppingcartItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime datetime { get; set; } = DateTime.Now;
        public object ItemName { get; set; }
        public bool collected { get; set; }
    }
    public class createshoppingcartItem
    {
        public object ItemName { get; set; }
    }
    public class updateshoppingcartItem
    {
        public bool collected { get; set; }
    }
}