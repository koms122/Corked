using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineTime.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WineTime.Data;

namespace WineTime.Controllers
{
    public class CheckoutController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            CheckoutModel model = new CheckoutModel();
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = _userManager.GetUserAsync(User).Result;
                model.Email = currentUser.Email;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CheckoutModel model)
        {
            if (ModelState.IsValid)
            {
                WineOrder order = new WineOrder
                {
                    City = model.City,
                    State = model.State,
                    Email = model.Email,
                    StreetAddress = model.StreetAddress,
                    AptSuite = model.AptSuite,
                    ZipCode = model.ZipCode,
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now
                };

                WineCart cart = null;
                if (User.Identity.IsAuthenticated)
                {
                    var currentUser = _userManager.GetUserAsync(User).Result;
                    cart = _context.WineCarts.Include(x => x.WineCartProducts).ThenInclude(x => x.WineProducts).Single(x => x.ApplicationUserID == currentUser.Id);
                }
                else if (Request.Cookies.ContainsKey("cart_id"))
                {
                    int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                    cart = _context.WineCarts.Include(x => x.WineCartProducts).ThenInclude(x => x.WineProducts).FirstOrDefault(x => x.ID == existingCartID);
                }
                foreach (var cartItem in cart.WineCartProducts)
                {
                    order.WineOrderProducts.Add(new WineOrderProduct
                    {
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now,
                        Quantity = cartItem.Quantity ?? 1,
                        ProductID = cartItem.WineProductsID,
                        ProductDescription = cartItem.WineProducts.Description,
                        ProductName = cartItem.WineProducts.Name,
                        ProductPrice = cartItem.WineProducts.Price ?? 0
                    });
                }

                _context.WineCartProducts.RemoveRange(cart.WineCartProducts);
                _context.WineCarts.Remove(cart);

                if (Request.Cookies.ContainsKey("cart_id"))
                {
                    Response.Cookies.Delete("cart_id");
                }

                _context.WineOrders.Add(order);
                _context.SaveChanges();
                //TODO: Save this information to the database so we can ship the order
                return RedirectToAction("Index", "Receipt", new { id = order.ID });
            }
            //TODO: we have an error!  Redisplay the form!
            return View();
        }
    }
}