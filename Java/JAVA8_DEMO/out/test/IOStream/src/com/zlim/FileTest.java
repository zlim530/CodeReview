package com.zlim;

import org.junit.Test;

import java.io.File;
import java.io.IOException;

/**
 * File类的使用：
 * File类的一个对象，代表一个文件或一个文件目录
 * File类声明在java.io包下
 * File类中涉及到文件或文件目录的创建、删除、重命名、修改时间、文件大小等犯法
 *  并未涉及到写入或读取文件内容的操作，如果需要读取或写入文件内容，必须使用IO流来完成
 *  后续File类的对象常会作为参数传递到流的构造器中，指明读取或写入的“终点”.
 *
 * 关于路劲分隔符：Windows：\\   Unix：/
 *
 * 为了解决这个隐患，File类提供了一个常量：
 * public static final String separator。根据操作系统，动态的提供分隔符。
 * 举例：
 * File file1 = new File("d:\\atguigu\\info.txt");
 * File file2 = new File("d:" + File.separator + "atguigu" + File.separator + "info.txt");
 * File file3 = new File("d:/atguigu");
 *
 * @author zlim
 * @create 2020-03-02 18:08
 */
public class FileTest {

    @Test
    public void test1(){
        File file = new File("hello.txt");// 相对路劲：相对于当前module的路劲而言
        // 如果使用JUnit单元测试方法测试，则相当路径为当前module下；但如果是在main()方法中测试，则相对路径即为当前的project下
        // 而对于Eclipse，则不管是用单元测试还是main()方法其相对路径都是当前project下
        System.out.println(file);   // 此时还仅仅是内存层面的一个对象，还未涉及到硬盘


        File file1 = new File("C:\\Users\\Lim\\Desktop\\code\\CodeReview\\Java\\JAVA8_DEMO\\IOStream");
        // 绝对路径
    }

    @Test
    public void test2(){
        File file1 = new File("hi.txt");
        File file2 = new File("C:\\Users\\Lim\\Desktop\\Linux\\B-其他语言资料\\Java\\宋红康-Java-尚硅谷");

        System.out.println(file1.getAbsolutePath());
        System.out.println(file1.getPath());
        System.out.println(file1.getName());
        System.out.println(file1.getParent());
        System.out.println(file1.length());
        System.out.println(file1.lastModified());

        System.out.println(file2.getAbsolutePath());
        System.out.println(file2.getPath());
        System.out.println(file2.getName());
        System.out.println(file2.getParent());
        System.out.println(file2.length());
        System.out.println(file2.lastModified());

    }

    @Test
    public void test3(){
        File file1 = new File("C:\\Users\\Lim\\Desktop\\Linux\\B-其他语言资料\\Java\\宋红康-Java-尚硅谷");
        String[] list = file1.list();
        for (String s : list){
            System.out.println(s);
        }

        File[] files = file1.listFiles();
        for (File file : files){
            System.out.println(file);
        }
    }

    //File 类的重命名功能
    //  public boolean renameTo(File dest):把文件重命名为指定的文件路径
    //  例如file1.renameTo(file2);
    //  注意：若想重命名成功，则file1必须存在，file2不能存在
    @Test
    public void test4(){
        File file1 = new File("hi.txt");
        File file2 = new File("C:\\Users\\Lim\\Desktop\\Linux\\B-其他语言资料\\Java\\宋红康-Java-尚硅谷/hello.txt");

        boolean b = file2.renameTo(file1);
        System.out.println(b);
    }

//     File 类的判断功能
//  public boolean isDirectory()：判断是否是文件目录
//  public boolean isFile() ：判断是否是文件
//  public boolean exists() ：判断是否存在
//  public boolean canRead() ：判断是否可读
//  public boolean canWrite() ：判断是否可写
//  public boolean isHidden() ：判断是否隐藏


//     File 类的创建功能
//  public boolean createNewFile() ：创建文件。若文件存在，则不创建，返回false
//  public boolean mkdir() ：创建文件目录。如果此文件目录存在，就不创建了。
//     如果此文件目录的上层目录不存在，也不创建。
//  public boolean mkdirs() ：创建文件目录。如果上层文件目录不存在，一并创建
//  注意事项：如果你创建文件或者 文件 目录没有 写 盘符路径 ， 那么默认在项目路径下 。
//     File 类的删除功能
//  public boolean delete()：删除文件或者文件夹
//     删除注意事项：
//     Java中的删除不走 回收站。要删除一个文件目录，请注意该文件目录内不能包含文件或者文件目录
    @Test
    public void test5() throws IOException {
        File file = new File("hello.txt");
        if (!file.exists()){
            file.createNewFile();
            System.out.println("创建成功");
        } else{
            file.delete();
            System.out.println("删除成功");
        }
    }



}
























