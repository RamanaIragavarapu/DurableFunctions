using CRUDDemo;
using System;

namespace CrudDemo
{
    public class ShoppingcartItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime datetime { get; set; } = DateTime.Now;
        public string ItemName { get; set; }
        public bool collected { get; set; }

        internal static void Add(ShoppingcartItem item)
        {
            throw new NotImplementedException();
        }
    }
    public class CreateshoppingcartItem
    {
        public string ItemName { get; set; }
    }
    public class UpdateshoppingcartItem
    {
        public bool collected { get; set; }
    }
}