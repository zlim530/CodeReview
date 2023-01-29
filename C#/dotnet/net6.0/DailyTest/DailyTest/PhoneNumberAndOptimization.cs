using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyTest
{
    public class PhoneNumberAndOptimization
    {
        static void Main0(string[] args)
        {
            var phoneNumber = "56";
            //var result = Combinations(phoneNumber);
            var result = CombinationsOptimization(phoneNumber);
            foreach (var r in result)
            {
                Console.WriteLine(r);
            }
            //File.WriteAllLines(@"D:\Temp\output.txt",result);
        }

        public static IList<string> Combinations(string digits)
        {
            var dict = new Dictionary<char, char[]>
            {
                { '2', new []{ 'a','b','c'} },
                { '3', new []{ 'd','e','f'} },
                { '4', new []{ 'g','h','i'} },
                { '5', new []{ 'j','k','l'} },
                { '6', new []{ 'm','n','0'} },
                { '7', new []{ 'p','q','r','s'} },
                { '8', new []{ 't','u','v'} },
                { '9', new []{ 'w','x','y','z'} }
            };

            var result = new List<string>();
            if (string.IsNullOrEmpty(digits)) return result;
            var q = new Queue<string>();
            q.Enqueue(String.Empty);
            while (q.Count() > 0)
            {
                var cur = q.Dequeue();
                if (cur.Length == digits.Length)
                {
                    result.Add(cur);
                }
                else
                {
                    foreach (var c in dict[digits[cur.Length]])
                    {
                        q.Enqueue(cur + c);
                    }
                }
            }

            return result;
        }

        public static IList<string> CombinationsOptimization(string digits)
        {
            string[] dict = { null, null, "abc","def","ghi","jkl","mno","pqrs","tuv","wxyz"};

            var result = new List<string>();
            if (string.IsNullOrEmpty(digits)) return result;
            var q = new Queue<string>();
            q.Enqueue(String.Empty);
            while (q.Count() > 0)
            {
                var cur = q.Dequeue();
                if (cur.Length == digits.Length)
                {
                    result.Add(cur);
                }
                else
                {
                    foreach (var c in dict[digits[cur.Length] - '0'])
                    {
                        q.Enqueue(cur + c);
                    }
                }
            }

            return result;
        }
    }
}
