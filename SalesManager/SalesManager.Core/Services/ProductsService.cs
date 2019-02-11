using SalesManager.Core.Interfaces;
using SalesManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManager.Core.Services
{
    public class ProductsService : AbstractService<Product>
    {
        public ProductsService(IRepository repository) : base(repository) { }

        public override bool ValidateEntity(Product entity)
        {
            return true;
        }
    }
}
