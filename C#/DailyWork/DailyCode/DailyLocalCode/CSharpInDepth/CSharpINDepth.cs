using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CSharpINDepth
{
    public static class CSharpINDepth
    {
        static void Main0(string[] args)
        {
            List<Product> products = Product.GetSampleProducts();
            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }
            /*products.Sort((x, y) => x.Name.CompareTo(y.Name));
            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }*/
            Console.WriteLine("===============");
            foreach (Product product in products.OrderBy(p => p.Name))
            {
                Console.WriteLine(product);
            }
        }

        static void Main1(string[] args)
        {
            string text = @"Do you like green eggs and ham?
                            I do not like them, Sam-I-am.
                            I do not like green eggs and ham.";
            Dictionary<string, int> frequencies = CountWords(text);
            foreach (KeyValuePair<string, int> entry in frequencies)
            {
                string word = entry.Key;
                int frequency = entry.Value;
                System.Console.WriteLine("{0}:{1}", word, frequency);
            }
        }

        static void Main2(string[] args)
        {
            List<int> integers = new List<int>();
            integers.Add(1);
            integers.Add(2);
            integers.Add(3);
            integers.Add(4);
            Converter<int, double> converter = TakeSquareRoot;
            List<double> doubles;
            doubles = integers.ConvertAll<double>(converter);
            foreach (double d in doubles)
            {
                Console.WriteLine(d);
            }
        }

        static void Main3(string[] args)
        {
            Nullable<int> x = 5;
            x = new Nullable<int>(5);
            Console.WriteLine("Instance with value:");
            Display(x);

            x = new Nullable<int>();
            Console.WriteLine("Instance without value:");
            Display(x);
            /* 
            Instance with value:
            HasValue:True
            Value:5
            Explicit conversion:5
            GetValueOrDefault():5
            GetValueOrDefault(10):5
            ToString():"5"
            GetHashCode():5

            Instance without value:
            HasValue:False
            GetValueOrDefault():0
            GetValueOrDefault(10):10
            ToString():""
            GetHashCode():0
            */

            Nullable<int> nullable = 5;

            object boxed = nullable;// 装箱成“有值的可空类型的实例”
            Console.WriteLine(boxed.GetType());

            int normal = (int)boxed;// 拆箱成非可空变量
            Console.WriteLine(normal);

            nullable = (Nullable<int>)boxed;// 拆箱成可空变量
            Console.WriteLine(nullable);

            nullable = new Nullable<int>();
            boxed = nullable;// 装箱成“没有值的可空类型的实例”

            Console.WriteLine(boxed == null);

            nullable = (Nullable<int>)boxed;// 拆箱成可空变量
            Console.WriteLine(nullable.HasValue);

            Person turing = new Person("Alan Turing", new DateTime(1912, 6, 23), new DateTime(1954, 6, 7));
            Person knuth = new Person("Donald Knuth", new DateTime(1938, 1, 10), null);
        }

        static void Main4(string[] args)
        {
            PrintValueAsInt32(5);
            PrintValueAsInt32("some string");

            int? parsed = TryParse("Not valid");
            if (parsed != null)
            {
                Console.WriteLine("Parsed to {0}",parsed.Value);
            }
            else
            {
                Console.WriteLine("Couldn't parse");
            }
        }

        static void Main5(string[] args)
        {
            Action<string> printReverse = (string text) =>
            {
                char[] chars = text.ToCharArray();
                Array.Reverse(chars);
                Console.WriteLine(new string(chars));
            };

            Action<int> printRoot = (int number) =>
            {
                Console.WriteLine(Math.Sqrt(number));
            };

            Action<IList<double>> printMean = (IList<double> numbers) =>
            {
                double total = 0;
                foreach (double value in numbers)
                {
                    total += value;
                }
                Console.WriteLine(total / numbers.Count);
            };

            printReverse("Hello World");
            printRoot(2);
            printMean(new double[] { 1.5,2.5,3,4.5});
        }

        static void Main6(string[] args)
        {
            EnclosingMethod();
            EnclosingMethod2();
            Action x = CreateDelegateInstance();
            x();
            x();
            /*
            5
            6
            7
            */

            Action[] delegates = new Action[2];
            int outside = 0;
            for (int i = 0; i < 2; i++)
            {
                int inside = 0;
                delegates[i] = () =>
                {
                    Console.WriteLine("{0},{1}",outside,inside);
                    outside++;
                    inside++;
                };
            }
            Action first = delegates[0];
            Action second = delegates[1];
            first();
            first();
            first();

            second();
            second();
            /*
            0,0
            1,1
            2,2
            3,0
            4,1
            */
        }

        static void Main7(string[] args)
        {
            object[] values = { "a", "b", "c", "d", "e" };
            IterationSample collection = new IterationSample(values, 3);
            foreach (object x in collection)
            {
                Console.WriteLine(x);
            }
            
            // 从 Console 程序中获取当前程序路径 
            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine("==============================");
            Console.WriteLine("AppDomain.CurrentDomain.BaseDirectory=>" + AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine("==============================");
            Console.WriteLine(Path.GetDirectoryName((Process.GetCurrentProcess().MainModule.FileName)));
            Console.WriteLine("==============================");
            Console.WriteLine(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine("==============================");
            Console.WriteLine(Assembly.GetExecutingAssembly().CodeBase);          
            Console.WriteLine("==============================");
            Console.WriteLine(Environment.GetCommandLineArgs()[0]);
        }

        static void Main8(string[] args)
        {
            //Console.WriteLine(Directory.GetCurrentDirectory());

            decimal m = 1m / 6m;
            double d = 1.0 / 6.0;

            decimal mm = m + m + m + m + m + m;
            double dd = d + d + d + d + d + d;

            Console.WriteLine(mm);
            //1.0000000000000000000000000002
            Console.WriteLine(dd);
            //0.9999999999999999
            Console.WriteLine(mm == 1m);// False
            Console.WriteLine(dd < 1.0);// True

        }

        static IEnumerable<int> CountWithTimeLimit(DateTime limit)
        {
            for (int i = 0; i <=100; i++)
            {
                if (DateTime.Now >= limit)
                {
                    yield break;
                }
                yield return i;
            }
        }

        static Action CreateDelegateInstance() 
        {
            int counter = 5;
            Action ret = () =>
            {
                Console.WriteLine(counter);
                counter++;
            };
            ret();
            return ret;
        }

        // 对于一个捕获变量，只要还有任何委托实例在引用它，它就会一直存在
        static void EnclosingMethod2()
        {
            string captured = "before x is created";

            Action x = () => 
            {
                Console.WriteLine(captured);
                captured = "changed by x";
            };
            captured = "directly before x is invoked";
            x();
            Console.WriteLine(captured);
            captured = "before second invocation";
            x();
        }

        static void EnclosingMethod()
        {
            int outerVariable = 5;
            string capturedVariable = "captured";

            if (DateTime.Now.Hour == 23)
            {
                int normalLocalVariable = DateTime.Now.Minute;
                Console.WriteLine(normalLocalVariable);
            }

            Action x = delegate ()
            {
                string anonLocal = " local to anonymous method";
                Console.WriteLine(capturedVariable + anonLocal);
            };
            x();
        }

        static int? TryParse(string text)
        {
            int ret;
            if (int.TryParse(text,out ret))
            {
                return ret;
            }
            else
            {
                return null;
            }
        }

        static void PrintValueAsInt32(object o)
        {
            int? nullable = o as int?;
            Console.WriteLine(nullable.HasValue? nullable.Value.ToString() :"null");
        }

        static void Display(Nullable<int> x)
        {
            Console.WriteLine("HasValue:{0}",x.HasValue);
            if (x.HasValue)
            {
                Console.WriteLine("Value:{0}",x.Value);
                Console.WriteLine("Explicit conversion:{0}",(int)x);
            }
            Console.WriteLine("GetValueOrDefault():{0}",x.GetValueOrDefault());
            Console.WriteLine("GetValueOrDefault(10):{0}",x.GetValueOrDefault(10));
            Console.WriteLine("ToString():\"{0}\"",x.ToString());
            Console.WriteLine("GetHashCode():{0}",x.GetHashCode());
            Console.WriteLine();
        }

        static double TakeSquareRoot(int x)
        {
            return Math.Sqrt(x);
        }

        static Dictionary<string,int> CountWords(string text)
        {
            Dictionary<string,int> frequencies;
            frequencies = new Dictionary<string,int>();

            string[] words = Regex.Split(text,@"\W+");
            foreach (string word in words)
            {
                if (frequencies.ContainsKey(word))
                {
                    frequencies[word]++;
                }
                else
                {
                    frequencies[word] = 1;
                }
            }
            return frequencies;
        }
    }

    public class Product
    {
        readonly string name;
        public string Name { get { return name; } }
        readonly decimal price;
        public decimal Price { get { return price; } }

        public Product(string name, decimal price)
        {
            this.name = name;
            this.price = price;
        }

        public static List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product(name:"West Side Story",price:9.99m),
                new Product(name:"Assassins",price:14.99m),
                new Product(name:"Frogs",price:13.99m),
                new Product(name:"Sweeny",price:10.99m)
            };
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}",name,price);
        }
    }

    public class Person
    {
        DateTime birth;
        DateTime? death;
        string name;

        public TimeSpan Age
        {
            get
            {
                if (death == null)
                {
                    return DateTime.Now - birth;
                }
                else
                {
                    return death.Value - birth;
                }
            }
        }

        public Person(string name,DateTime birth,DateTime? death)
        {
            this.birth = birth;
            this.death = death;
            this.name = name;
        }
    }

    public static class PartialComparer
    {
        public static int? Compare<T>(T first, T second)
        {
            return Compare(Comparer<T>.Default, first, second);
        }

        public static int? Compare<T>(IComparer<T> comparer, T first, T second)
        {
            int ret = comparer.Compare(first,second);
            return ret == 0 ? new int?() : ret;
        }

        public static int? ReferenceCompare<T>(T first, T second) where T : class
        {
            return first == second ? 0
                    : first == null ? -1
                    : second == null ? 1
                    : new int?();
        }

    }

    public class IterationSample : IEnumerable
    {
        public object[] values;
        public int startingPoint;

        public IterationSample(object[] values, int startPoint)
        {
            this.values = values;
            this.startingPoint = startPoint;
        }

        public IEnumerator GetEnumerator()
        {
            for (int index = 0; index < values.Length; index++)
            {
                yield return values[(index + startingPoint) % values.Length];
            }
            //return new IterationSampleIterator(this);
        }
    }

    class IterationSampleIterator : IEnumerator
    {
        // 1.正在迭代的集合
        IterationSample parent;
        // 2.指出遍历到的位置
        int position;

        internal IterationSampleIterator(IterationSample parent)
        {
            this.parent = parent;
            // 3.在第一个元素之前开始
            position = -1;
        }


        public bool MoveNext()
        {
            // 4.如果仍要遍历，那么增加 position 的值
            if (position != parent.values.Length)
            {
                position++;
            }
            return position < parent.values.Length;
        }

        public object Current
        {
            get
            {
                // 5.防止访问第一个元素之前和最后一个元素之后
                if (position == -1 || position == parent.values.Length)
                {
                    throw new InvalidOperationException();
                }
                int index = position + parent.startingPoint;
                // 6.实现封装
                index = index % parent.values.Length;
                return parent.values[index];
            }
        }

        public void Reset()
        {
            // 7.返回第一个元素之前
            position = -1;
        }
    }

}
