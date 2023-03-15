using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission09_hwatso02.Infrastructure;
using Mission09_hwatso02.Models;

namespace Mission09_hwatso02.Pages
{
    public class PurchaseModel : PageModel
    {        
        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }
        //load up data since there isn't a controller
        private IBookstoreRepository repo { get; set; }
        public PurchaseModel (IBookstoreRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }

        //keep items in cart, or create new if empty
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        //when adding an item to cart, update if there's already things there
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(b => b.BookId == bookId);

            cart.AddItem(b, 1);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        //after removing a book, go back to Cart page
        public IActionResult OnPostRemove(int bookId, string returnUrl)
        {
            cart.RemoveItem(cart.Items.First(b => b.Book.BookId == bookId).Book);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
