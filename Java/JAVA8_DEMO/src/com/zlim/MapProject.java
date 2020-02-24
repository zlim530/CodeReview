package com.zlim;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

public class MapProject {
    public static void main(String[] args) {
        Map<Integer,Student> map = new HashMap<>();
        Student s1 = new Student(23,"张三");
        Student s2 = new Student(24,"李四");
        Student s3 = new Student(23,"张三");
        map.put(1,s1);
        map.put(2,s2);
        map.put(3,s3);
//        Student stu = map.get(3);
//        System.out.println("key: " + 3 + ", value: " + stu);
//        System.out.println("-------------------------------");
//        Student sTest = new Student(23,"张三");
//        System.out.println(sTest.equals(s3));

        Set<Integer> keys = map.keySet();
        Iterator<Integer> it = keys.iterator();
        while (it.hasNext()){
            Integer key = it.next();
            Student s = map.get(key);
            System.out.println("key = " + key + ",value = "+ s);
        }

        Set<Integer> keyss = map.keySet();
        for (Integer key : keyss) {
            Student value = map.get(key);
            System.out.println("key -> " + key + ", value -> " + value);
        }
    }
}
