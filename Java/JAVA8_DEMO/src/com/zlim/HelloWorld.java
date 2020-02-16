package com.zlim;

public class HelloWorld {

    public static void main(String[] args) {
        System.out.println("Hello,world!");
        Student stu = new Student();
        stu.setAge(22);
        stu.setName("ZLim");

        Student stu2 = new Student(22,"YG");
        System.out.println("Name:" + stu.getName() + " Age:" + stu.getAge());
        System.out.println("Name:" + stu2.getName() + " Age:" + stu2.getAge());
    }
}
