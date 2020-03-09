package com.zlim;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;

/**
 *  动态代理的举例
 *  框架中的AOP（Aspect Orient Programming）思想：即面向切面编程
 *
 * @author zlim
 * @create 2020-03-09 21:34
 */

interface Humam{
    String getBelief();

    void eat(String food);
}

//被代理类
class SuperMan implements Humam{

    @Override
    public String getBelief() {
        return "I believe I can fly.";
    }

    @Override
    public void eat(String food) {
        System.out.println("I like to eat " + food);
    }
}


// 动态代理与AOP结合形成动态方法
class HumanUtil{

    public void  method1(){
        System.out.println("*********这是通用方法1************");
    }

    public void method2(){
        System.out.println("*********这是通用方法2************");
    }

}



/*
要想实现动态代理，需要解决的问题？
问题一：如何根据加载到内存中的被代理类，动态的创建一个代理类及其对象。
问题二：当通过代理类的对象调用方法a时，如何动态的去调用被代理类中的同名方法a。


 */
class ProxyFactory{

    //调用此方法，返回一个代理类的对象。解决问题一
    public static Object getProxyInstance(Object obj){//obj:被代理类的对象

        MyInvocationHandler handler = new MyInvocationHandler();

        handler.bind(obj);

        return Proxy.newProxyInstance(obj.getClass().getClassLoader(),obj.getClass().getInterfaces(),handler);

    }

}

// 解决问题二
class MyInvocationHandler implements InvocationHandler{

    private Object obj;//需要使用被代理类的对象进行赋值:声明被代理类对象

    // 给被代理类对象赋值
    public void  bind(Object obj){
        this.obj = obj;
    }

    //当我们通过代理类的对象，调用方法a时，就会自动的调用如下的方法：也即调用invoke()方法
    //将被代理类要执行的方法a的功能就声明在invoke()中
    @Override
    public Object invoke(Object proxy, Method method, Object[] args) throws Throwable {

        HumanUtil humanUtil = new HumanUtil();
        humanUtil.method1();

        //method:即为代理类对象调用的方法，此方法也就作为了被代理类对象要调用的方法
        //obj:被代理类的对象，args为被代理类调用方法所需要的参数
        Object invokeResult = method.invoke(obj, args);

        humanUtil.method2();

        //上述方法的返回值就作为当前类中的invoke()的返回值。
        return invokeResult;
    }
}




public class DynamicProxyTest {

    public static void main(String[] args) {
        SuperMan superMan = new SuperMan();
        //proxyInstance:代理类的对象
        Humam proxyInstance = (Humam) ProxyFactory.getProxyInstance(superMan);
        //当通过代理类对象调用方法时，会自动的调用被代理类中同名的方法
        String belief = proxyInstance.getBelief();  //通过代理类去调用被代理类中的方法
        System.out.println("belief = " + belief);
        proxyInstance.eat("火锅。");

        System.out.println("-----------------------------------");

        NikeClothFactory nikeClothFactory = new NikeClothFactory();
        // 传入被代理类对象：动态的帮你生成代理类对象
        ClothFactory proxyInstance1 = (ClothFactory) ProxyFactory.getProxyInstance(nikeClothFactory);
        proxyInstance1.produceCloth();

    }


}
