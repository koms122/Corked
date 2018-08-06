using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineTime.Models;

namespace WineTime.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CheckoutModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: save this info for shipping purposes
                return RedirectToAction("Index", "Receipt", new { id = Guid.NewGuid() });
            }
            //TODO: we havea n error..redisplay the form;
            return View();
        }
    }
}