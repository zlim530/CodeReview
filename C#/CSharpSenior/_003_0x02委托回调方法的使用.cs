using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpSenior {
    class _003_0x02委托回调方法的使用 {

        static void Main() {
            var productFactory = new ProductFactory();

            Func<Product> funcMakePizza = new Func<Product>(productFactory.MakePizza);
            Func<Product> funcMakeToyCar = new Func<Product>(productFactory.MakeToyCar);

            var wrapFactory = new WrapFactory();
            var logger = new Logger();

            Action<Product> logAction = new Action<Product>(logger.Log);

            Box box1 = wrapFactory.WrapProduct(funcMakePizza, logAction);
            Box box2 = wrapFactory.WrapProduct(funcMakeToyCar, logAction);

            Console.WriteLine(box1.Product.Price);
            Console.WriteLine("Hello,World!");
            Console.WriteLine(box2.Product.Price);
        }

    }

    class Product  {
        public string Name { get; set; }

        public double Price { get; set; }

    }

    class Logger {
        public void Log(Product product) {
            Console.WriteLine("Product '{0}' created at {1}.Price is {2}",product.Name,DateTime.UtcNow,product.Price);
        }
    }

    class Box {
        public Product Product { get; set; }
    }

    class WrapFactory {

        public Box WrapProduct(Func<Product> getProduct,Action<Product> logCallBack) {
            var box = new Box();
            Product product = getProduct.Invoke();

            // 回调方法
            if (product.Price >= 50) {
                logCallBack(product);
            }

            box.Product = product;
            return box;
        }
    }

    class ProductFactory {
        public Product MakePizza() {
            var product = new Product();
            product.Name = "Pizza";
            product.Price = 12;
            return product;
        }

        public Product MakeToyCar() {
            var product = new Product();
            product.Name = "ToyCar";
            product.Price = 100;
            return product;
        }
    }
}
