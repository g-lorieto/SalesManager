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
    public class ClientsController : Controller
    {
        private AbstractService<Client> _service;

        public ClientsController(AbstractService<Client> service) => _service = service;

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View((await _service.ListAsync()).Data);
        }

        // GET: Clients/Details/5
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

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,IsCantinero,Comment,Id")] Client client)
        {
            if (_service.ValidateEntity(client).Data)
            {
                var result = await _service.AddAsync(client);

                if (!result.IsSuccess)
                {

                }

                return RedirectToAction(nameof(Index));
            }

            return View(client);
        }

        // GET: Clients/Edit/5
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

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Address,IsCantinero,Comment,Id")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (_service.ValidateEntity(client).Data)
            {
                try
                {
                    var result = await _service.UpdateAsync(id, client);

                    if (!result.IsSuccess)
                    {

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    var alreadyExists = await _service.EntityExistsAsync(client.Id);
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
            return View(client);
        }

        // GET: Clients/Delete/5
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

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
