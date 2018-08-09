using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineTime.Models;
using WineTime.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WineTime.Controllers
{
    public class WineListController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public WineListController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public IActionResult Index(string category)
        {
            if(_context.WineProduct.Count() == 0)
            {
                List<WineProducts> RedWine = new List<WineProducts>();
                RedWine.Add(new WineProducts { Name = "Educated Guess Cabernet Sauvignon", ImagePath = "./images/EducatedGuessCabernetSauvignon.jpeg", Description = "Grown in the prestigious Napa Valley", Price = 26.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                RedWine.Add(new WineProducts { Name = "2 Educated Guess Cabernet Sauvignon", ImagePath = "./images/EducatedGuessCabernetSauvignon.jpeg", Description = "Grown in the prestigious Napa Valley", Price = 26.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                RedWine.Add(new WineProducts { Name = "3 Educated Guess Cabernet Sauvignon", ImagePath = "./images/EducatedGuessCabernetSauvignon.jpeg", Description = "Grown in the prestigious Napa Valley", Price = 26.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                RedWine.Add(new WineProducts { Name = "4 Educated Guess Cabernet Sauvignon", ImagePath = "./images/EducatedGuessCabernetSauvignon.jpeg", Description = "Grown in the prestigious Napa Valley", Price = 26.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                RedWine.Add(new WineProducts { Name = "5 Educated Guess Cabernet Sauvignon", ImagePath = "./images/EducatedGuessCabernetSauvignon.jpeg", Description = "Grown in the prestigious Napa Valley", Price = 26.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                RedWine.Add(new WineProducts { Name = "6 Educated Guess Cabernet Sauvignon", ImagePath = "./images/EducatedGuessCabernetSauvignon.jpeg", Description = "Grown in the prestigious Napa Valley", Price = 26.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                RedWine.Add(new WineProducts { Name = "7 Educated Guess Cabernet Sauvignon", ImagePath = "./images/EducatedGuessCabernetSauvignon.jpeg", Description = "Grown in the prestigious Napa Valley", Price = 26.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                _context.WineCategories.Add(new WineCategory { Name = "RedWine", WineProduct = RedWine });

                List<WineProducts> WhiteWine = new List<WineProducts>();
                WhiteWine.Add(new WineProducts { Name = "Barefoot Pinot Grigio", ImagePath = "./images/BarefootPinotGrigio.jpeg", Description = "Tart green apple flavors", Price = 9.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                WhiteWine.Add(new WineProducts { Name = "2 Barefoot Pinot Grigio", ImagePath = "./images/BarefootPinotGrigio.jpeg", Description = "Tart green apple flavors", Price = 9.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                WhiteWine.Add(new WineProducts { Name = "3 Barefoot Pinot Grigio", ImagePath = "./images/BarefootPinotGrigio.jpeg", Description = "Tart green apple flavors", Price = 9.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                WhiteWine.Add(new WineProducts { Name = "4 Barefoot Pinot Grigio", ImagePath = "./images/BarefootPinotGrigio.jpeg", Description = "Tart green apple flavors", Price = 9.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                WhiteWine.Add(new WineProducts { Name = "5 Barefoot Pinot Grigio", ImagePath = "./images/BarefootPinotGrigio.jpeg", Description = "Tart green apple flavors", Price = 9.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                WhiteWine.Add(new WineProducts { Name = "6 Barefoot Pinot Grigio", ImagePath = "./images/BarefootPinotGrigio.jpeg", Description = "Tart green apple flavors", Price = 9.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                WhiteWine.Add(new WineProducts { Name = "7 Barefoot Pinot Grigio", ImagePath = "./images/BarefootPinotGrigio.jpeg", Description = "Tart green apple flavors", Price = 9.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                _context.WineCategories.Add(new WineCategory { Name = "WhiteWine", WineProduct = WhiteWine });

                _context.SaveChanges();
            }

            ViewBag.selectedCategory = category;
            List<WineProducts> model;
            if (string.IsNullOrEmpty(category))
            {
                model = this._context.WineProduct.ToList();
            }
            else
            {
                model = this._context.WineProduct.Where(x => x.WineCategoryName == category).ToList();
            }

            ViewData["Categories"] = this._context.WineCategories.Select(x => x.Name).ToArray();
            return View(model);
        }
        public IActionResult Details(int? id)
        {
            WineProducts model = _context.WineProduct.Find(id);
            return View(model);
        }

        //change add to cart logic for anonymous users and the applicationuser for signed in users
        [HttpPost]
        public IActionResult Details(int? id, int quantity, string color)
        {
            // TODO: Take the Posted details and update the user's cart
            WineCart cart = null;
            //how you can tell if someone's logged in
            if(User.Identity.IsAuthenticated)
            {
                //Authenticated path
                var currentUser =_userManager.GetUserAsync(User).Result;
                //grabs hold of the current users' winecart and fetches a 
                cart = _context.WineCarts.Include(x => x.WineCartProducts).Single(x => x.ID == currentUser.WineCartID);
            
            }
            else
            {
                if (Request.Cookies.ContainsKey("cart_id")) // Make sure that cart_id is consistent throughout the app
                {
                    //peel the cart_id out of the cookie
                    int existingCartID = int.Parse(Request.Cookies["cart_id"]);
                    //see if the cart exists:
                    // Using EF.Core (to join Carts and Products)
                    // If I don't use "Include", cart won't return products even if it's in the database
                    cart = _context.WineCarts.Include(x => x.WineCartProducts).FirstOrDefault(x => x.ID == existingCartID);
                    cart.DateLastModified = DateTime.Now;
                }

                if (cart == null)
                {
                    cart = new WineCart
                    {
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now
                    };
                    _context.WineCarts.Add(cart);
                }
            }
            
            //At this point, no matter what, it'll either be a newly created cart or an existing cart

            // Find the first product in the cart with the product ID we're looking for
            // If none exists, return "NULL"
            WineCartProduct product = cart.WineCartProducts.FirstOrDefault(x => x.WineProductsID == id);
            if(product == null)
            {
                product = new WineCartProduct
                {
                    DateLastModified = DateTime.Now,
                    DateCreated = DateTime.Now,
                    WineProductsID = id ?? 0,
                    Quantity = 0,
                };
                cart.WineCartProducts.Add(product);
            }
            product.Quantity += quantity;
            product.DateLastModified = DateTime.Now;

            _context.SaveChanges();

            if(!User.Identity.IsAuthenticated)
            {
                Response.Cookies.Append("cart_id", cart.ID.ToString(), new Microsoft.AspNetCore.Http.CookieOptions
                {
                    //Cookie expires in a year -- so the cart will stay existing for a year
                    Expires = DateTime.Now.AddYears(1)
                });
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}
