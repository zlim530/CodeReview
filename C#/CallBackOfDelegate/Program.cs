using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallBackOfDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductFactory productFactory = new ProductFactory();
            WrapFactory wrapFactory = new WrapFactory();

            // Func委托封装了一个无形参但有返回值的方法
            Func<Product> func1 = new Func<Product>(productFactory.MakePizza);
            Func<Product> func2 = new Func<Product>(productFactory.MakeToyCar);

            Logger logger = new Logger();
            // Action委托封装一个方法，该方法只有一个参数并且没有返回值
            Action<Product> log = new Action<Product>(logger.Log);

            Box box1 = wrapFactory.WrapProduct(func1,log);
            Box box2 = wrapFactory.WrapProduct(func2,log);

            Console.WriteLine(box1.Product.Name);
            Console.WriteLine(box2.Product.Name);
        }
    }

    class Logger
    {
        public void Log(Product product)
        {
            // DateTime.UtcNow是不带时区的时间，存储到数据库中应该使用不带时区的时间
            Console.WriteLine("Product '{0}' created at {1}.Price is {2}",product.Name,DateTime.UtcNow,product.Price);
        }
    }
    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }

    class Box
    {
        public Product Product { get; set; }
    }

    class WrapFactory
    {
        // 模板方法：提高代码的复用性
        public Box WrapProduct(Func<Product> getProduct,Action<Product> logCallBack)
        {
            Box box = new Box();
            Product product = getProduct.Invoke();
            // 回调方法一般是根据主调方法的逻辑来觉得是否调用委托的方法：这里的逻辑是只有产品的价格大于等于50的才会被Log记录下来
            if (product.Price >= 50)
            {
                logCallBack(product); 
            }
            box.Product = product;
            return box;
        }
    }

    class ProductFactory
    {
        public Product MakePizza()
        {
            Product product = new Product();
            product.Name = "Pizza";
            product.Price = 12;
            return product;
        }

        public Product MakeToyCar()
        {
            Product product = new Product();
            product.Name = "Toy Car";
            product.Price = 100;
            return product;
        }
    }
}
