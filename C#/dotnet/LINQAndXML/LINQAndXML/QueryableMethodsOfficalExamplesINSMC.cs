using System;
using System.Collections.Generic;
using System.Linq;

namespace QueryableMethodsOfficalExamplesINSMC
{
	public class Pet
	{
		public string Name { get; set; }

		public int Age { get; set; }
	}

	public class Person
	{
		public string LastName { get; set; }

		public Pet[] Pets { get; set; }
	}

	public class AllEx
	{

		static void Main(string[] args)
		{
			AllEx2();
		}

		public static void AllEx1()
		{
			Pet[] pets = { new Pet { Name = "Barley",Age = 10},
							new Pet{ Name = "Boots",Age = 4},
							new Pet { Name = "Whiskers",Age = 6} };

			bool allStartsWithB = pets.AsQueryable().All(pet => pet.Name.StartsWith("B"));

			Console.WriteLine("{0} pets names start with 'B' ", allStartsWithB ? "All" : "Not all");
		}

		public static void AllEx2()
		{
			List<Person> people = new List<Person>
			{ new Person { LastName = "Haas",
						   Pets = new Pet[] { new Pet { Name="Barley", Age=10 },
											  new Pet { Name="Boots", Age=14 },
											  new Pet { Name="Whiskers", Age=6 }}},
			  new Person { LastName = "Fakhouri",
						   Pets = new Pet[] { new Pet { Name = "Snowball", Age = 1}}},
			  new Person { LastName = "Antebi",
						   Pets = new Pet[] { new Pet { Name = "Belle", Age = 8} }},
			  new Person { LastName = "Philips",
						   Pets = new Pet[] { new Pet { Name = "Sweetie", Age = 2},
											  new Pet { Name = "Rover", Age = 13}} }
			};
			IEnumerable<string> names = from person in people
										where person.Pets.AsQueryable().All(pet => pet.Age > 5)
										select person.LastName;

			foreach (var name in names)
			{
				Console.WriteLine(name);
			}
		}


	}
}
