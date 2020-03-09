package com.zlim;

import org.junit.Test;

import java.io.PrintStream;
import java.util.Comparator;
import java.util.function.BiPredicate;
import java.util.function.Consumer;
import java.util.function.Function;
import java.util.function.Supplier;

/**
 * 方法引用的使用：即某一个函数式接口的抽象方法已经由某一个类中的方法实现了，此时可以使用方法引用。
 *
 * 1.使用情境：当要传递给Lambda体的操作，已经有实现的方法了，可以使用方法引用！
 *
 * 2.方法引用，本质上就是Lambda表达式，而Lambda表达式作为函数式接口的实例。
 *   所以方法引用，也是函数式接口的实例。
 *
 * 3. 使用格式：  类(或对象) :: 方法名		// 参数列表与小括号都不用写
 *
 * 4. 具体分为如下的三种情况：
 *    情况1     对象 :: 非静态方法
 *    情况2     类 :: 静态方法
 *
 *    情况3     类 :: 非静态方法	// 在面向对象中不可以这么用，但是在方法引用中可以这么用
 *
 * 5. 方法引用使用的要求：要求接口中的抽象方法的形参列表和返回值类型与方法引用的方法的
 *    形参列表和返回值类型相同！（针对于情况1和情况2）
 *
 * @author zlim
 * @create 2020-03-09 23:18
 */
public class MethodRefTest {

    // 情况一：对象 :: 实例方法
    //Consumer中的void accept(T t)：接收一个T类型的变量而没有返回值:  Consumer<T>
    //PrintStream中的void println(T t)：而这个方法恰好实现了accept方法，故可以使用方法引用
    @Test
    public void test1(){
        Consumer<String> con1 = str -> System.out.println(str);
        con1.accept("北京");

        System.out.println("**************************");

        PrintStream out = System.out;
        Consumer<String> con2 = out::println;
        con2.accept("beijing");
    }


    //Supplier中的T get() : Supplier<T>
    //Employee中的String getName()
    @Test
    public void test2(){
        Employee emp = new Employee(1001,"Tom",22,6000);
        Supplier<String> supplier = () -> emp.getName();
        System.out.println(supplier.get());

        System.out.println("**************************");

        Supplier<String> supplier1 = emp::getName;
        System.out.println(supplier1.get());
    }




    // 情况二：类 :: 静态方法
    //Comparator中的int compare(T t1,T t2): Comparator<T>
    //Integer中的int compare(T t1,T t2)
    @Test
    public void test3(){
        Comparator<Integer> com1 = new Comparator<Integer>() {
            @Override
            public int compare(Integer o1, Integer o2) {
                return o1.compareTo(o2);
            }
        };
        System.out.println(com1.compare(12,23));

        System.out.println("*******************************");

        // Comparator<Integer> com2 = (o1,o2) -> o1.compareTo(o2);
        Comparator<Integer> com2 = (o1,o2) -> Integer.compare(o1,o2);
        System.out.println(com2.compare(12,23));

        System.out.println("*******************************");

        Comparator<Integer> com3 = Integer::compare;
        System.out.println(com3.compare(12,23));
    }


    //Function中的R apply(T t): Function<T,R> <形参类型,返回值类型>
    //Math中的Long round(Double d)
    @Test
    public void test4(){

        // 匿名实现类对象写法
        Function<Double,Long> fun = new Function<Double, Long>() {
            @Override
            public Long apply(Double v) {
                return Math.round(v);
            }
        };
        System.out.println(fun.apply(12.3));

        System.out.println("*************************");

        // lambda表达式写法
        Function<Double,Long> fun1 = (d) -> Math.round(d);
        System.out.println(fun1.apply(12.3));

        System.out.println("*************************");

        // 方法引用的写法
        Function<Double,Long> fun2 = Math::round;
        System.out.println(fun2.apply(12.3));
    }


    // 情况三：类 :: 实例方法  (有难度)
    // Comparator中的int comapre(T t1,T t2)
    // String中的int t1.compareTo(t2)：第一个参数作为了compareTo方法的调用者，并且此方法的调用是用类来调用
    @Test
    public void test5(){
        Comparator<String> com = (s1,s2) -> s1.compareTo(s2);
        System.out.println(com.compare("abc","abd"));   // 1

        System.out.println("***********************");

        Comparator<String> com1 = String::compareTo;
        System.out.println(com1.compare("abc","abm")); // -9
    }



    //BiPredicate中的boolean test(T t1, T t2);  BiPredicate<T,T>
    //String中的boolean t1.equals(t2)
    @Test
    public void test6(){
        BiPredicate<String,String> bp = (t1,t2) -> t1.equals(t2);
        System.out.println(bp.test("abc","abc"));// true

        System.out.println("***********************");

        BiPredicate<String,String> bp2 = String::equals;
        System.out.println(bp2.test("abc","abcd"));// false

    }


    // Function中的R apply(T t)：Function<T,R>
    // Employee中的String getName(); 这里的apple方法只有一个参数，而这个参数在getName中作为了方法的调用者
    // 虽然getName是一个实例方法，但是要使用方法所在类进行调用，因为调用getName方法的实例对象说到底还是Employee类的对象
    @Test
    public void test7(){
        Employee employee = new Employee(1002,"jerry",24,5500);
        Function<Employee,String> fun1 = (employee1) -> employee1.getName();
        System.out.println(fun1.apply(employee));

        System.out.println("*******************");

        Function<Employee,String> fun2 = Employee::getName;
        System.out.println(fun2.apply(employee));
    }



}
