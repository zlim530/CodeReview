using Factory.Packet;
using System;

namespace CSharpTenNew
{
    public class Worker
    {
        public readonly string Planet = "Earth";
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public WorldCity favoriteCity { get; set; }

        public Worker()
        {
            Name = "Unknown";
            BirthDay = DateTime.Now;
            favoriteCity = WorldCity.Beijing;
        }

        public Worker(string name, DateTime birthDay, WorldCity favoriteCity)
        {
            Name = name;
            BirthDay = birthDay;
            this.favoriteCity = favoriteCity;
        }

        public string Greeting => $"Nice work {Name}";

        public int Age => DateTime.Now.Year - BirthDay.Year;

        public static void Surprise(Worker worker)
        {
            Console.WriteLine($"{worker.Greeting}");
            Console.WriteLine($"Send {worker.Name} to {worker.favoriteCity}, " +
                $"hope you have fun when you were {worker.Age} to {worker.Age + 1} years old.");
        }
    }
}
