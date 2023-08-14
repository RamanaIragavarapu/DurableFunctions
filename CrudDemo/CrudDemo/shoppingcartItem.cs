using System;

namespace CrudDemo
{
    public class shoppingcartItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime datetime { get; set; } = DateTime.Now;
        public string ItemName { get; set; }
        public bool collected { get; set; }

        internal static void Add(shoppingcartItem item)
        {
            throw new NotImplementedException();
        }
    }
    public class createshoppingcartItem
    {
        public string ItemName { get; set; }
    }
    public class updateshoppingcartItem
    {
        public bool collected { get; set; }
    }
}