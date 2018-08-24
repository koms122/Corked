using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineTime.Models;

namespace WineTime.Controllers
{
    public class SubscriptionController : Controller
    {
        public IActionResult Index()
        {
            Subscription model = new Subscription();
            return View(model);
        }
    }
}