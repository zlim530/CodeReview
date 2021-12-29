using System.Text.Json;

namespace ListDemo
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Book other = obj as Book;
            if (other.Id == this.Id && other.Name == this.Name && other.Price == this.Price) return true;
            return false;
        }

    }
}
