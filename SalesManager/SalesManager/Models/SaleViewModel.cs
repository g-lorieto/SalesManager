using SalesManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManager.Models
{
    public class SaleViewModel
    {        
        public Sale Sale { get; set; }                
        public List<Client> Clients { get; set; }
        public List<Product> Products { get; set; }
        
        public SaleViewModel() { }

        public SaleViewModel(List<Client> clients, List<Product> products)
        {
            Clients = clients;
            Products = products;            
        }
    }
}
