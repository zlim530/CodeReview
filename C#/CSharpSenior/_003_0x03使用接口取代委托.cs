using System;

namespace CSharpSenior {
    class _003_0x03使用接口取代委托 {

        static void Main(string[] args) {
            IProductFactory pizzaFactory = new PizzaFactroy();

            IProductFactory toyCarFactory = new ToyCarFactory();

            var wrapFactory = new WrapFactory();

            Box pizzaBox = wrapFactory.WrapProduct(pizzaFactory);
            Box toyCarBox = wrapFactory.WrapProduct(toyCarFactory);

            Console.WriteLine(pizzaBox.Product.Name);
            Console.WriteLine(toyCarBox.Product.Name);
        }
    }

    interface IProductFactory {
        Product Make();
    }

    class PizzaFactroy : IProductFactory {
        public Product Make() {
            var product = new Product();
            product.Name = "Pizza";
            return  product;
        }
    }

    class ToyCarFactory : IProductFactory {
        public Product Make() {

            var product = new Product();
            product.Name = "ToyCar";
            return product;
        }
    }

    class Product {
        public string Name { get; set; }
    }

    class Box {
        public Product Product { get; set; }
    }
    class WrapFactory {

        public Box WrapProduct(IProductFactory productFactory) {

            var box = new Box();
            Product product = productFactory.Make();
            box.Product = product;
            return box;
        }
    }

}
