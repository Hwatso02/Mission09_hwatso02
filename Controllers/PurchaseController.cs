using Microsoft.AspNetCore.Mvc;
using Mission09_hwatso02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_hwatso02.Controllers
{
    public class PurchaseController : Controller
    {
        private IPurchaseRepository repo { get; set; }
        private Cart cart { get; set; }
        public PurchaseController(IPurchaseRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Purchase());
        }

        [HttpPost]
        public IActionResult Checkout(Purchase purchase)
        {
            //check if books are in cart
            if(cart.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            //if everything is valid
            if (ModelState.IsValid)
            {
                purchase.Lines = cart.Items.ToArray();
                repo.SavePurchase(purchase);
                cart.ClearCart();

                return RedirectToPage("/PurchaseComplete");
            }
            else
            {
                return View();
            }
        }
    }
}
