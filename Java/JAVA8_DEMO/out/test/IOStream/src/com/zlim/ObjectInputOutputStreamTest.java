package com.zlim;

import org.junit.Test;

import java.io.*;

/**
 *
 * 对象序列化机制允许把内存中的Java对象转换成平台无关的二进制流，从
 * 而允许把这种二进制流持久地保存在磁盘上，或通过网络将这种二进制流传
 * 输到另一个网络节点。//当其它程序获取了这种二进制流，就可以恢复成原
 * 来的Java对象
 *
 * 要想一个java对象是可序列化的，需要满足一定的要求：
 * 1.如果需要让某个对象支持序列化机制，则必须让对象所属的类及其属性是可序列化的，为了让某个类是可序列化的，该类必须实现如下两个接口之一。
 * 否则，会抛出NotSerializableException异常
 * Serializable    ：常实现这个接口：称为标识接口
 * Externalizable
 * 2.需要此类提供一个全局常量：为了唯一的标识某一个类
 *  序列版本号：psfl = public/private static final long serialVersionUID
 *  凡是实现Serializable接口的类都有一个表示序列化版本标识符的静态变量：
 *  private static final long serialVersionUID;
 *  serialVersionUID用来表明类的不同版本间的兼容性。
 *      简言之，其目的是以序列化对象进行版本控制，有关各版本反序列化时是否兼容。
 *  如果类没有显示定义这个静态常量，它的值是Java运行时环境根据类的内部细节自动生成的。
 *      若类的实例变量做了修改，serialVersionUID 可能发生变化。故建议，
 *  显式声明。
 *  3.除了当前此类需要实现Serializable接口之外，还要保证当前此类内部所有属性
 *      也必须是可序列化的：默认情况下，基本类型都是可序列化的
 *
 *补充：ObjectOutputStream和ObjectInputStream不能序列化static和transient修饰的成员变量
 *
 * @author zlim
 * @create 2020-03-04 1:29
 */
public class ObjectInputOutputStreamTest {


    /*
    * 序列化过程：将内存中的java对象保存到磁盘中或通过网络传输出去
    *   使用ObjectOutputStream实现
    * */
    @Test
    public void testObjectOutputStream() throws IOException {

        ObjectOutputStream oos = new ObjectOutputStream(new FileOutputStream("data.dat"));

        oos.writeObject(new String("i want make money."));
        // oos.writeObject("i want make money.");
        oos.flush();

        oos.writeObject(new Person("zlim",22,160,new Account(4000)));
        oos.flush();

        System.out.println("数据序列化成功。");

        oos.close();

    }


    /*
    * 反序列化：将磁盘文件中的对象还原为内存中的一个java对象
    *   使用ObjectInputStream来实现
    * */
    @Test
    public void testObjectInputStream() throws IOException, ClassNotFoundException {

        ObjectInputStream ois = new ObjectInputStream(new FileInputStream("data.dat"));

        Object o = ois.readObject();
        System.out.println( o);

        Object o1 = ois.readObject();
        Person  p1 = (Person) o1;
        System.out.println("p1 = " + p1);

        System.out.println("数据反序列化成功。");

        ois.close();

    }


}
















