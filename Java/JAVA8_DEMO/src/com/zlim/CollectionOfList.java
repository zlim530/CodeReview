package com.zlim;

import java.util.*;

public class CollectionOfList {
    public static void main(String[] args) {
        List<Student> list = new ArrayList<>();
        Student s1 = new Student(23,"张三");
        Student s2 = new Student(24,"李四");
        Student s3 = new Student(23,"张三");
        list.add(s1);
        list.add(s2);
        list.add(s3);
        System.out.println(list);
//        Collections.sort(list);
//        Collections.reverse(list);
//        Collections.max(list);
//        Collections.shuffle(list);
//        System.out.println(list);
    }


    public static void main1(String[] args) {
        List list = new ArrayList();
        list.add("a");
        list.add("b");
        list.add("c");
        list.add("d");

//        for (Object obj : list) {
//            Integer ii = (Integer) obj;
//            System.out.println(ii);
//        }

//        Iterator iterator = list.iterator();
//        while (iterator.hasNext()){
//            Object obj = iterator.next();
//            String s = (String) obj;
//            if ( "b".equals(s)) {
//                list.add("java");// ConcurrentModificationException:直接使用集合在迭代器遍历时添加元素会抛出并发修改异常
//            }
//            System.out.println(s);
//        }

//      解决方法：使用列表迭代器
        ListIterator listIterator = list.listIterator();
        while (listIterator.hasNext()){
            Object obj = listIterator.next();
            String s = (String) obj;
            if ("b".equals(s)){
               listIterator.add("java");// 使用列表迭代器的方法添加才可以
            }
            System.out.println(s);
//            a
//            b
//            c
//            d
        }
        System.out.println("new list:");
//      打印新集合中的值
        System.out.println(list);// [a, b, java, c, d]

    }
}
