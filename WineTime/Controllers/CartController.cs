using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineTime.Models;
using WineTime.Data;
using Microsoft.EntityFrameworkCore;

namespace WineTime.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            WineCart model = null;
            if (Request.Cookies.ContainsKey("cart_id"))
            {
                int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                model = _context.WineCarts.Include(x => x.WineCartProducts).ThenInclude(x => x.WineProducts).FirstOrDefault(x => x.ID == existingCartID);
            }
            else
            {
                model = new WineCart();
            }
            return View(model);
        }

        public IActionResult Remove(int id)
        {
            // TODO: Look through the cart items and remove the prodcut with that ID
            return RedirectToAction("Index");
        }
    }
}
