using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineTime.Models;
using WineTime.Data;

namespace WineTime.Controllers
{
    public class WineListController : Controller
    {
        public ApplicationDbContext _context;

        public WineListController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index(string category)
        {
            if(_context.WineProducts.Count() == 0)
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
                model = this._context.WineProducts.ToList();
            }
            else
            {
                model = this._context.WineProducts.Where(x => x.WineCategoryName == category).ToList();
            }

            ViewData["Categories"] = this._context.WineCategories.Select(x => x.Name).ToArray();
            return View(model);
        }
        public IActionResult Details(int? id)
        {
            WineProducts model = _context.WineProducts.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Details(int? id, int quantity, string color)
        {
            // TODO: Take the Posted details and update the user's cart
            Console.WriteLine("User added " + id.ToString() + " , " + quantity.ToString() + ", " + color);
            return RedirectToAction("Index", "Cart");
        }

    }
}
