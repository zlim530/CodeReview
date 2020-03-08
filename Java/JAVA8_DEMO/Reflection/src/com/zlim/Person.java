package com.zlim;

import javax.swing.*;

/**
 * @author zlim
 * @create 2020-03-07 21:11
 */
@MyAnnotation("PersonClassAnnotatin")
public class Person  extends Creature<String> implements Comparable<String>{

    private String name;

    public int age;

    public Person(String name, int age) {
        this.name = name;
        this.age = age;
    }

    private Person(String name){
        this.name = name;
    }

    public Person() {
    }

    @Override
    public String toString() {
        return "Person{" +
                "name='" + name + '\'' +
                ", age=" + age +
                '}';
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public void  show(){
        System.out.println("Hi,there.");
    }

    @MyAnnotation
    private String  showNation(String nation){
        System.out.println("My nation is " + nation);
        return nation;
    }

    @Override
    public int compareTo(String o) {
        return 0;
    }

    private static void showDesc(){
        System.out.println("I am a cute person.");
    }


}
