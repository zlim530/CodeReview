using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DailyLocalCode.SomeTryExample.DifferentElementsSorts
{
    public class DifferentElementsSorts
    {
        /// <summary>
        /// 使用不同排序方法对元素进行排序
        /// </summary>
        class Program
        {
            private static void Main0(string[] args)
            {
                ArrayList arrayList = Product.GetArrayList();
                List<Product> list = Product.GetList();


                // 1.使用继承 IComparer 接口的函数来进行排序
                arrayList.Sort(new ProductCompare());
                foreach (Product product in arrayList)
                {
                    Console.WriteLine(product.ToString());
                }
                Console.WriteLine("---------------------------");


                // 2.使用继承 IComparer<T> 接口的函数来进行排序
                list.Sort(new ProductCompareT());
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("---------------------------");


                // 3.使用委托来进行排序
                list.Sort(delegate(Product x, Product y)
                {
                    return x.Price.CompareTo(y.Price);
                });
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("---------------------------");

                // 4.使用 Lambda 表达式来排序
                list.Sort((x,y) => x.Price.CompareTo(y.Price));
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("---------------------------");


                // 5.使用扩展方法来进行排序
                foreach (var item in list.OrderBy(p => p.Price))
                {
                    Console.WriteLine(item);
                }
                Console.ReadLine();

            }
        }

        public class Product
        {
            public string Name { get; set; }

            public decimal Price { get; set; }

            public static ArrayList GetArrayList()
            {
                return new ArrayList()
                {
                    new Product {Name = "WindowsPhone", Price = 10m},
                    new Product {Name = "Apple", Price = 20m},
                    new Product {Name = "Android", Price = 5m}
                };
            }

            public static List<Product> GetList()
            {
                return new List<Product>
                {
                    new Product {Name = "WindowsPhone", Price = 10m},
                    new Product {Name = "Apple", Price = 20m},
                    new Product {Name = "Android", Price = 5m}
                };
            }

            public override string ToString()
            {
                return string.Format("{0}--{1}", Name, Price);
            }

        }

        /// <summary>
        /// 使用 IComparer 对 ArrayList 进行排序
        /// 显示实现 ICompare 接口，常用 ArrayList 类型的集合来调用
        /// </summary>
        public class ProductCompare : IComparer
        {
            public int Compare(object x, object y)
            {
                Product first = x as Product;
                Product second = y as Product;
                if (first != null && second != null)
                {
                    return first.Price.CompareTo(second.Price);
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 使用 IComparer<Product> 进行排序
        /// 显示实现 IComparer 接口，常用 List<T> 类型的集合来调用
        /// </summary>
        public class ProductCompareT : IComparer<Product>
        {
            public int Compare(Product x, Product y)
            {
                Product first = x as Product;
                Product second = y as Product;
                if (first != null && second != null)
                {
                    return first.Price.CompareTo(second.Price);
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
