using System;
using System.Collections.Generic;
using System.Text;

namespace SalesManager.Core.Models
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsCantinero { get; set; }
        public string Comment { get; set; }

        public IList<Sale> Sales { get; set; }
    }
}
