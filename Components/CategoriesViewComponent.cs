using Microsoft.AspNetCore.Mvc;
using Mission09_hwatso02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_hwatso02.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        //load dataset
        private IBookstoreRepository repo { get; set; }

        public CategoriesViewComponent (IBookstoreRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            //highlight button if in category
            ViewBag.SelectedCategory = RouteData?.Values["Category"];

            //get different categories
            var categories = repo.Books
                .Select(b => b.Category)
                .Distinct()
                .OrderBy(b => b);

            return View(categories);
        }
    }
}
