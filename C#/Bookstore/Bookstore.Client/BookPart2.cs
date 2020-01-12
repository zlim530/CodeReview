namespace Bookstore.Client
{
    using System;
    using System.Collections.Generic;

    public partial class Book
    {
        public string Report()
        {
            return $"#{ID} Name:{Name} Price:{Price}";
        }
    }
}
