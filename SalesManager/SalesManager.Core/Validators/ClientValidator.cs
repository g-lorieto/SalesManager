using FluentValidation;
using SalesManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesManager.Core.Validators
{
    class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(client => client.Name).NotEmpty();

            RuleFor(client => client.IsCantinero).NotNull();
        }
    }
}
