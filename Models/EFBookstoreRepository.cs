using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_hwatso02.Models
{

    //implementation of interface created
    public class EFBookstoreRepository : IBookstoreRepository
    {
        //set constructors in framework
        private BookstoreContext context { get; set; }
        public EFBookstoreRepository(BookstoreContext temp) => context = temp;

        public IQueryable<Book> Books => context.Books;
    }
}
