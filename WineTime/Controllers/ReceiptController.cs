using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WineTime.Data;

namespace WineTime.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceiptController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wineOrder = await _context.WineOrders.Include(x => x.WineOrderProducts)
                .SingleOrDefaultAsync(m => m.ID == id);

            if (wineOrder == null)
            {
                return NotFound();
            }

            return View(wineOrder);
        }
    }
}