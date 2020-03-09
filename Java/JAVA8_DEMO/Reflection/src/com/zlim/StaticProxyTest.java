package com.zlim;

/**
 *
 * 静态代理举例：有明显的代理类声明实现
 *
 * 特点：代理类和被代理类在编译期间，就确定下来了。
 *
 * @author zlim
 * @create 2020-03-09 21:27
 */

interface ClothFactory{// 被代理类与代理类需要实现同一套接口
    void produceCloth();
}

//代理类
class ProxyClothFactory implements ClothFactory{

    private ClothFactory factory;//用被代理类对象进行实例化

    public ProxyClothFactory(ClothFactory factory) {
        this.factory = factory;
    }

    @Override
    public void produceCloth() {
        System.out.println("代理工厂做一些准备的工作。");

        // 这一步实际上是调用被代理类中的同名方法：与即接口中共同实现的方法
        factory.produceCloth();

        System.out.println("代理工厂做一些后续的收尾工作。");

    }
}


//被代理类
class NikeClothFactory implements ClothFactory{

    // 代理类实际上也是通过创建被代理类对象然后调用它的方法进行调用
    @Override
    public void produceCloth() {
        System.out.println("Nike工厂生产了一批运动服。");
    }
}


public class StaticProxyTest {

    public static void main(String[] args) {

        //创建被代理类的对象：父接口 = 实现类：多态
        NikeClothFactory nikeClothFactory = new NikeClothFactory();
        //创建代理类的对象
        ProxyClothFactory proxyClothFactory = new ProxyClothFactory(nikeClothFactory);
        proxyClothFactory.produceCloth();

    }


}
