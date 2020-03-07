package com.zlim;

import org.junit.Test;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.InputStream;
import java.util.Properties;

/**
 * @author zlim
 * @create 2020-03-07 21:45
 */
public class ClassLoadTest {

    @Test
    public void test1(){
        //对于自定义类，使用系统类加载器进行加载
        ClassLoader classLoader = ClassLoadTest.class.getClassLoader();
        System.out.println("classLoader = " + classLoader);

        //调用系统类加载器的getParent()：可以获取扩展类加载器
        ClassLoader parent = classLoader.getParent();
        System.out.println("parent = " + parent);

        //调用扩展类加载器的getParent()：无法获取引导类加载器（使用C++编写）
        //引导类加载器主要负责加载java的核心类库，无法加载自定义类的。
        ClassLoader parent1 = parent.getParent();
        System.out.println("parent1 = " + parent1);// null

        // String类就是Java中核心类，所以我们无法获得它的类加载器
        ClassLoader classLoader1 = String.class.getClassLoader();
        // 不能再调用classLoader1.getParent()方法：不然会抛出空指针异常:因为引导类加载器已经是最顶层的类加载器了
        // ClassLoader parent2 = classLoader1.getParent(); // java.lang.NullPointerException
        // System.out.println("parent2 = " + parent2);
        System.out.println("classLoader1 = " + classLoader1);// null
    }


    /*
    Properties：用来读取配置文件。
     */
    @Test
    public void testProperties() throws Exception {
        Properties properties = new Properties();
        //此时的文件默认在当前的module下。
        //读取配置文件的方式一：
        // FileInputStream fis = new FileInputStream("jdbc.properties");
        // properties.load(fis);

        // 读取配置文件的方式二：使用ClassLoader
        //配置文件默认识别为：当前module的src下
        // ClassLoaderTest 为当前类名
        ClassLoader classLoader = ClassLoadTest.class.getClassLoader();
        InputStream is = classLoader.getResourceAsStream("jdbcsrc.properties");
        properties.load(is);

        String user = properties.getProperty("user");
        System.out.println("user = " + user);
        String password = properties.getProperty("password");
        System.out.println("password = " + password);
        /*user = zlim
        password = 123456*/


    }



}
