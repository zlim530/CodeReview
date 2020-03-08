package com.zlim;

import org.junit.Test;

import java.lang.annotation.Annotation;
import java.lang.reflect.Constructor;
import java.lang.reflect.ParameterizedType;
import java.lang.reflect.Type;

/**
 * @author zlim
 * @create 2020-03-08 23:53
 */
public class GetClassOtherInfo {

    /*
    获取构造器结构

     */
    @Test
    public void testGetConstructors(){
        Class<Person> personClass = Person.class;
        //getConstructors():获取当前运行时类中(无法获得父类的)声明为public的构造器
        Constructor<?>[] constructors = personClass.getConstructors();
        for (Constructor<?> c : constructors) {
            System.out.println(c);
        }

        System.out.println();

        //getDeclaredConstructors():获取当前运行时类中声明的所有的构造器
        Constructor<?>[] declaredConstructors = personClass.getDeclaredConstructors();
        for (Constructor<?> dc : declaredConstructors) {
            System.out.println(dc);
        }
    }



    /*
    获取运行时类的带泛型的父类的泛型:比较重要:要掌握
    代码：逻辑性代码  vs 功能性代码
     */
    @Test
    public void testGetSuperClassGenericType(){
        Class<Person> personClass = Person.class;
        Type genericSuperclass = personClass.getGenericSuperclass();
        // 带参数的Type
        ParameterizedType paramType = (ParameterizedType) genericSuperclass;
        //获取泛型类型(泛型参数):可以有多个泛型参数:如Map<K,V>
        Type[] actualTypeArguments = paramType.getActualTypeArguments();
        // System.out.println(actualTypeArguments[0].getTypeName());// java.lang.String
        System.out.println(((Class)actualTypeArguments[0]).getName());
    }


    /*
    获取运行时类实现的接口:动态代理中会用到:要掌握
     */
    @Test
    public void testGetInterface(){
        Class<Person> personClass = Person.class;
        // 返回值类型为Class数组:因为可以实现多个接口
        Class<?>[] interfaces = personClass.getInterfaces();
        for (Class<?> anInterface : interfaces) {
            System.out.println("anInterface = " + anInterface);
        }

        System.out.println();
        //获取运行时类的父类实现的接口:先获取父类再获取其接口
        Class<?>[] interfaces1 = personClass.getSuperclass().getInterfaces();
        for (Class<?> aClass : interfaces1) {
            System.out.println("aClass = " + aClass);
        }
    }



    /*
        获取运行时类声明的注解:在框架中用得比较多
        通过反射拿到对应类或者方法的注解,就知道此类或者此方法是干什么的
     */
    @Test
    public void testGetAnnotation(){
        Class<Person> personClass = Person.class;
        Annotation[] annotations = personClass.getAnnotations();
        for (Annotation annotation : annotations) {
            System.out.println("annotation = " + annotation);
        //    annotation = @com.zlim.MyAnnotation(value=PersonClassAnnotatin)
        }
    }


}
