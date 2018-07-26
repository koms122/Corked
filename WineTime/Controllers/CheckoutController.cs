using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WineTime.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                //TODO: we havea n error..redisplay the form;
                return View();
            }
            else
            {
                //TODO: Save this info to the database for shipping purposes
                return RedirectToAction("Index", "Receipt", new { id = Guid.NewGuid() });
            }
        }
    }
}