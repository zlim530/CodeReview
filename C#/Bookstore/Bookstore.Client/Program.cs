using System;
using System.Collections.Generic;

namespace Bookstore.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new BookstoreEntities();
            var books = dbContext.Book;
            foreach (var book in books)
            {
                //Console.WriteLine(book.Name);
                Console.WriteLine(book.Report());
            }
        }
    }
}
