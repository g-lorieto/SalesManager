using SalesManager.Core.Interfaces;
using SalesManager.Core.Models;
using SalesManager.Core.Models.Results;
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
        public ProductsService(IRepository<Product> repository) : base(repository) { }

        public override Result<bool> ValidateEntity(Product entity)
        {
            var validator = new ProductValidator();

            var result = validator.Validate(entity);

            return new Result<bool>(result.IsValid, true);
        }
    }
}
