package com.zlim.lambdaAndmethodref;

import org.junit.Test;

import java.util.Arrays;
import java.util.function.BiFunction;
import java.util.function.Function;
import java.util.function.Supplier;

/**
 * 一、构造器引用
 *      和方法引用类似，函数式接口的抽象方法的形参列表和构造器的形参列表一致。
 *      抽象方法的返回值类型即为构造器所属的类的类型
 *      抽象方法中的返回值T就是我们要创建的这个类类型的对象
 *
 * 二、数组引用:格式：数据元素类型[]::new
 *     大家可以把数组看做是一个特殊的类，则写法与构造器引用一致。
 *
 * @author zlim
 * @create 2020-03-09 23:46
 */
public class ConstructorRefTest {
    //构造器引用
    //Supplier中的T get()：没有形参，但是有返回值
    //Employee的空参构造器：Employee()：没有形参，但是有返回值
    @Test
    public void test1(){
        Supplier<Employee> sup = new Supplier<Employee>() {
            @Override
            public Employee get() {
                return new Employee();
            }
        };

        System.out.println("***************");

        // lambda表达式：省略return 关键字 与{}大括号：仅留下小括号与方法体
        Supplier<Employee> sup1 = () -> new Employee();
        System.out.println(sup1.get());

        System.out.println("******************");

        // 构造器引用：调用的是空参构造器
        Supplier<Employee> sup2 = Employee::new;
        System.out.println(sup2.get());

    }



    //Function中的R apply(T t)：有一个形参并且有返回值的apply方法：并且形参的类型与返回值的类型不一样
    @Test
    public void test2(){
        Function<Integer,Employee> fun = (id) -> new Employee(id);
        System.out.println(fun.apply(1001));

        System.out.println("*****************");

        // 此时调用的是Employee中的public Employee(int id)这个构造器
        Function<Integer,Employee> fun2 = Employee::new;
        System.out.println(fun2.apply(1002));
    }




    //BiFunction中的R apply(T t,U u) : BiFunction<T,U,R> : <形参类型1,形参类型2,返回值类型>
    @Test
    public void test3(){
        BiFunction<Integer,String,Employee> bf = (id,name) -> new Employee(id,name);
        System.out.println(bf.apply(1001,"Tom"));

        System.out.println("*******************");

        BiFunction<Integer,String,Employee> bf2 = Employee::new;
        System.out.println(bf2.apply(1002,"jerry"));

    }



    //数组引用
    //Function中的R apply(T t): Function<T,R>
    @Test
    public void test4(){
        Function<Integer,String[]> fun = (length) -> new String[length];
        System.out.println(Arrays.toString(fun.apply(5)));// [null, null, null, null, null]

        System.out.println("*******************");

        Function<Integer,String[]> fun2 = String[]::new;
        System.out.println(Arrays.toString(fun2.apply(10)));// [null, null, null, null, null, null, null, null, null, null]
    }


}
