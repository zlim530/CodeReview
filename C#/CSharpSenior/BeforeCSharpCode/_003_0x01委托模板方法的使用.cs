using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpSenior {
    class _003_0x01委托模板方法的使用 {
        static void Main() {

            var productFactory = new ProductFactory();

            Func<Product> funMakePizza = new Func<Product>(productFactory.MakePizza);
            Func<Product> funMakeToyCar = new Func<Product>(productFactory.MakeToyCar);

            var wrapFactroy = new WrapFactory();
            Box box1 = wrapFactroy.WrapProduct(funMakeToyCar);
            Box box2 = wrapFactroy.WrapProduct(funMakePizza);

            Console.WriteLine(box1.Product.Name);
            Console.WriteLine(box2.Product.Name);
        }
    }

    class Product {
        public string Name { get; set; }
    }

    class Box {
        public Product Product { get; set; }
    }

    class WrapFactory {

        // 模板方法
        public Box WrapProduct(Func<Product> getProduct) {
            var box = new Box();
            Product product = getProduct.Invoke();
            box.Product = product;
            return box;
        }
    }

    class ProductFactory {
        public Product MakePizza() {
            var product = new Product();
            product.Name = "Pizza";
            return product;
        }

        public Product MakeToyCar() {
            var product = new Product();
            product.Name = "ToyCar";
            return product;
        }
    }
}
