package com.zlim;

import org.junit.Test;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.function.Consumer;

/**
 * Lambda表达式的使用:要想用lambda表达式就一定要依赖于函数式接口:相当于函数式接口的实例就是lambda表达式
 *
 * 1.举例： (o1,o2) -> Integer.compare(o1,o2);
 * 2.格式： (形参列表) -> {方法体};
 *      -> :lambda操作符 或 箭头操作符
 *      ->左边：lambda形参列表 （其实就是接口中的抽象方法的形参列表）
 *      ->右边：lambda体 （其实就是重写的抽象方法的方法体）
 *
 * 3. Lambda表达式的使用：（分为6种情况介绍）
 *
 *    总结：
 *    ->左边：lambda形参列表的参数类型可以省略(类型推断)；如果lambda形参列表只有一个参数，其一对()也可以省略
 *    ->右边：lambda体应该使用一对{}包裹；如果lambda体只有一条执行语句（可能是return语句），省略这一对{}和return关键字
 *
 * 4.Lambda表达式的本质：作为函数式接口的实例
 *      (这一点与其他语言不太一样,在Python在lambda本质是一个函数,但是在Java中万事万物届对象,故lambda表达式也是一个
 *       对象,并且是一个接口实现类的对象)
 *
 * 5. 什么是函数式接口:如果一个接口中，只声明了一个抽象方法，则此接口就称为函数式接口。我们可以在一个接口上使用 @FunctionalInterface 注解，
 *   这样做可以检查它是否是一个函数式接口。

         1.Java从诞生日起就是一直倡导“一切皆对象”，在Java里面面向对象(OOP)
         编程是一切。但是随着python、scala等语言的兴起和新技术的挑战，Java不
         得不做出调整以便支持更加广泛的技术要求，也即java不但可以支持OOP还
         可以支持OOF（面向函数编程）
         2.在函数式编程语言当中，函数被当做一等公民对待。在将函数作为一等公民的
         编程语言中，Lambda表达式的类型是函数。但是在Java8中，有所不同。在
         Java8中，Lambda表达式是对象，而不是函数，它们必须依附于一类特别的
         对象类型——函数式接口。
         3.简单的说，在Java8中，Lambda表达式就是一个函数式接口的实例。这就是
         Lambda表达式和函数式接口的关系。也就是说，只要一个对象是函数式接口
         的实例，那么该对象就可以用Lambda表达式来表示。
         4.所以以前用匿名实现类表示的现在都可以用Lambda表达式来写
 *
 * 6. 所以以前用匿名实现类表示的现在都可以用Lambda表达式来写。
 *
 * @author zlim
 * @create 2020-03-09 21:53
 */
public class LambdaTest {

    //语法格式一：无参，无返回值
    @Test
    public void test1(){
        Runnable runnable = new Runnable() {
            @Override
            public void run() {
                System.out.println("我去过北京天安门。");
            }
        };

        runnable.run();

        System.out.println("****************************");

        Runnable run = () -> System.out.println("但是我没有去过故宫。");
        run.run();
    }



    //语法格式二：Lambda 需要一个参数，但是没有返回值。
    @Test
    public void test2(){
        Consumer<String> con = new Consumer<String>() {
            @Override
            public void accept(String s) {
                System.out.println(s);
            }
        };

        con.accept("what's the difference between liar and truth?");

        System.out.println("************with (parametertype  parameter) and {};**************************");

        Consumer<String> con1 = (String s) -> {
                System.out.println(s);
            };
        con1.accept("while,the liar is the listener is believed,the truth is the teller is believed.");

    }



    //语法格式三：数据类型可以省略，因为可由编译器推断得出，称为“类型推断”
    @Test
    public void test3(){
        Consumer<String> con = new Consumer<String>() {
            @Override
            public void accept(String s) {
                System.out.println(s);
            }
        };

        con.accept("what's the difference between liar and truth?");

        System.out.println("**************with (parameter) and {};************************");

        Consumer<String> con1 = (s) -> {
            System.out.println(s);
        };
        con1.accept("while,the liar is the listener is believed,the truth is the teller is believed.");
    }

    @Test
    public void test4(){
        ArrayList<String> list = new ArrayList<>();//类型推断

        // int[] arr = new int[]{1,2,3};// 原来的写法:数组的静态初始化
        int[] arr = {1,2,3,};//类型推断
    }


    //语法格式四：Lambda 若只需要一个参数时，参数的小括号可以省略
    @Test
    public void test5(){
        Consumer<String> con = new Consumer<String>() {
            @Override
            public void accept(String s) {
                System.out.println(s);
            }
        };

        con.accept("what's the difference between liar and truth?");

        System.out.println("***************with parameter and {};***********************");

        Consumer<String> con1 = s -> {
            System.out.println(s);
        };
        con1.accept("while,the liar is the listener is believed,the truth is the teller is believed.");
    }


    //语法格式五：Lambda 需要两个或以上的参数，多条执行语句，并且可以有返回值
    @Test
    public void test6(){
        Comparator<Integer> com1 = new Comparator<Integer>() {
            @Override
            public int compare(Integer o1,Integer o2) {
                System.out.println("o1 = " + o1 + ", o2 = " + o2);
                return o1.compareTo(o2);
            }
        };

        System.out.println(com1.compare(12,34));

        System.out.println("*************************");

        Comparator<Integer> com2 = ( o1, o2) -> {
                System.out.println("o1 = " + o1 + ", o2 = " + o2);
                return o1.compareTo(o2);
            };

        System.out.println(com2.compare(90,4));

    }


    //语法格式六：当 Lambda 体只有一条语句时，return 与大括号若有，都可以省略
    // 同时省略return 关键字 与{} 大括号
    @Test
    public void test7(){
        Comparator<Integer> com1 = new Comparator<Integer>() {
            @Override
            public int compare(Integer o1, Integer o2) {
                return o1.compareTo(o2);
            }
        };

        System.out.println(com1.compare(12,6));

        System.out.println("*************ignore return and {}*************");

        Comparator<Integer> com2 = ( o1,  o2) -> o1.compareTo(o2);

        System.out.println(com2.compare(12,34));
    }


    // 没有return关键字，仅省略{}大括号
    @Test
    public void test8(){
        Consumer<String> con1 = s -> {
            System.out.println(s);
        };
        con1.accept("while,the liar is the listener is believed,the truth is the teller is believed.");

        System.out.println("***********************");

        Consumer<String> con2 = s -> System.out.println( s);
        con2.accept("while,the liar is the listener is believed,the truth is the teller is believed.");

    }


}




















