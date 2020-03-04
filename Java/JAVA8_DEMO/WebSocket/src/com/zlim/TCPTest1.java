package com.zlim;

import org.junit.Test;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;

/**
 * @author zlim
 * @create 2020-03-04 23:25
 */
public class TCPTest1 {

    /*
    * 实现客户端：
    *   1.创建Socket对象：需要指明服务器端的ip地址与端口号
    *   2.获取一个输出流：用于向服务器输出（发送）数据
    *   3.输出数据的具体操作：wirte()方法
    *   4.关闭资源
    * */
    @Test
    public void client()  {
        Socket sc = null;
        OutputStream os = null;
        try {
            // 通过InetAddress类的静态getByName()方法来获取一个InetAddress对象
            InetAddress inet = InetAddress.getByName("192.168.42.1");
            sc = new Socket(inet,4399);
            os = sc.getOutputStream();

            os.write("hi i am zlim from yourself".getBytes());
            System.out.println("数据发送成功。");
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            if( os != null){
                try {
                    os.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }

            }
            if( sc != null){
                try {
                    sc.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }


    }


    /*
    * 实现服务器端：
    *   1.创建服务器端对象ServerSocket，并指明自己的端口号；
    *   2.调用accept()方法接收来自客户端的socket
    *   3.获取输入流
    *   4.读取客户端输入流中的数据：即接收来自客户端的数据
    *   5.关闭资源
    *  */
    @Test
    public void server()  {

        ServerSocket sc = null;
        Socket socket = null;
        InputStream is = null;
        ByteArrayOutputStream baos = null;
        try {
            System.out.println("正在等待接收数据...");
            sc = new ServerSocket(4399);
            socket = sc.accept();

            is = socket.getInputStream();

            // ByteArrayOutputStream会自动将数据写到内部的数组中:并且内部的数据会动态扩容
            baos = new ByteArrayOutputStream();
            byte[] buffer = new byte[5];
            int len;
            while( (len = is.read(buffer)) != -1){
                baos.write(buffer,0,len);
            }

            System.out.println("收到了来自" + socket.getInetAddress() + "的数据:");
            System.out.println(baos.toString());
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            if( baos != null){
                try {
                    baos.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }

            }
            if( is != null){
                try {
                    is.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }

            }
            if( socket != null){
                try {
                    socket.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }

            }
            if( sc != null){
                try {
                    sc.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }

            }

        }


    }

}
