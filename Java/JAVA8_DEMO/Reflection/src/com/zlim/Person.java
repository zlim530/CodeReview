package com.zlim;

/**
 * @author zlim
 * @create 2020-03-07 21:11
 */
public class Person {

    private String name;

    public int age;

    public Person(String name, int age) {
        this.name = name;
        this.age = age;
    }

    private Person(String name){
        this.name = name;
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

    private String  showNation(String nation){
        System.out.println("My nation is " + nation);
        return nation;
    }
}
