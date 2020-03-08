package com.zlim;

import org.junit.Test;

import java.lang.reflect.Constructor;
import java.lang.reflect.Field;
import java.lang.reflect.Method;

/**
 * @author zlim
 * @create 2020-03-09 0:14
 */
public class GetRunTimeClassInfo {

    /*
    如何操作运行时类中的指定的属性 -- 需要掌握:真正开发中用的
     */
    @Test
    public void testGetFields() throws Exception {
        Class<Person> personClass = Person.class;
        //创建运行时类的对象
        Person person = personClass.newInstance();
        //1. getDeclaredField(String fieldName):获取运行时类中指定变量名的属性
        // 任何权限修饰符都可以获取
        Field name = personClass.getDeclaredField("name");

        //2.保证当前属性是可访问的:如果不设置则会抛出IllegalAccessException
        name.setAccessible(true);

        //3.获取、设置指定对象的此属性值
        name.set(person,"jerry");
        System.out.println("name = " + name.get(person));   // name = jerry
    }


    /*
    如何操作运行时类中的指定的方法 -- 需要掌握
     */
    @Test
    public void testGetMethod() throws Exception {
        Class<Person> personClass = Person.class;
        //创建运行时类的对象
        Person person = personClass.newInstance();

        /*
        1.获取指定的某个方法
        getDeclaredMethod():参数1 ：指明获取的方法的名称  参数2：指明获取的方法的形参类型列表
         */
        Method showNation = personClass.getDeclaredMethod("showNation", String.class);
        //2.保证当前方法是可访问的
        showNation.setAccessible(true);

        /*
        3. 调用方法的invoke():参数1：方法的调用者(非静态的方法需要具体的对象去调用)  参数2：给方法形参赋值的实参的值
        参数1一定要有,参数2为可变形参:也即可以为null
        invoke()的返回值即为对应类中调用的方法的返回值。
         */
        Object china = showNation.invoke(person, "China");//类似于:String nation = p.show("China");
        System.out.println("nation = " + china);

        System.out.println("*****************How to get static method?*******************");

        // private static void showDesc()

        Method showDesc = personClass.getDeclaredMethod("showDesc");
        showDesc.setAccessible(true);

        //如果调用的运行时类中的方法没有返回值，则此invoke()返回null
//        Object returnVal = showDesc.invoke(null);
        // 之所以写null也可以是因为本身调用invoke这个方法的对象showDesc就是通过当前类Person.class获取的
        // 而静态方法不管是谁调用都是一样的,不像实例方法(不同的实例调用会产生不同的效果)
        // 因此当我们在创建这个运行时类对象clazz时其实就已经知道了这个运行时类的静态方法
        // 又因为invoke方法第一个参数不能为空(即不可以不写),故写上null也可以正确调用此运行时类的静态方法
        // invoke(Person.class):表示方法的调用者是当前类
        Object invoke = showDesc.invoke(Person.class);
        // Object invoke = showDesc.invoke(null);
        System.out.println("returnValue = " + invoke);//null:表示当前方法没有返回值


    }


    /*
    如何调用运行时类中的指定的构造器:不常用:因为调用构造器就是用来创建对象的:但是我们创建对象通常都是用newInstance方法
     */
    @Test
    public void testGetConstructor() throws  Exception{
        Class<Person> personClass = Person.class;

        //private Person(String name)
        /*
        1.获取指定的构造器
        getDeclaredConstructor():参数：指明构造器的参数列表:只有一个参数,构造器名是不用说的,因为都是当前类名
         */

        Constructor<Person> declaredConstructor = personClass.getDeclaredConstructor(String.class);
        //2.保证此构造器是可访问的
        declaredConstructor.setAccessible(true);
        //3.调用此构造器创建运行时类的对象
        Person tom = declaredConstructor.newInstance("Tom");
        System.out.println("P = " + tom);//P = Person{name='Tom', age=0}
    }



}
