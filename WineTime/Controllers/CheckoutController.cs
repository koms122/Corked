using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineTime.Data;
using WineTime.Models;
using WineTime.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Braintree;

namespace WineTime.Controllers
{
    public class CheckoutController : Controller
    {
        //inject
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;
        private IEmailSender _emailSender;
        private IBraintreeGateway _braintreeGateway;

        public CheckoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender, IBraintreeGateway braintreeGateway)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
            _braintreeGateway = braintreeGateway;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            CheckoutModel model = new CheckoutModel();
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                model.Email = currentUser.Email;
            }

            ViewBag.ClientAuthorization = await _braintreeGateway.ClientToken.GenerateAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CheckoutModel model, string nonce)
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

                var result = await _braintreeGateway.Transaction.SaleAsync(new TransactionRequest
                {
                    Amount = order.WineOrderProducts.Sum(x => x.Quantity * x.ProductPrice),
                    PaymentMethodNonce = nonce
                });

                await _emailSender.SendEmailAsync(model.Email, "Your order " + order.ID, "Thanks for ordering! You bought : " + String.Join(",", order.WineOrderProducts.Select(x => x.ProductName)));

                //TODO: Save this information to the database so we can ship the order
                return RedirectToAction("Index", "Receipt", new { id = order.ID });
            }
            //TODO: we have an error!  Redisplay the form!
            return View();
        }
    }
}