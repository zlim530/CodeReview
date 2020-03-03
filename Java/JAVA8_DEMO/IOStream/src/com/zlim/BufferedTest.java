package com.zlim;

import org.junit.Test;

import javax.swing.text.FieldView;
import javax.swing.text.html.ObjectView;
import java.io.*;

/**
 *
 * 处理流之一：缓冲流的使用
 *
 * 1.缓冲流
 * BufferedInputStream
 * BufferedOutputStream
 * BufferedReader
 * BufferedWriter
 *
 * 2.提高流的读取、写入速度
 *      原因：内部提供了一个缓冲区
 *
 * 3.处理流:就是"套接"在已有的流的基础上
 *
 * @author zlim
 * @create 2020-03-03 18:46
 */
public class BufferedTest {


    @Test
    public void BufferedStreamTest(){
        BufferedInputStream bis = null;
        BufferedOutputStream bos = null;
        try {
            // 新建文件对象
            File srcFile = new File("C:\\Users\\Lim\\Desktop\\code\\人脸库\\m.jpg");
            File destFile = new File("C:\\Users\\Lim\\Desktop\\code\\人脸库\\m.jpg\\...");

            // 新建节点流对象：也即文件流对象
            FileInputStream fis = new FileInputStream(srcFile);
            FileOutputStream fos = new FileOutputStream(destFile);

            // 新建处理流对象:这里是缓冲流
            bis = new BufferedInputStream(fis);
            bos = new BufferedOutputStream(fos);

            // 具体的读写操作
            byte[] buffer = new byte[10];
            int len;
            while ( (len = bis.read(buffer)) != -1) {
                bos.write(buffer,0,len);
                // bos.flush(); 刷新（清空）缓冲区
            }
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            // 资源的关闭：当我们关闭外部的处理流时内部的节点流资源会自动关闭:故一定要关闭外部流
            if (bos != null){
                try {
                    bos.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }

            try {
                if (bis != null)
                    bis.close();
            } catch (IOException e) {
                e.printStackTrace();
            }

        }

    }



    // 使用缓冲流复制字节文件：并将其封装为一个方法
    public void copFileWithBuffered(String srcPath, String destPath){
        BufferedInputStream bis = null;
        BufferedOutputStream bos = null;
        try {
            // 新建文件对象
            File srcFile = new File(srcPath);
            File destFile = new File(destPath);

            // 新建节点流对象：也即文件流对象
            FileInputStream fis = new FileInputStream(srcFile);
            FileOutputStream fos = new FileOutputStream(destFile);

            // 新建处理流对象:这里是缓冲流
            bis = new BufferedInputStream(fis);
            bos = new BufferedOutputStream(fos);

            // 具体的读写操作
            byte[] buffer = new byte[10];
            int len;
            while ( (len = bis.read(buffer)) != -1) {
                bos.write(buffer,0,len);
            }
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            // 资源的关闭：对我们关闭外部的处理流时内部的节点会自动关闭:注意一定要先关闭外部流
            if (bos != null){
                try {
                    bos.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }

            try {
                if (bis != null)
                    bis.close();
            } catch (IOException e) {
                e.printStackTrace();
            }

        }
    }


    @Test
    public void testCopyFileWithBuffered(){

        long start = System.currentTimeMillis();

        String srcPath = "C:\\Users\\Lim\\Desktop\\Linux\\B-其他语言资料\\Java\\宋红康-Java-尚硅谷\\尚硅谷_宋红康_第13章_IO流\\尚硅谷_宋红康_第13章节练习_IO流.doc";
        String destPath = "testBuffered.doc";

        copFileWithBuffered(srcPath,destPath);

        long end = System.currentTimeMillis();

        System.out.println("使用缓冲流复制操作花费的时间为" + (end - start) + "毫秒");

    }

    @Test
    public void testBufferedReaderWriter(){

        BufferedReader br = null;
        BufferedWriter bw = null;
        try {
            br = new BufferedReader(new FileReader(new File("hi.txt")));
            bw = new BufferedWriter(new FileWriter(new File("lalal.txt")));

            // 方式一：
            // char[] cbuff = new char[1024];
            // int len;
            // while ( (len = br.read(cbuff)) != -1){
            //     bw.write(cbuff,0,len);
            // }

            String date;
            while ( (date = br.readLine()) != null){
                bw.write(date); // 没有换行符
            //    解决方法1：
            //     bw.write(date + "\n");
            //    解决方法2：
                bw.newLine();
            }

        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            try {
                if (br != null)
                    br.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
            try {
                if (bw != null)
                    bw.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }


    }

}




























