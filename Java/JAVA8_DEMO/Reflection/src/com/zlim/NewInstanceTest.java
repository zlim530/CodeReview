package com.zlim;

import org.junit.Test;
import java.util.Random;


/**
 *
 * 通过反射创建对应的运行时类的对象：即Class类对应的是哪个类就只能创建那个类的对象
 *
 * @author zlim
 * @create 2020-03-08 22:59
 */
public class NewInstanceTest {

    @Test
    public void test1() throws Exception {
        Class<Person> clazz = Person.class;
        /*
        newInstance():调用此方法，创建对应的运行时类的对象。内部调用了运行时类的空参的构造器。
        即只要是创建对象，都需要用到构造器。也只有构造器才可以创建对象。

        要想此方法正常的创建运行时类的对象，要求：
        1.运行时类必须提供空参的构造器
        2.空参的构造器的访问权限得够（即不能是private的）。通常，设置为public。


        在javabean中要求提供一个public的空参构造器。原因：
        1.便于通过反射，创建运行时类的对象
        2.便于子类继承此运行时类时，默认调用super()时，保证父类有此构造器

         */

        Person person = clazz.newInstance();    // 如果当前类没有空参构造器：则会抛出InstantiationException异常
        System.out.println("person = " + person);
        // person = Person{name='null', age=0}
    }


    //体会反射的动态性：即在编译时无法知道创建的哪个类，只有在运行时才知道
    @Test
    public void test2(){

        for (int i = 0; i < 100; i++) {
            int i1 = new Random().nextInt(3);
            String classPath = "";
            switch (i1){
                case 0:
                    // classPath = "java.lang.String";
                    classPath = "java.util.Date";
                    break;
                case 1:
                    classPath = "java.lang.Object";
                    break;
                case 2:
                    classPath = "com.zlim.Person";
                    break;
            }

            // 因为getInstance抛出了异常所以在调用此方法时需要对异常进行处理
            try {
                Object o = getInstance(classPath);
                System.out.println("o["+ i +"] = " + o);
            } catch (Exception e) {
                e.printStackTrace();
            }

        }



    }


    /*
    创建一个指定类的对象。
    classPath:指定类的全类名
     */
    public Object getInstance(String classPath) throws Exception {
        Class clazz = Class.forName(classPath);
        return clazz.newInstance();
    }




}








