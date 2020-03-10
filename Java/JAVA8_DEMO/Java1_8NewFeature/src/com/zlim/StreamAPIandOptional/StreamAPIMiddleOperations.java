package com.zlim.StreamAPIandOptional;

import com.zlim.lambdaAndmethodref.Employee;
import com.zlim.lambdaAndmethodref.EmployeeData;
import org.junit.Test;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Stream;

/**
 * 测试Stream的中间操作:链式编程
 *
 * @author zlim
 * @create 2020-03-10 22:41
 */
public class StreamAPIMiddleOperations {

    //1-筛选与切片
    @Test
    public void test1(){
        List<Employee> employees = EmployeeData.getEmployees();
        Stream<Employee> stream = employees.stream();

        //练习：查询员工表中薪资大于7000的员工信息
        // forEach(Consumer<T> con)：遍历终止操作，需要传入一个Consumer接口的实现类，这里用了方法引用
        // Consumer接口中需要实现apply()方法，即传入参数但是无返回值，典型的实现方法就是System.out类中的println方法
        // 因为输出语句就是接收任何数据但是没有返回值
        // filter(Predicate p)——接收 Lambda ， 从流中排除某些元素。下面用的是Lambda表达式实现的函数接口
        stream.filter(e -> e.getSalary() > 6000).forEach(System.out::println);
        System.out.println();

        //        limit(n)——截断流，使其元素不超过给定数量。
        // stream.limit(3).forEach(System.out::println);
        // 不可以再使用stream，因为上面stream已经执行了终止操作forEach()，一旦某一个Stream对象执行了终止操作
        // 则此Stream对象就已经关闭了
        employees.stream().limit(3).forEach(System.out::println);// 如果还想继续使用，只能重新生成一个
        System.out.println();

        //        skip(n) —— 跳过元素，返回一个扔掉了前 n 个元素的流。若流中元素不足 n 个，则返回一个空流。与 limit(n) 互补
        employees.stream().skip(3).forEach(System.out::println);
        System.out.println();

        employees.add(new Employee(1010,"刘强东",40,8000));
        employees.add(new Employee(1010,"刘强东",41,8000));
        employees.add(new Employee(1010,"刘强东",40,8000));
        employees.add(new Employee(1010,"刘强东",40,8000));

        //        distinct()——筛选，通过流所生成元素的 hashCode() 和 equals() 去除重复元素
        employees.stream().distinct().forEach(System.out::println);

    }



    //2.映射操作：
    @Test
    public void test2(){
        //map(Function f)——接收一个函数作为参数，将元素转换成其他形式或提取信息，该函数会被应用到每个元素上，并将其映射成一个新的元素。
        // 如果map()中的元素是一个流,则会把这个流当做整体加入原有流中 --> 形如list.add()方法
        // map(Function f)：是对Stream流集合中的每个元素按照指定的规则形成一个映射
        // 例如下面的示例中就是将集合list形成的Stream流对象中的每个元素（"aa", "bb", "cc", "dd"）拿出来并映射成原来元素的大写形式（"AA", "BB", "CC", "DD"）
        List<String> list = Arrays.asList("aa", "bb", "cc", "dd");
        // 将原集合中的元素取出变成大写
        list.stream().map(str -> str.toUpperCase()).forEach(System.out::println);
        System.out.println();

        //        练习1：获取员工姓名长度大于3的员工的姓名。
        List<Employee> employees = EmployeeData.getEmployees();
        Stream<String> nameStream = employees.stream().map(Employee::getName);
        nameStream.filter(str -> str.length() > 3).forEach(System.out::println);
        System.out.println();

        //练习2：map与flatMap的区别:
        // 类似于test3中的list1.add(list2); --> 即集合中还有一个集合 :[  [] ]
        Stream<Stream<Character>> streamStream = list.stream().map(StreamAPIMiddleOperations::fromStringToStream);
        streamStream.forEach(s -> {
            // 类似于两层for循环的遍历  --> 此时就可以隐藏集合中的集合元素,将集合元素中的元素按照正常元素输出
            // 即到达输出形如[a,a,b,b,c,c,d,d]的效果;
            s.forEach(System.out::println);
        });
        System.out.println();

        // 用flatMap去实现上述效果就很简单：
//        flatMap(Function f)——接收一个函数作为参数，将流中的每个值都换成另一个流，然后把所有流连接成一个流。
//        如果flatMap()中的元素是一个流,则它会把流中的所有元素取出,依次加入原有流中 --> 形如list.addAll()方法
        Stream<Character> characterStream = list.stream().flatMap(StreamAPIMiddleOperations::fromStringToStream);
        characterStream.forEach(System.out::println);// 此时一层forEach循环即可
    }



    //将字符串中的多个字符构成的集合转换为对应的Stream的实例(即Stream对象)
    public static Stream<Character> fromStringToStream(String str){
        ArrayList<Character> list = new ArrayList<>();
        for (Character c : str.toCharArray()) {
            list.add(c);
        }
        // 返回Stream对象
        return list.stream();
    }



    @Test
    public void test3(){
        ArrayList list1 = new ArrayList();
        list1.add(1);
        list1.add(2);
        list1.add(3);

        ArrayList list2 = new ArrayList();
        list2.add(4);
        list2.add(5);
        list2.add(6);

        // list1.add(list2);//[1, 2, 3, [4, 5, 6]]
        list1.addAll(list2);//[1, 2, 3, 4, 5, 6]
        System.out.println(list1);

    }



    //3-排序：只要在Java层面涉及到对象排序就去想两个接口：Comparator（定制排序） 与 Comparable（自然排序）
    @Test
    public void test4(){
        //        sorted()——自然排序
        List<Integer> list = Arrays.asList(12, 234, 45, 56, 7, 0, -69);
        list.stream().sorted().forEach(System.out::println);

        //抛异常：ClassCastException ，原因:Employee没有实现Comparable接口
        // List<Employee> employees = EmployeeData.getEmployees();
        // employees.stream().sorted().forEach(System.out::println);

        //        sorted(Comparator com)——定制排序
        List<Employee> employees = EmployeeData.getEmployees();
        employees.stream().sorted( (e1,e2) -> {

            int ageValue = Integer.compare(e1.getAge(), e2.getAge());
            if( ageValue != 0){
                return ageValue;
            }else{
                return -Double.compare(e1.getSalary(),e2.getSalary());
            }

        }).forEach(System.out::println);
    }





}
