package com.zlim;

import org.junit.Test;

import java.util.Arrays;
import java.util.Comparator;

/**
 * Comparable接口 与 Comparator接口
 * 二者使用的对比：
 * Comparable接口的方法一旦实现，保证Comparable接口实现类的对象在任何位置都可以比较大小
 * Comparator接口：属于临时性的比较
 *
 *
 * @author zlim
 * @create 2020-03-01 18:52
 */
public class CompareTest {
    /*
     Compatable接口:自然排序：自然而然的排序
     String 或者 包装类 重写compareTo()方法后，进行了从小到大的排序
     实现 Comparable 的类必须实现 compareTo(Object obj) 方法，两个对象即
     通过 compareTo(Object obj) 方法的返回值来比较大小。如果当前对象this大
     于形参对象obj，则返回正整数，如果当前对象this小于形参对象obj，则返回
     负整数，如果当前对象this等于形参对象obj，则返回零。
     */
    @Test
    public void test1(){
        String[] arr = new String[]{"AA","CC","KK","MM","GG","JJ","DD"};
        Arrays.sort(arr);
        System.out.println(Arrays.toString(arr));
    }

    @Test
    public void test2(){
        // 对于自定义类来说，如果需要排序，我们可以让自定义类实现Comparable接口，重写compareTo(obj)方法。
        // 在compareTo(obj)方法中指明如何排序
        Goods[] arr = new Goods[4];
        arr[0] = new Goods("xiaomi",19);
        arr[1] = new Goods("lianxinag",39);
        arr[2] = new Goods("leise",129);
        arr[3] = new Goods("huipu",29);

        Arrays.sort(arr);
        System.out.println(Arrays.toString(arr));

    }

    /*
    * Comparator接口的使用：定制排序
    *   1.背景：
    * 当元素的类型没有实现java.lang.Comparable 接口而又不方便修改代码，
      了 或者实现了java.lang.Comparable 接口的排序规则不适合当前的操作，那
      用 么可以考虑使用 Comparator  的对象来 排序，强行对多个对象进行整体排
      序的比较。
      * 2.重写compare(Object o1,Object o2)方法，比较o1和o2的大小： 如果方法返
        示 回正整数，则表示o1 大于o2 ；如果返回0 ，表示相等；返回负整数，表示
        o1 小于o2。
    */
    @Test
    public void test3(){
        String[] arr = new String[]{"AA","CC","KK","MM","GG","JJ","DD"};
        Arrays.sort(arr, new Comparator<String>() {
            // 按照字符串从大小的顺序排列
            @Override
            public int compare(String o1, String o2) {
                if (o1 instanceof  String && o2 instanceof String){
                    String s1 = (String) o1;
                    String s2 = (String) o2;
                    return -s1.compareTo(s2);
                }
                // return 0;
                throw new RuntimeException("输入数据类型不一致");
            }
        });
        System.out.println(Arrays.toString(arr));
    }

    @Test
    public void test4(){
        // 对于自定义类来说，如果需要排序，我们可以让自定义类实现Comparable接口，重写compareTo(obj)方法。
        // 在compareTo(obj)方法中指明如何排序
        Goods[] arr = new Goods[4];
        arr[0] = new Goods("xiaomi",19);
        arr[1] = new Goods("lianxinag",39);
        arr[2] = new Goods("leise",129);
        arr[3] = new Goods("huipu",29);

        Arrays.sort(arr, new Comparator<Goods>() {

            @Override
            public int compare(Goods o1, Goods o2) {
                if (o1 instanceof  Goods && o2 instanceof Goods){
                    Goods g1 = (Goods) o1;
                    Goods g2 = (Goods) o2;
                    if (g1.getName().equals(g2.getName())){
                        return -Double.compare(g1.getPrice(),g2.getPrice());
                    }else{
                        return g1.getName().compareTo(g2.getName());
                    }
                }
                // return 0;
                throw new RuntimeException("输入数据类型不一致");
            }
        });
        System.out.println(Arrays.toString(arr));

    }

}




































