package com.zlim;

public class PassByValue {
    public static void main(String[] args) {
        PassByValue ps = new PassByValue();
        int i = 10;
        // ps.pass(i);
        // System.out.println("print in main func: i is " + i);
        // print in main func: i is 10

        //--------------------------------------------

        Student stu = new Student();
        stu.setName("hello");
        stu.setAge(22);
        // ps.pass(stu);
        // System.out.println("print in main func, stu is " + stu);
        // print in main func, stu is com.zlim.Student@1b6d3586

        //--------------------------------------------

        String str = "Hello";
        ps.pass(str);
        System.out.println("print in main func, name is " + str);
        // print in main func, name is Hello
    }

    public void pass(String name){
        name = "helloWorld";
        System.out.println("print in pass, name is " + name);
        // print in pass, name is helloWorld
    }

    public  void  pass(Student stu){
        stu.setName("helloWorld");
        System.out.println("print in pass,stu is " + stu);
        // print in pass,stu is com.zlim.Student@1b6d3586
    }

    public void  pass(int j){
        j = 20;
        System.out.println("print in pass func: j is " + j);
        // print in pass func: j is 20
    }

    public static void print(String name){
        System.out.println(name);
    }
}
