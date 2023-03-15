using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_hwatso02.Models
{
    public class Cart
    {
        //declare and instantiate
        public List<CartLineItem> Items { get; set; } = new List<CartLineItem>();

        public virtual void AddItem (Book book, int qty)
        {
            CartLineItem Line = Items
                .Where(b => b.Book.BookId == book.BookId)
                .FirstOrDefault();

            if (Line == null)
            {
                Items.Add(new CartLineItem
                {
                    Book = book,
                    Quantity = qty
                });
            }
            else
            {
                Line.Quantity += qty;
            }
        }

        //be able to remove a book from cart
        public virtual void RemoveItem (Book book)
        {
            Items.RemoveAll(b => b.Book.BookId == book.BookId);
        }

        //be able to clear whole cart
        public virtual void ClearCart()
        {
            Items.Clear();
        }

         //total sum of all cart items
        public double CalculateTotal()
        {
                double sum = Items.Sum(b => b.Quantity * b.Book.Price);

                return sum;
        }
    }

    //list important things to show in cart
    public class CartLineItem
    {
        public int LineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
