using System;
using System.Collections.Generic;
using System.Linq;

namespace DailyLocalCode.SomeTryExample
{
    public class Product
    {
        public string Name { get; set; }
        public int Code { get; set; }
    }

    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            // 比较两个对象的内存地址是否一致
            if (object.ReferenceEquals(x, y)) 
                return true;
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
                return false;
            return x.Code == y.Code && x.Name == y.Name;
        }

        public int GetHashCode(Product product)
        {
            if (object.ReferenceEquals(product, null))
                return 0;
            int hashProductName = product.Name == null ? 0 : product.Name.GetHashCode();
            int hashProductCode = product.Code.GetHashCode();
            return hashProductName ^ hashProductCode;
        }

        static void Main0(string[] args)
        {
            Product[] store1 = { new Product { Name = "apple", Code = 9},
                                    new Product { Name = "orange", Code = 4} };
            Product[] store2 = { new Product { Name = "apple", Code = 9},
                                    new Product { Name = "lemon", Code = 12} };

            /*
            IEnumerable<Product> IEnumerable<Product>.Intersect<Product>(IEnumerable<Product> second, IEqualityComparer<Product> comparer)
            通过使用指定的 IEqualityComparer<in T> 对值进行比较，生成两个序列的交集
            返回结果：
                包含组成两个序列交集的元素的序列
            异常：
                ArgumentNullException
            */
            IEnumerable<Product> duplicates = store1.Intersect(store2, new ProductComparer());

            foreach (var product in duplicates)
            {
                Console.WriteLine($"{product.Name} {product.Code}");
                /*
                    输出结果：apple 9
                */
            }

        }

    }
}
