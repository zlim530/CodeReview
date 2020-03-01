package com.zlim;

/**
 * @author zlim
 * @create 2020-03-01 18:59
 */
public class Goods  implements Comparable{

    private String name;

    private double price;

    public Goods() {
    }

    public Goods(String name, double price) {
        this.name = name;
        this.price = price;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public double getPrice() {
        return price;
    }

    @Override
    public String toString() {
        return "Goods{" +
                "name='" + name + '\'' +
                ", price=" + price +
                '}';
    }

    public void setPrice(double price) {
        this.price = price;
    }

    @Override
    public int compareTo(Object o) {
        if (o instanceof Goods){
            Goods goods = (Goods) o;
            if (this.price > goods.price){
                return 1;
            }else if (this.price < goods.price){
                return -1;
            }else {
                return 0;
            }
        }
        // 因为抛出的是运行时异常，故在方法调用时可以不处理
        throw  new  RuntimeException("传入数据类型不一致");
    }
}
