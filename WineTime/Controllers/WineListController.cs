using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineTime.Models;

namespace WineTime.Controllers
{
    public class WineListController : Controller
    {
        public IActionResult Index(string category)
        {
            List<WineProducts> model = new List<WineProducts>();

            if (string.IsNullOrEmpty(category))
            {
                ViewData["Title"] = "All Products";
                model.Add(new WineProducts
                {
                    ID = 1,
                    Name = "Barefoot Pinot Grigio",
                    Description = "Tart green apple flavors get down with a white peach undertone. " +
                    "Floral blossoms and citrus aromas do the tango. Barefoot’s Pinot Grigio is light-bodied with a bright finish.",
                    ImagePath = "./images/BarefootPinotGrigio.jpeg",
                    Price = 9.99m
                });
                model.Add(new WineProducts
                {
                    ID = 2,
                    Name = "Educated Guess Cabernet Sauvignon",
                    Description = "The Educated Guess Cabernet Sauvignon is crafted from grapes " +
                    "grown in the prestigious Napa Valley wine districts of Yountville, Oak Knoll, Calistoga, Oakville, and Rutherford.",
                    ImagePath = "./images/EducatedGuessCabernetSauvignon.jpeg",
                    Price = 26.99m
                });
                model.Add(new WineProducts
                {
                    ID = 3,
                    Name = "Justin Cabernet Sauvignon",
                    Description = "This big, bold artisanal wine is brought to you by " +
                    "some of the pioneers of Paso Robles in California's Central Coast. ",
                    ImagePath = "./images/JustinCabernetSauvignon.jpg",
                    Price = 34.99m
                });
                Console.WriteLine("Get All Products");
            }
            else if (category.ToLowerInvariant() == "redwine")
            {
                ViewData["Title"] = "Red Wine";
                model.Add(new WineProducts
                {
                    ID = 2,
                    Name = "Educated Guess Cabernet Sauvignon",
                    Description = "The Educated Guess Cabernet Sauvignon is crafted from grapes " +
                    "grown in the prestigious Napa Valley wine districts of Yountville, Oak Knoll, Calistoga, Oakville, and Rutherford.",
                    ImagePath = "./images/EducatedGuessCabernetSauvignon.jpeg",
                    Price = 26.99m
                });
                model.Add(new WineProducts
                {
                    ID = 3,
                    Name = "Justin Cabernet Sauvignon",
                    Description = "This big, bold artisanal wine is brought to you by " +
                    "some of the pioneers of Paso Robles in California's Central Coast. ",
                    ImagePath = "./images/JustinCabernetSauvignon.jpg",
                    Price = 34.99m
                });
                Console.WriteLine("Get Red Wine");
            }
            else if (category.ToLowerInvariant() == "whitewine")
            {
                ViewData["Title"] = "White Wine";
                model.Add(new WineProducts
                {
                    ID = 1,
                    Name = "Barefoot Pinot Grigio",
                    Description = "Tart green apple flavors get down with a white peach undertone" +
                    "Floral blossoms and citrus aromas do the tango. Barefoot’s Pinot Grigio is light-bodied with a bright finish.",
                    ImagePath = "./images/BarefootPinotGrigio.jpeg",
                    Price = 9.99m
                });
                Console.WriteLine("Get White Wine");
            }

            return View(model);
        }
        public IActionResult Details(int? id)
        {
            WineProducts model = new WineProducts
            {
                ID = 1,
                Name = "Barefoot Pinot Grigio",
                Description = "Tart green apple flavors get down with a white peach undertone. " +
                    "Floral blossoms and citrus aromas do the tango. Barefoot’s Pinot Grigio is light-bodied with a bright finish.",
                ImagePath = "./images/BarefootPinotGrigio.jpeg",
                Price = 9.99m
            };
            return View(model);
        }

        /*
        [HttpPost]
        public IActionResult Details(int? id, int quantity, string color)
        {
            // TODO: Take the Posted details and update the user's cart

            return RedirectToAction("Index", "Cart");
        }
        */
    }
}
