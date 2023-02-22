namespace EntityFrameworkCoreModel
{
    public class Book
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string AuthorName { get; set; }

        public double Price { get; set; }

        public DateTime? PubDate { get; set; }
    }
}