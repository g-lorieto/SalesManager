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

namespace SalesManager.Controllers
{
    public class SalesController : Controller
    {
        private AbstractService<Sale> _service;

        public SalesController(AbstractService<Sale> service) => _service = service;

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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,DeliveryDay,IsPayed,IsDelivered,Comment,Id")] Sale sale)
        {
            if (_service.ValidateEntity(sale).Data)
            {
                var result = await _service.AddAsync(sale);

                if (!result.IsSuccess)
                {

                }

                return RedirectToAction(nameof(Index));
            }

            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _service.GetByIdAsync(id.Value);
            if (result == null)
            {
                return NotFound();
            }

            return View(result.Data);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Date,DeliveryDay,IsPayed,IsDelivered,Comment,Id")] Sale sale)
        {
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
            return View(sale);
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
