﻿using Microsoft.AspNetCore.Mvc;
using Mission09_hwatso02.Models;
using Mission09_hwatso02.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_hwatso02.Controllers
{
    public class HomeController : Controller
    {

        //use repository instead of controller
        private IBookstoreRepository repo;

        public HomeController (IBookstoreRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index(string category, int pageNum = 1)
        {
            //set variables and show only so many books on a page
            int pageSize = 10;

            //revamp since we are now passing in more info
            var book = new BooksViewModel
            {
                Books = repo.Books
                .Where(b => b.Category == category || category == null)
                .OrderBy(b => b.Title)
                .Skip(pageSize * (pageNum - 1))
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks = (category == null 
                        ? repo.Books.Count() 
                        : repo.Books.Where(b => b.Category == category).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(book);
        }
    }
}
