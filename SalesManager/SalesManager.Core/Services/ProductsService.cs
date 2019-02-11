using SalesManager.Core.Interfaces;
using SalesManager.Core.Models;
using SalesManager.Core.Validators;
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
            var validator = new ProductValidator();

            var result = validator.Validate(entity);

            return result.IsValid;
        }
    }
}
