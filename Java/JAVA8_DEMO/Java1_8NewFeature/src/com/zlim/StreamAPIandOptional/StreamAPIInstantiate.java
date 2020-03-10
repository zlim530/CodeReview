package com.zlim.StreamAPIandOptional;

import com.zlim.lambdaAndmethodref.Employee;
import com.zlim.lambdaAndmethodref.EmployeeData;
import org.junit.Test;

import java.util.Arrays;
import java.util.List;
import java.util.stream.IntStream;
import java.util.stream.Stream;

/**
 * 1. Stream关注的是对数据的运算，与CPU打交道
 *    集合关注的是数据的存储，与内存打交道
 *
 * 2.
 * ①Stream 自己不会存储元素。
 * ②Stream 不会改变源对象。相反，他们会返回一个持有结果的新Stream。
 * ③Stream 操作是延迟执行的。这意味着他们会等到需要结果的时候才执行
 *  即只有当Stream调用了终止操作后，才会执行中间的一系列操作，也即才会执行中间操作链
 *
 * 3.Stream 执行流程
 * ① Stream的实例化
 * ② 一系列的中间操作（过滤、映射、...)
 * ③ 终止操作
 *
 * 4.说明：
 * 4.1 一个中间操作链，对数据源的数据进行处理
 * 4.2 一旦执行终止操作，就执行中间操作链，并产生结果。之后，不会再被使用，与即不能再返回再去调用中间操作链中的方法
 如果还想再对集合中的数据进行操作，只能重新创建一个Stream对象
 *
 *
 *  测试Stream的实例化
 *
 * @author zlim
 * @create 2020-03-10 22:27
 */
public class StreamAPIInstantiate {

    //创建 Stream方式一：通过集合
    @Test
    public void test1(){
        List<Employee> employees = EmployeeData.getEmployees();

        //        default Stream<E> stream() : 返回一个顺序流：是Collection接口中定义的默认方法
        Stream<Employee> stream = employees.stream();

        //        default Stream<E> parallelStream() : 返回一个并行流：类似于多线程的方法
        Stream<Employee> employeeStream = employees.parallelStream();

    }



    //创建 Stream方式二：通过数组（数组也是一种集合）
    @Test
    public void test2(){
        int[] arr = new int[]{1,2,3,4,5,6,};// 数组的静态初始化

        //调用Arrays类的static <T> Stream<T> stream(T[] array): 返回一个流
        IntStream stream = Arrays.stream(arr);

        Employee e1 = new Employee(1001,"Tom");
        Employee e2 = new Employee(1002, "Jerry");
        Employee[] employees = {e1, e2};

        // 如果数组是自定义类型的,那么返回的Stream就会泛型
        Stream<Employee> stream1 = Arrays.stream(employees);

    }



    //创建 Stream方式三：通过Stream的of()
    @Test
    public void test3(){
        // 其实相当于将1, 2, 3, 4, 5, 6看做了一个容器：并且这里的类型是包装类，不是简单的int
        Stream<Integer> integerStream = Stream.of(1, 2, 3, 4, 5, 6 );

    }


    //创建 Stream方式四：创建无限流
    @Test
    public void test4(){
        //      迭代
        //      public static<T> Stream<T> iterate(final T seed, final UnaryOperator<T> f)
        //遍历前10个偶数
        Stream.iterate(0,t -> t + 2).limit(10).forEach(System.out::println);

        System.out.println();
        // 生成
        // public static<T> Stream<T> generate(Supplier<T> s)
        Stream.generate(Math::random).limit(10).forEach(System.out::println);

    }
    

}
