package com.zlim.StreamAPIandOptional;

import org.junit.Test;

import java.util.Optional;

/**
 * Optional类：为了在程序中避免出现空指针异常而创建的。
 *
 * Optional<T> 类(java.util.Optional) 是一个容器类，它可以保存类型T的值，代表
 * 这个值存在。或者仅仅保存null，表示这个值不存在。原来用 null 表示一个值不存在，
 * 现在 Optional 可以更好的表达这个概念。并且可以避免空指针异常。
 * Optional类的Javadoc描述如下：这是一个可以为null的容器对象。
 * 如果值存在则isPresent()方法会返回true，调用get()方法会返回该对象。
 * 类似于基本数据类型与它对应包装类之间的关系，如int与Integer，Integer就可以看做是一个装整型数据的容器，
 * 原来我们对int数据的操作，就转换为对Integer的操作
 *
 * 常用的方法：ofNullable(T t)
 *            orElse(T t)
 * @author zlim
 * @create 2020-03-10 23:57
 */
public class OptionalTest {

    /*
    Optional提供很多有用的方法，这样我们就不用显式进行空值检测：
    Optional.of(T t) : 创建一个 Optional 实例，t必须非空；
    Optional.empty() : 创建一个空的 Optional 实例
    Optional.ofNullable(T t)：t可以为null

     */
    @Test
    public void test1(){
        Girl girl = new Girl();
        //        girl = null;
        //of(T t):保证t是非空的：否则会报NullPointerException
        Optional<Girl> girl1 = Optional.of(girl);

    }

    @Test
    public void test2(){
        Girl girl = new Girl();
        girl = null;
        //ofNullable(T t)：t可以为null
        Optional<Girl> girl1 = Optional.ofNullable(girl);
        System.out.println("girl1 = " + girl1);//girl1 = Optional.empty

        //orElse(T t1):如果单前的Optional内部封装的t是非空的，则返回内部的t.
        //如果内部的t是空的，则返回orElse()方法中的参数t1.
        // 实现的效果是：保证了orElse()方法的返回值一定是非空的
        Girl girl2 = girl1.orElse(new Girl("GiGi"));
        System.out.println("girl2 = " + girl2);// girl2 = Girl{name='GiGi'}
    }


    public String getGirlName(Boy boy){
        return boy.getGirl().getName();
    }


    @Test
    public void test3(){
        Boy boy = new Boy();
        boy = null;// 将对象赋值为null即赋值为空
        // 如果boy没有赋值为null，也会报空指针异常，因为再新建Boy对象并没有传入Girl对象
        String girlName = getGirlName(boy);//java.lang.NullPointerException
        // 故在调用getGirl()方法时就会发生空指针异常
        System.out.println("girlName = " + girlName);
    }


    //优化以后的getGirlName():手动写判空异常：在Optional类之前
    public String getGirlName1(Boy boy){
        if( boy != null){
            Girl girl = boy.getGirl();
            if( girl != null){
                return girl.getName();
            }
        }
        return null;
    }


    @Test
    public void test4(){
        Boy boy = new Boy();
        boy = null;
        String girlName = getGirlName1(boy);
        System.out.println("girlName = " + girlName);// girlName = null
    }


    //使用Optional类的getGirlName():
    public String getGirlName2(Boy boy){
        Optional<Boy> boy1 = Optional.ofNullable(boy);
        //获取Optional 容器的对象：T orElse(T other)  ：如果有值则将其返回，否则（如果没有值也即如果为空）返回指定的other对象。
        //此时的boy1一定非空
        Boy boy2 = boy1.orElse(new Boy(new Girl("Bili")));

        Girl girl = boy2.getGirl();// 这里的girl也有可能是null

        Optional<Girl> girlOptional = Optional.ofNullable(girl);
        //girl1一定非空
        Girl girl1 = girlOptional.orElse(new Girl("Nance"));

        return girl1.getName();
    }


    @Test
    public void test5(){
        Boy boy = null;//girlName = Bili
        boy = new Boy();// girlName = Nance
        boy = new Boy(new Girl("Princess King"));// girlName = Princess King
        String girlName = getGirlName2(boy);
        System.out.println("girlName = " + girlName);
    }

}
