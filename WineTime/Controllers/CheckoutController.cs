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
using SmartyStreets.USStreetApi;

namespace WineTime.Controllers
{
    public class CheckoutController : Controller
    {
        //inject
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;
        private IEmailSender _emailSender;
        private IBraintreeGateway _braintreeGateway;
        //SmartyStreets API called it "Client"
        private Client _client;

        public CheckoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender, IBraintreeGateway braintreeGateway, Client client)
        {
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
            _braintreeGateway = braintreeGateway;
            _client = client;
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
                    DateLastModified = DateTime.Now,
                    PaidDate = (DateTime?)null
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
                return RedirectToAction("Payment", new { id = order.ID });
            }
            return View();
        }

        public async Task<IActionResult> Payment(Guid id)
        {
            PaymentModel model = new PaymentModel();
            model.ID = id;
            model.Order = await _context.WineOrders.Include(x => x.WineOrderProducts).FirstOrDefaultAsync(x => x.ID == id);
            //TODO: Save this information to the database so we can ship the order
            ViewBag.ClientAuthorization = await _braintreeGateway.ClientToken.GenerateAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(PaymentModel model)
        {
            if (ModelState.IsValid)
            {
                model.Order = await _context.WineOrders.Include(x => x.WineOrderProducts).FirstOrDefaultAsync(x => x.ID == model.ID);
                var result = await _braintreeGateway.Transaction.SaleAsync(new TransactionRequest
                {
                    Amount = model.Order.WineOrderProducts.Sum(x => x.Quantity * x.ProductPrice),
                    PaymentMethodNonce = model.Nonce,
                    LineItems = model.Order.WineOrderProducts.Select(x => new TransactionLineItemRequest
                    {
                        Description = x.ProductDescription,
                        Name = x.ProductName,
                        Quantity = x.Quantity,
                        ProductCode = x.ProductID.Value.ToString(),
                        UnitAmount = x.ProductPrice,
                        TotalAmount = x.ProductPrice * x.Quantity,
                        LineItemKind = TransactionLineItemKind.DEBIT
                    }).ToArray()
                });
                await _emailSender.SendEmailAsync(model.Order.Email, "Your order " + model.ID, "Thanks for ordering! You bought : " + String.Join(",", model.Order.WineOrderProducts.Select(x => x.ProductName)));
                model.Order.PaidDate = DateTime.Now;
                await _context.SaveChangesAsync();
                //TODO: Save this information to the database so we can ship the order
                return RedirectToAction("Index", "Receipt", new { id = model.ID });
            }
            return View(model);
            
        }

        [HttpPost]
        public IActionResult ValidateAddress([FromBody]Lookup lookup)
        {
            try
            {
                _client.Send(lookup);
                return Json(lookup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }

}