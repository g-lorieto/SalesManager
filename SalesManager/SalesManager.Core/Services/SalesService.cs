using Microsoft.EntityFrameworkCore;
using SalesManager.Core.Interfaces;
using SalesManager.Core.Models;
using SalesManager.Core.Models.Results;
using SalesManager.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalesManager.Core.Services
{
    public class SalesService : AbstractService<Sale>
    {
        private ProductsService _productsService;
        private ClientsService _clientsService;
        public SalesService(IRepository<Sale> repository, ProductsService productsService, ClientsService clientsService) : base(repository)
        {
            _productsService = productsService;
            _clientsService = clientsService;
        }

        public async Task<Result<int>> UpdateAsync(int id, Sale sale, params Expression<Func<Sale, object>>[] navigations)
        {
            try
            {
                var isCantinero = await _clientsService.Query().Select(x => x.IsCantinero).SingleAsync();

                foreach (var item in sale.Items)
                {
                    item.Price = await GetItemSalePrice(isCantinero, item.ProductId);
                }

                return await base.UpdateAsync(id, sale, navigations);
            }
            catch (Exception ex)
            {
                return Failure(-1, ex.Message);
            }
        }

        public async Task<decimal> GetItemSalePrice(bool isCantinero, int productId)
        {
            return await _productsService.Query().Select(x => isCantinero ? x.FriendsPricePerKilo : x.PricePerKilo).SingleAsync();

        }

        public override Result<bool> ValidateEntity(Sale entity)
        {
            var validator = new SaleValidator();

            var result = validator.Validate(entity);

            return new Result<bool>(result.IsValid, true);
        }
    }
}
