using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_hwatso02.Models
{
    public class EFPurchaseRepository : IPurchaseRepository
    {
        //set constructors in framework

        private BookstoreContext context;
        public EFPurchaseRepository (BookstoreContext temp)
        {
            context = temp;
        }

        public IQueryable<Purchase> Purchases => context.Purchases.Include(b => b.Lines).ThenInclude(b => b.Book);

        public void SavePurchase(Purchase purchase)
        {
            context.AttachRange(purchase.Lines.Select(b => b.Book));

            if(purchase.PurchaseId == 0)
            {
                context.Purchases.Add(purchase);
            }

            context.SaveChanges();
        }
    }
}
