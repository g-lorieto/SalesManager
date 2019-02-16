using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesManager.Core.Models;
using SalesManager.Core.Services;
using SalesManager.DataAccess;
using SalesManager.Models;

namespace SalesManager.Controllers
{
    public class SalesController : Controller
    {
        private AbstractService<Sale> _service;

        private AbstractService<Product> _productService;

        private AbstractService<Client> _clientService;

        public SalesController(AbstractService<Sale> service, AbstractService<Product> productService, AbstractService<Client> clientService)
        {
            _service = service;
            _productService = productService;
            _clientService = clientService;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            return View((await _service.ListAsync()).Data);
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _service.GetByIdAsync(id.Value);

            if (result.Data == null)
            {
                return NotFound();
            }

            return View(result.Data);
        }

        // GET: Sales/Create
        public async Task<IActionResult> Create()
        {
            var clients = (await _clientService.ListAsync()).Data;

            var products = (await _productService.ListAsync()).Data;

            var saleViewModel = new SaleViewModel(clients, products);

            return View(saleViewModel);
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleViewModel saleViewModel)
        {
            var sale = saleViewModel.Sale;

            if (_service.ValidateEntity(sale).Data)
            {
                var result = await _service.AddAsync(sale);

                if (!result.IsSuccess)
                {

                }

                return RedirectToAction(nameof(Index));
            }

            return View(saleViewModel);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var clients = (await _clientService.ListAsync()).Data;

            var products = (await _productService.ListAsync()).Data;

            var saleViewModel = new SaleViewModel(clients, products);

            if (id == null)
            {
                return NotFound();
            }

            var sale = await _service.GetByIdAsync(id.Value);

            if (sale == null)
            {
                return NotFound();
            }

            saleViewModel.Sale = sale.Data;

            return View(saleViewModel);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SaleViewModel saleViewModel)
        {
            var sale = saleViewModel.Sale;

            if (id != sale.Id)
            {
                return NotFound();
            }

            if (_service.ValidateEntity(sale).Data)
            {
                try
                {
                    var result = await _service.UpdateAsync(id, sale);

                    if (!result.IsSuccess)
                    {

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    var alreadyExists = await _service.EntityExistsAsync(sale.Id);

                    if (!alreadyExists.Data)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(saleViewModel);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _service.GetByIdAsync(id.Value);

            if (result.Data == null)
            {
                return NotFound();
            }

            return View(result.Data);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
