using System;
using System.Collections.Generic;

namespace SalesManager.Core.Models
{
    public class Sale : BaseEntity
    {
        public DateTime Date{ get; set; }
        public DateTime DeliveryDay { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public bool IsPayed { get; set; }
        public bool IsDelivered { get; set; }
        public string Comment { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}