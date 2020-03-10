package com.zlim.StreamAPIandOptional;

import com.zlim.lambdaAndmethodref.Employee;
import com.zlim.lambdaAndmethodref.EmployeeData;
import org.junit.Test;

import java.util.Arrays;
import java.util.List;
import java.util.Optional;
import java.util.Set;
import java.util.stream.Collectors;
import java.util.stream.Stream;

/**
 * 测试Stream的终止操作
 *
 * @author zlim
 * @create 2020-03-10 23:04
 */
public class StreamAPIEndOperations {

    //1-匹配与查找
    @Test
    public void test1(){
        List<Employee> employees = EmployeeData.getEmployees();

        //        allMatch(Predicate p)——检查是否匹配所有元素：所有元素都匹配都为true才会返回true
//          练习：是否所有的员工的年龄都大于18
        boolean result = employees.stream().allMatch(e -> e.getAge() > 18);
        System.out.println("result = " + result);// false

        //        anyMatch(Predicate p)——检查是否至少匹配一个元素：只要有一个元素为true返回值就为true
        //         练习：是否存在员工的工资大于 10000
        boolean result2 = employees.stream().anyMatch(e -> e.getSalary() > 10000);
        System.out.println("result2 = " + result2);

        //        noneMatch(Predicate p)——检查是否没有匹配的元素：如果有匹配的元素，则返回false
        //          练习：是否存在员工姓“雷”
        boolean result3 = employees.stream().noneMatch(e -> e.getName().startsWith("雷"));
        System.out.println("result3 = " + result3);

        //        findFirst()——返回第一个元素
        Optional<Employee> first = employees.stream().findFirst();
        System.out.println("first = " + first);

        //        findAny()——返回当前流中的任意元素
        //        parallelStream()：获取一个并行流，stream()：获取一个串行流
        Optional<Employee> any = employees.parallelStream().findAny();
        System.out.println("any = " + any);

    }
    
    
    
    @Test
    public void test2(){
        List<Employee> employees = EmployeeData.getEmployees();
        // count——返回流中元素的总个数：返回值类型是long
        long count = employees.stream().filter(e -> e.getSalary() > 5500).count();
        System.out.println("count = " + count);

        //        max(Comparator c)——返回流中最大值
        //        练习：返回最高的工资
        Stream<Double> salaryStream = employees.stream().map(Employee::getSalary);
        Optional<Double> max = salaryStream.max(Double::compare);
        System.out.println("max = " + max);

        //        min(Comparator c)——返回流中最小值
        //        练习：返回最低工资的员工
        Optional<Employee> min = employees.stream().min((e1, e2) -> Double.compare(e1.getSalary(), e2.getSalary()));
        System.out.println("min = " + min);

        //forEach(Consumer c)——内部迭代：通过Stream对象调用：这是Stream流中的终止操作
        // 内部迭代(使用 Collection 接口需要用户去做迭代，称为外部迭代。相反，Stream API 使用内部迭代——它帮你把迭代做了)
        employees.stream().forEach(System.out::println);

        System.out.println();
        //使用集合的遍历操作：还可以利用集合的forEach直接去调用：这是集合中的一个普通（默认）方法
        employees.forEach(System.out::println);
    }



    //2-归约
    @Test
    public void test3(){
        //        reduce(T identity, BinaryOperator)——可以将流中元素反复结合起来，得到一个值。返回 （一个）T
        //        BinaryOperator<T> extends BiFunction<T,T,T>：接收两个类型相同形参，并最后返回同类型的值
        //        练习1：计算1-10的自然数的和
        List<Integer> list = Arrays.asList(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        // reduce(T identity, BinaryOperator)：自动就有累加的效果：所以称为归约
        Integer sum = list.stream().reduce(0, Integer::sum);// 参数1表示初始值
        System.out.println("sum = " + sum);

        //        reduce(BinaryOperator<T,T,T>) ——可以将流中元素反复结合起来，得到一个值。返回 Optional<T>：不需要初始值
        //        练习2：计算公司所有员工工资的总和
        List<Employee> employees = EmployeeData.getEmployees();
        Stream<Double> salaryStream = employees.stream().map(Employee::getSalary);
        // Optional<Double> sumSalary = salaryStream.reduce((d1, d2) -> d1 + d2);// Lambda表达式写法
        Optional<Double> sumSalary = salaryStream.reduce(Double::sum);// 方法引用写法
        System.out.println("sumSalary = " + sumSalary);// 此时打印的是一个Optional类型对象
        // T get(): 如果调用对象包含值，返回该值，否则抛异常
        // System.out.println(sumSalary.get());// 此时打印的是一个double类型对象
    }




    //3-收集
    @Test
    public void test4(){
        //  collect(Collector c)——将流转换为其他形式。接收一个 Collector接口的实现，用于给Stream中元素做汇总的方法
        //  Collectors 实用类提供了很多静态方法，可以方便地创建常见收集器实例
        //  练习1：查找工资大于6000的员工，结果返回为一个List或Set
        List<Employee> employees = EmployeeData.getEmployees();
        // 将Stream流对象使用collect(Collector c)转化为list
        List<Employee> emloyeeList = employees.stream().filter(e -> e.getSalary() > 6000).collect(Collectors.toList());
        emloyeeList.forEach(System.out::println);
        System.out.println();

        // 将Stream流对象使用collect(Collector c)转化为set：注意这里需要重新造流：即重新使用待处理集合对象的stream()方法创建Stream流对象
        Set<Employee> emloyeeSet = employees.stream().filter(e -> e.getSalary() > 6000).collect(Collectors.toSet());
        emloyeeSet.forEach(System.out::println);
    }









    
}
