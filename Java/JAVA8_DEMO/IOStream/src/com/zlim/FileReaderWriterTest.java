package com.zlim;

import org.junit.Test;

import java.io.*;

/**
 * 流的分类：
 * 按照数据单位：字节流、字符流
 * 按照数据的流向：输入流、输出流
 * 按照流的角色：节点流、处理流
 *
 * 流的体系结构：
 * 四个抽象基类           节点流（或文件流）           缓冲流（处理流的一种）
 * InputStream            FileInputStream            BufferedInputStream
 * OutputStream           FileOutputStream           BufferedOutputStream
 * Reader                 FileReader                 BufferedReader
 * Writer                 FileWriter                 BufferedWriter
 *
 * @author zlim
 * @create 2020-03-02 20:38
 */
public class FileReaderWriterTest {

    public static void main(String[] args) {
        File file = new File("hi.txt"); // 相较于当前工程
        System.out.println(file.getAbsolutePath());
    }


    // 异常的处理：为了保证流资源一定可以执行关闭操作：需要使用try-catch-finally处理
    // 读入的文件一定要存在，否则就会报FileNotFoundException
    @Test
    public void testFileReader() {
        FileReader fr = null;
        try {
            // 实例化File类的对象，指明要操作的文件
            File file = new File("hi.txt"); // 相较于当前module
            // 提供具体的流
            fr = new FileReader(file);

            // 数据的读入
            // read():返回读入的一个字符(以字符的值的方法)，如果到达文件末尾，则返回-1
            // int date = fr.read();
            // while (date != -1){
            //     System.out.print((char) date);
            //     date = fr.read();   // 类似于i++
            // }

            int date;
            while ((date = fr.read()) != -1){
                System.out.println((char)date);
            }
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            // 手动的关闭流:流的关闭操作
            try {
                if (fr != null)
                    fr.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }


    }

    // 对read()操作升级：使用read的重载方法
    @Test
    public void testFileReader1()  {
        FileReader fr = null;
        try {
            File file = new File("hi.txt");

            fr = new FileReader(file);

            char[] cbuff = new char[5];
            // read(char[] cbuf):返回每次读入cbuf数组中的字符的个数。如果达到文件末尾，返回-1
            int len = fr.read(cbuff);
            while (len != -1){
                // for (int i = 0; i < cbuff.length; i++) {
                //     System.out.print(cbuff[i]); // HelloWorld123ld:错误的:因为后续字符只有3个故在cbuff这个数组中
                //     // 仅重写了前三个字符
                // }

                // for (int i = 0; i < len; i++) {
                //     System.out.print(cbuff[i]); // HelloWorld123：正确写法
                // }

                // String str = new String(cbuff);
                // System.out.print(str);  // HelloWorld123ld:错误的

                String str = new String(cbuff,0,len);
                System.out.print(str);  // HelloWorld123:正确的
                len = fr.read(cbuff);
            }
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            if (fr != null) {
                try {
                    fr.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }

    }

    /*
    从内存中写出数据到硬盘的文件里
        输出操作对应的file可以不存在，并不会报异常，如果File类对应硬盘上的文件不存在，在输出的过程会自动创建此文件；
        如果文件存在，则会进行覆盖
    * */
    @Test
    public void testFileWriter()  {
        File file1 = null;
        FileWriter fw = null;
        try {
            File file = new File("hi1.txt");
            file1 = new File("hi.txt");

            fw = new FileWriter(file1);


            // 写出数据的操作
            fw.write("I hava a dream.\n");
            // fw.flush();
            fw.write("You need to have a dream,too\n");
            // fw.flush();
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            if (fw!= null) {
                try {
                    fw.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }


        FileReader fr = null;
        try {
            fr = new FileReader(file1);
            // 数据的读入
            // read():返回读入的一个字符(以字符的值的方法)，如果到达文件末尾，则返回-1
            int date = fr.read();
            while (date != -1){
                System.out.print((char) date);
                date = fr.read();   // 类似于i++
            }
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            if (fr != null) {
                try {
                    fr.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }

    }





}






































