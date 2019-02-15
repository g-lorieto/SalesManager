using SalesManager.Core.Interfaces;
using SalesManager.Core.Models;
using SalesManager.Core.Models.Results;
using SalesManager.Core.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesManager.Core.Services
{
    public class ClientsService : AbstractService<Client>
    {
        public ClientsService(IRepository repository) : base(repository) { }

        public override Result<bool> ValidateEntity(Client entity)
        {
            var validator = new ClientValidator();

            var result = validator.Validate(entity);

            return new Result<bool>(result.IsValid, true);
        }
    }
}
