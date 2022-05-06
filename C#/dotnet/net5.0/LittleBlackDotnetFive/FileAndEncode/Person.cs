using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAndEncode
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public decimal Salary { get; set; }

        public Person()
        {

        }

        public Person(decimal s)
        {
            Salary = s;
        }

        public override string ToString()
        {
            return $"{FirstName} {Salary}";
        }
    }
}
