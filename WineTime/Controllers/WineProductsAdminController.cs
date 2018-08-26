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
    // this allows only those that have the role of an admin to access this page
    //[Microsoft.AspNetCore.Authorization.Authorize(Roles = "Administrator")]
    public class WineProductsAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WineProductsAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WineProductsAdmin
        // declarative security: Do either this or the imperative security; does the same thing;
        //'authorize' by default takes you to the login page; you can edit it in the startup.cs to change the default behavior
        // This allows any user that is logged in to access the page. Instead, a declarative security was put at the top 
        //              right under namespace to only let those with a role of an admin access the page
        // [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.WineProduct.ToListAsync());

            // imperative security: if/else statement where do one thing if they're logged in and another if they're not
            // pick either imperative or declarative
            //if(User.Identity.IsAuthenticated)
            //{ 
            //    return View(await _context.WineProduct.ToListAsync());
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Account");
            //}
        }

        // GET: WineProductsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wineProducts = await _context.WineProduct
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

            var wineProducts = await _context.WineProduct.SingleOrDefaultAsync(m => m.ID == id);
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

            var wineProducts = await _context.WineProduct
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
            var wineProducts = await _context.WineProduct.SingleOrDefaultAsync(m => m.ID == id);
            _context.WineProduct.Remove(wineProducts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WineProductsExists(int id)
        {
            return _context.WineProduct.Any(e => e.ID == id);
        }
    }
}
