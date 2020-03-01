package com.zlim;

/**
 * 注解的使用：
 * 1.Annotation：jdk5.0之后新增的功能
 *
 *
 * @author zlim
 * @create 2020-03-01 21:17
 */
public class AnnotationTest {
}

@MyAnnotation
class Person{
    private String name;
    private int age;

    public Person() {
    }

    public Person(String name, int age) {
        this.name = name;
        this.age = age;
    }

    public void walk(){
        System.out.println("person walking.");
    }
}























