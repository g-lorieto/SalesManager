﻿using FluentValidation;
using SalesManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesManager.Core.Validators
{
    class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(sale => sale.ClientId).NotNull();

            RuleFor(sale => sale.Date).NotNull();

            RuleFor(sale => sale.Items).NotEmpty();            
        }
    }
}
