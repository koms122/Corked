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

        public async Task<IActionResult> Index(string category)
        {
            if(await _context.WineProduct.CountAsync() == 0)
            {
                List<WineProducts> RedWine = new List<WineProducts>();
                WineCategory redWine = await _context.WineCategories.Include(x => x.WineProduct).FirstOrDefaultAsync(x => x.Name == "RedWine");
                if(redWine == null)
                {
                    redWine = new WineCategory { Name = "RedWine", WineProduct = RedWine };
                    _context.WineCategories.Add(redWine);
                }
                if (redWine.WineProduct.Count == 0)
                {
                    RedWine.Add(new WineProducts { Name = "Educated Guess Cabernet Sauvignon", ImagePath = "./images/EducatedGuessCabernetSauvignon.jpeg", Description = "Grown in the prestigious Napa Valley", Price = 26.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                    RedWine.Add(new WineProducts { Name = "2 Educated Guess Cabernet Sauvignon", ImagePath = "./images/EducatedGuessCabernetSauvignon.jpeg", Description = "Grown in the prestigious Napa Valley", Price = 26.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                    RedWine.Add(new WineProducts { Name = "3 Educated Guess Cabernet Sauvignon", ImagePath = "./images/EducatedGuessCabernetSauvignon.jpeg", Description = "Grown in the prestigious Napa Valley", Price = 26.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                }
                
                List<WineProducts> WhiteWine = new List<WineProducts>();
                WineCategory whiteWine = await _context.WineCategories.Include(x => x.WineProduct).FirstOrDefaultAsync(x => x.Name == "WhiteWine");
                if (whiteWine == null)
                {
                    whiteWine = new WineCategory { Name = "WhiteWine", WineProduct = WhiteWine };
                    _context.WineCategories.Add(whiteWine);
                }
                if (whiteWine.WineProduct.Count == 0)
                {
                    WhiteWine.Add(new WineProducts { Name = "Barefoot Pinot Grigio", ImagePath = "./images/BarefootPinotGrigio.jpeg", Description = "Tart green apple flavors", Price = 9.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                    WhiteWine.Add(new WineProducts { Name = "2 Barefoot Pinot Grigio", ImagePath = "./images/BarefootPinotGrigio.jpeg", Description = "Tart green apple flavors", Price = 9.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                    WhiteWine.Add(new WineProducts { Name = "3 Barefoot Pinot Grigio", ImagePath = "./images/BarefootPinotGrigio.jpeg", Description = "Tart green apple flavors", Price = 9.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });

                }

                List<WineProducts> Rose = new List<WineProducts>();
                WineCategory rose = await _context.WineCategories.Include(x => x.WineProduct).FirstOrDefaultAsync(x => x.Name == "Rose");
                if (rose == null)
                {
                    rose = new WineCategory { Name = "Rose", WineProduct = Rose };
                    _context.WineCategories.Add(rose);
                }
                if (rose.WineProduct.Count == 0)
                {
                    Rose.Add(new WineProducts { Name = "Dark Horse Rose", ImagePath = "./images/DarkHorseRose.jpeg", Description = "Don’t let the pale pink fool you, Dark Horse Rosé is only blushing from all the attention", Price = 15.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                    Rose.Add(new WineProducts { Name = "2 Dark Horse Rose", ImagePath = "./images/DarkHorseRose.jpeg", Description = "Don’t let the pale pink fool you, Dark Horse Rosé is only blushing from all the attention", Price = 15.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                    Rose.Add(new WineProducts { Name = "3 Dark Horse Rose", ImagePath = "./images/DarkHorseRose.jpeg", Description = "Don’t let the pale pink fool you, Dark Horse Rosé is only blushing from all the attention", Price = 15.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                }


                List<WineProducts> Champagne = new List<WineProducts>();
                WineCategory champagne = await _context.WineCategories.Include(x => x.WineProduct).FirstOrDefaultAsync(x => x.Name == "Champagne");
                if (champagne == null)
                {
                    champagne = new WineCategory { Name = "Champagne", WineProduct = Champagne };
                    _context.WineCategories.Add(champagne);
                }
                if (champagne.WineProduct.Count == 0)
                {
                    Champagne.Add(new WineProducts { Name = "Veuve Clicquot Yellow Label", ImagePath = "./images/VeuveClicqout.jpeg", Description = "Veuve Clicquot Yellow Label Champagne is the signature champagne of the House", Price = 59.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                    Champagne.Add(new WineProducts { Name = "2 Veuve Clicquot Yellow Label", ImagePath = "./images/VeuveClicqout.jpeg", Description = "Veuve Clicquot Yellow Label Champagne is the signature champagne of the House", Price = 59.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                    Champagne.Add(new WineProducts { Name = "3 Veuve Clicquot Yellow Label", ImagePath = "./images/VeuveClicqout.jpeg", Description = "Veuve Clicquot Yellow Label Champagne is the signature champagne of the House", Price = 59.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                }
                

                List<WineProducts> CabernetSauvignon = new List<WineProducts>();
                WineCategory cabernetSauvignon = await _context.WineCategories.Include(x => x.WineProduct).FirstOrDefaultAsync(x => x.Name == "CabernetSauvignon");
                if (cabernetSauvignon == null)
                {
                    cabernetSauvignon = new WineCategory { Name = "CabernetSauvignon", WineProduct = CabernetSauvignon };
                    _context.WineCategories.Add(cabernetSauvignon);
                }
                if (cabernetSauvignon.WineProduct.Count == 0)
                {
                    CabernetSauvignon.Add(new WineProducts { Name = "Decoy", ImagePath = "./images/Decoy.jpeg", Description = "The lush fruit flavors are framed by rich tannins and hints of sweet oak and spice", Price = 31.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                    CabernetSauvignon.Add(new WineProducts { Name = "2 Decoy", ImagePath = "./images/Decoy.jpeg", Description = "The lush fruit flavors are framed by rich tannins and hints of sweet oak and spice", Price = 31.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                    CabernetSauvignon.Add(new WineProducts { Name = "3 Decoy", ImagePath = "./images/Decoy.jpeg", Description = "The lush fruit flavors are framed by rich tannins and hints of sweet oak and spice", Price = 31.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                }

                List<WineProducts> Chardonnay = new List<WineProducts>();
                WineCategory chardonnay = await _context.WineCategories.Include(x => x.WineProduct).FirstOrDefaultAsync(x => x.Name == "Chardonnay");
                if (chardonnay == null)
                {
                    chardonnay = new WineCategory { Name = "Chardonnay", WineProduct = Chardonnay };
                    _context.WineCategories.Add(chardonnay);
                }
                if (chardonnay.WineProduct.Count == 0)
                {
                    Chardonnay.Add(new WineProducts { Name = "Butter", ImagePath = "./images/Butter.jpeg", Description = "Ethereal creaminess on the palate, woven with smooth vanilla scented oak", Price = 24.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                    Chardonnay.Add(new WineProducts { Name = "2 Butter", ImagePath = "./images/Butter.jpeg", Description = "Ethereal creaminess on the palate, woven with smooth vanilla scented oak", Price = 24.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                    Chardonnay.Add(new WineProducts { Name = "3 Butter", ImagePath = "./images/Butter.jpeg", Description = "Ethereal creaminess on the palate, woven with smooth vanilla scented oak", Price = 24.99m, DateCreated = DateTime.Now, DateLastModified = DateTime.Now });
                }

                await _context.SaveChangesAsync();
            }

            ViewBag.selectedCategory = category;
            List<WineProducts> model;
            if (string.IsNullOrEmpty(category))
            {
                model = await this._context.WineProduct.ToListAsync();
            }
            else
            {
                model = await this._context.WineProduct.Where(x => x.WineCategoryName == category).ToListAsync();
            }

            ViewData["Categories"] = await this._context.WineCategories.Select(x => x.Name).ToArrayAsync();
            return View(model);
        }
        public async Task<IActionResult> Details(int? id)
        {
            WineProducts model = await _context.WineProduct.FindAsync(id);
            return View(model);
        }

        //change add to cart logic for anonymous users and the applicationuser for signed in users
        [HttpPost]
        public async Task<IActionResult> Details(int? id, int quantity, string color)
        {
            WineCart cart = null;
            //how you can tell if someone's logged in
            if (User.Identity.IsAuthenticated)
            {
                //Authenticated path
                var currentUser = await _userManager.GetUserAsync(User);
                //grabs hold of the current users' winecart
                cart = await _context.WineCarts.Include(x => x.WineCartProducts).FirstOrDefaultAsync(x => x.ApplicationUserID == currentUser.Id);
                if (cart == null)
                {
                    cart = new WineCart();
                    cart.ApplicationUserID = currentUser.Id;
                    cart.DateCreated = DateTime.Now;
                    cart.DateLastModified = DateTime.Now;
                    _context.WineCarts.Add(cart);
                }
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
                    cart = await _context.WineCarts.Include(x => x.WineCartProducts).FirstOrDefaultAsync(x => x.ID == existingCartID);
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
            if (product == null)
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

            await _context.SaveChangesAsync();

            if (!User.Identity.IsAuthenticated)
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
