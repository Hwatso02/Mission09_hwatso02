using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission09_hwatso02.Models;

namespace Mission09_hwatso02.Pages
{
    public class PurchaseModel : PageModel
    {
        //load up data since there isn't a controller
        private IBookstoreRepository repo { get; set; }
        public PurchaseModel (IBookstoreRepository temp)
        {
            repo = temp;
        }

        public Cart cart { get; set; }

        public void OnGet(Cart c)
        {
            cart = c;
        }

        public IActionResult OnPost(int bookId)
        {
            Book b = repo.Books.FirstOrDefault(b => b.BookId == bookId);

            cart = new Cart();
            cart.AddItem(b, 1);

            return RedirectToPage(cart);
        }
    }
}
