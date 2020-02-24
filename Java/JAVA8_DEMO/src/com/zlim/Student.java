package com.zlim;

import java.util.Objects;

public class Student {
    private int age;
    private String name;

    public Student() {
    }

    public Student(int age, String name) {
        this.age = age;
        this.name = name;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    @Override
    public String toString() {
        return "Student{" +
                "age=" + age +
                ", name='" + name + '\'' +
                '}';
    }

    @Override
    public boolean equals(Object o) {
        // 比较两个对象的地址值是否相同，提高效率
        if (this == o) return true;
        // 判断要比较的两个对象是否是同一个类型的对象，即拿到Student的字节码文件与o的字节码文件进行比较
        // getClass()方法：用来获取类的字节码文件，同一个类字节码文件只有一份
        // 这里其实省略了this关键字，即：this.getClass() != o.getClass()
        // 因为在本类中使用，故可以省略this关键字
        if (o == null || getClass() != o.getClass()) return false;
        // 在保证类型转换无误后进行类型转换：能来到这里说明待比较的对象o与Student不是同一个对象，但是是同一种类型：在这里则都为Student类型
        Student student = (Student) o;
        return age == student.age &&
                // name.equals(student.name)
                // 因为name是String类型，是引用类型，故在String中也有equals方法：表示查看字符串的内容是否相等
                // 因为不确定name是否为null，故我们在这里使用Objects.equals()方法防止报空指针异常
                Objects.equals(name, student.name);
//                (name == null) ? name == null :name.equals(student.name);
//        (person.name == null ? name == null : person.name.equals(name));
    }

    @Override
    public int hashCode() {
        return Objects.hash(age, name);
    }
}
