using SalesManager.Core.Interfaces;
using SalesManager.Core.Models;
using SalesManager.Core.Models.Results;
using SalesManager.Core.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesManager.Core.Services
{
    public class SalesService : AbstractService<Sale>
    {
        public SalesService(IRepository repository) : base(repository) { }

        public override Result<bool> ValidateEntity(Sale entity)
        {
            var validator = new SaleValidator();

            var result = validator.Validate(entity);

            return new Result<bool>(result.IsValid, true);
        }
    }
}
