using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WineTime.Data;
using WineTime.Models;

namespace WineTime.Controllers
{
    public class WineProductsAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WineProductsAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WineProductsAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.WineProducts.ToListAsync());
        }

        // GET: WineProductsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wineProducts = await _context.WineProducts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (wineProducts == null)
            {
                return NotFound();
            }

            return View(wineProducts);
        }

        // GET: WineProductsAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WineProductsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Price,ImagePath")] WineProducts wineProducts)
        {
            if (ModelState.IsValid)
            {
                wineProducts.DateCreated = DateTime.Now;
                wineProducts.DateLastModified = DateTime.Now;
                _context.Add(wineProducts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wineProducts);
        }

        // GET: WineProductsAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wineProducts = await _context.WineProducts.FindAsync(id);
            if (wineProducts == null)
            {
                return NotFound();
            }
            return View(wineProducts);
        }

        // POST: WineProductsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Price,ImagePath,Schedule,DateCreated,DateLastModified")] WineProducts wineProducts)
        {
            if (id != wineProducts.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    wineProducts.DateLastModified = DateTime.Now;
                    _context.Update(wineProducts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WineProductsExists(wineProducts.ID))
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
            return View(wineProducts);
        }

        // GET: WineProductsAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wineProducts = await _context.WineProducts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (wineProducts == null)
            {
                return NotFound();
            }

            return View(wineProducts);
        }

        // POST: WineProductsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wineProducts = await _context.WineProducts.FindAsync(id);
            _context.WineProducts.Remove(wineProducts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WineProductsExists(int id)
        {
            return _context.WineProducts.Any(e => e.ID == id);
        }
    }
}
