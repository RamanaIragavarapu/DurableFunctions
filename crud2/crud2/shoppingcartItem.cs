using System;

namespace CRUDDemo
{
    public class ShoppingcartItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime datetime { get; set; } = DateTime.Now;
        public string ItemName { get; set; }
        public bool collected { get; set; }

    }
}