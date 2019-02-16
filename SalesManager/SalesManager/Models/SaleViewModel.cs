using SalesManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManager.Models
{
    public class SaleViewModel
    {
        public List<Client> Clients { get; set; }
        public List<Product> Products { get; set; }
        public Sale Sale { get; set; }
        public Item Item { get; set; }
        public int ClientId { get; set; }


        public SaleViewModel(List<Client> clients, List<Product> products)
        {
            Clients = clients;
            Products = products;
            Item = new Item();
        }
    }
}
