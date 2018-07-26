using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineTime.Models;

namespace WineTime.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            WineCart model = new WineCart
            {
                ID = 1,
                Products = new WineProducts[]
                {
                    new WineProducts
                    {
                        ID = 2,
                        Name = "Educated Guess Cabernet Sauvignon",
                        Description = "The Educated Guess Cabernet Sauvignon is crafted from grapes " +
                        "grown in the prestigious Napa Valley wine districts of Yountville, Oak Knoll, Calistoga, Oakville, and Rutherford.",
                        ImagePath = "./images/EducatedGuessCabernetSauvignon.jpeg",
                        Price = 26.99m
                    },
                    new WineProducts
                    {
                        ID = 3,
                        Name = "Justin Cabernet Sauvignon",
                        Description = "This big, bold artisanal wine is brought to you by " +
                        "some of the pioneers of Paso Robles in California's Central Coast. ",
                        ImagePath = "./images/JustinCabernetSauvignon.jpg",
                        Price = 34.99m
                    }
                }
            };
            return View(model);
        }
        public IActionResult Remove(int id)
        {
            // TODO: Look through the cart items and remove the prodcut with that ID
            return RedirectToAction("Index");
        }
    }
}
