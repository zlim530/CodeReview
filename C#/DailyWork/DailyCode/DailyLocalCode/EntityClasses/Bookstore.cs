namespace Bookstore
{
    using System.Collections;

    public struct Book
    {
        public string Title;
        public string Author;
        public decimal Price;
        public bool Paperback;

        public Book(string title,string autohr, decimal price, bool paperBack)
        {
            Title = title;
            Author = autohr;
            Price = price;
            Paperback = paperBack;
        }
    }

    public delegate void ProcessBookCallback(Book book);

    public class BookDB
    {
        ArrayList list = new ArrayList();

        public void AddBook(string title, string author, decimal price, bool paperBack)
        {
            list.Add(new Book(title, author, price, paperBack));
        }

        public void ProcessPaperbackBooks(ProcessBookCallback processBook)
        {
            foreach (Book b in list)
            {
                if (b.Paperback)
                    processBook(b);
            }
        }
    }
}

namespace BookTestClient
{
    using Bookstore;
    using System;

    class PriceTotaller
    {
        int countBooks = 0;
        decimal pricesBooks = 0.0m;

        internal void AddBookToTotal(Book book)
        {
            countBooks += 1;
            pricesBooks += book.Price;
        }

        internal decimal AveragePrice()
        {
            return pricesBooks / countBooks;
        }
    }

    class Test
    {
        static void PrintTitle(Book b)
        {
            Console.WriteLine($"     {b.Title}");
        }

        static void AddBooks(BookDB bookDB)
        {
            bookDB.AddBook("The C Programming Language","Brian W. Kernighan and Dennis M. Ritchie",19.95m,true);
            bookDB.AddBook("The Unicode Standard 2.0","The Unicode Consortium",39.95m,true);
            bookDB.AddBook("The MS-DOS Encyclopedia","Ray Duncan",129.95m,false);
            bookDB.AddBook("Dogbert's Clues for the Clueless","Scott Adams",12.00m,true);
        }

        static void Main0()
        {
            BookDB bookDB = new BookDB();
            AddBooks(bookDB);
            Console.WriteLine("Paperback Book Titles:");
            bookDB.ProcessPaperbackBooks(PrintTitle);
            PriceTotaller totaller = new PriceTotaller();
            bookDB.ProcessPaperbackBooks(totaller.AddBookToTotal);
            Console.WriteLine("Average Parperback Book Price:${0:#.##}",totaller.AveragePrice());
        }

    }

}