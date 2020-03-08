package com.zlim;

import org.junit.Test;

import java.lang.annotation.Annotation;
import java.lang.reflect.Method;
import java.lang.reflect.Modifier;

/**
 * 获取运行时类的方法结构
 * 框架 = 注解 + 反射 + 设计模式
 *
 * @author zlim
 * @create 2020-03-08 23:30
 */
public class GetClassMethodTest {

    @Test
    public void testGetMethod(){
        Class<Person> clazz = Person.class;
        //getMethods():获取当前运行时类及其所有父类中声明为public权限的方法
        Method[] methods = clazz.getMethods();
        for (Method m : methods){
            System.out.println(m);
        }
        System.out.println();
        //getDeclaredMethods():获取当前运行时类(也即当前Class对象)中声明的所有方法。（不包含父类中声明的方法）
        Method[] declaredMethods = clazz.getDeclaredMethods();
        for (Method declaredMethod : declaredMethods) {
            System.out.println(declaredMethod);
        }
    }


    /*
    获取方法的所有信息：
    @Xxxx(注解)
    权限修饰符  返回值类型  方法名(参数类型1 形参名1,...) throws XxxException{}
     */
    @Test
    public void testGetMethodAllInfo(){
        Class<Person> clazz = Person.class;
        // 获取本运行类中的所有方法
        Method[] declaredMethods = clazz.getDeclaredMethods();
        for (Method dm : declaredMethods) {
            Annotation[] annos = dm.getAnnotations();
            //1.获取方法声明的注解：getAnnotations：注解的生命周期一定要是RUNTIME的
            for (Annotation a : annos) {
                System.out.println(a);
            }

            //2.权限修饰符：Modifier
            System.out.print(Modifier.toString(dm.getModifiers()) + "\t");

            //3.返回值类型：getReturnTpye
            System.out.print(dm.getReturnType().getName() + "\t");

            //4.方法名：getName
            System.out.print(dm.getName());
            System.out.print("(");
            //5.形参列表：获取形参类型：getParameterTypes
            Class<?>[] parameterTypes = dm.getParameterTypes();
            if( !(parameterTypes == null && parameterTypes.length == 0)){
                for (int i = 0; i < parameterTypes.length; i++) {
                    // 如果是最后一个形参，则不要加逗号，直接输出
                    if( i == parameterTypes.length - 1){
                        // 拼接成：形参数据类型 与形参名 的形式
                        System.out.print(parameterTypes[i].getName() + " args" + i);
                        break;
                    }
                    System.out.print(parameterTypes[i].getName() + " args" + i +",");
                }
            }
            System.out.print(")");

            //6.抛出的异常：获取异常的类型：getExceptionTypes
            Class<?>[] exceptionTypes = dm.getExceptionTypes();
            if( exceptionTypes.length > 0){
                System.out.print("throws");
                for (int i = 0; i < exceptionTypes.length; i++) {
                    // 最后一个异常不要加逗号
                    if( i == exceptionTypes.length - 1){
                        System.out.print(exceptionTypes[i].getName());
                        break;
                    }
                    System.out.print(exceptionTypes[i].getName() + ",");
                }
            }
            System.out.println();

        }


    }


}
