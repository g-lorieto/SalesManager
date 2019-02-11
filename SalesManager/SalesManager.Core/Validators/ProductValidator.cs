using FluentValidation;
using SalesManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesManager.Core.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Name).NotEmpty();

            RuleFor(product => product.CostPerKilo).NotNull().GreaterThan(0);

            RuleFor(product => product.FriendsPricePerKilo).NotNull().GreaterThan(0);

            RuleFor(product => product.Over10KilosPricePerKilo).NotNull().GreaterThan(0);

            RuleFor(product => product.PricePerKilo).NotNull().GreaterThan(0);
        }
    }
}
