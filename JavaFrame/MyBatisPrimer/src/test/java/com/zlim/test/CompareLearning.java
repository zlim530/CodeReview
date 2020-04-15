package com.zlim.test;

/**
 * @author zlim
 * @create 2020-04-15 0:43
 */
public class CompareLearning {

    public static void main(String[] args) {
        // 在JAVA中没有委托类型，所有委托均使用接口代替
        // 1 个引用
        ProductFactroy pizzaFactory = new PizzaFactory();
        ProductFactroy toyCarFactroy = new ToyCarFactory();

        WrapFactory wrapFactory = new WrapFactory();

        Box pizzaBox = wrapFactory.wrapFactory(pizzaFactory);
        Box toyCarBox = wrapFactory.wrapFactory(toyCarFactroy);

        System.out.println(pizzaBox.getProduct().getName());
        System.out.println(toyCarBox.getProduct().getName());

    }

}

interface ProductFactroy{
    Product make();
}

class PizzaFactory implements ProductFactroy{

    @Override
    public Product make() {
        Product product = new Product();
        product.setName("Pizza");
        return product;
    }
}

class ToyCarFactory implements ProductFactroy{

    @Override
    public Product make() {
        Product product = new Product();
        product.setName("ToyCar");
        return product;
    }
}

class Product {
    public String name;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}

class Box{
    public Product product;

    public Product getProduct() {
        return product;
    }

    public void setProduct(Product product) {
        this.product = product;
    }
}

class WrapFactory{

    public Box wrapFactory(ProductFactroy pf){

        Box box = new Box();
        Product product = pf.make();
        box.setProduct(product);
        return box;
    }

}
