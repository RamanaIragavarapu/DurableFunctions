using System;
namespace GetbyId
{
    public class shoppingcartItems
    {
        public string Id { get; set; }
        public DateTime dt { get; set; } = DateTime.Now;
        public string ItemName { get; set; }
        public bool collected { get; set; }
    }
}