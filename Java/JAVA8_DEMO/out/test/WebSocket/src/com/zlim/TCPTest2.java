package com.zlim;

import org.junit.Test;

import java.io.*;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.UnknownHostException;

/**
 *
 *   实现TCP的网络编程
 *   从客户端发送文件给服务端，服务端保存到本地。并返回“发送成功”给客户端。
 *   并关闭相应的连接。
 *
 * @author zlim
 * @create 2020-03-05 0:49
 */
public class TCPTest2 {
    
    @Test
    public void client() throws IOException {
        //1.准备Socket套接字对象用于连接服务器：需要指定服务器的ip地址与端口号
        Socket socket = new Socket(InetAddress.getByName("localhost"), 5300);

        // 2.获取输出流用来发送数据给服务器（对于服务器而言就是使用输入流来接收）
        OutputStream os = socket.getOutputStream();
        FileInputStream fis = new FileInputStream("m.jpg");

        // 2.1 发送数据给服务器
        byte[] buffer = new byte[1024];
        int len;
        while( (len = fis.read(buffer)) != -1){
            os.write(buffer,0,len);
        }

        System.out.println("数据发送成功。");

        /*
        * public void shutdownOutput()禁用此套接字的输出流。对于 TCP 套接字，任何以前写入的数据都将被发
            送，并且后跟 TCP 的正常连接终止序列。 如果在套接字上调用 shutdownOutput() 后写入套接字输出流，
            则该流将抛出 IOException。 即不能通过此套接字的输出流发送任何数据。
        * */
        socket.shutdownOutput();    //并且需要在输出流的末尾写入一个“流的末尾”标记
        // 相当于提醒服务器已经没有数据发送；否则服务区输入流的读取方法read()将会一直尝试读取，从而使程序阻塞

        //接收来自服务器端的数据，并显示到控制台上
        // 3.获取输入流：用来接收服务器发送给客户端的数据信息
        InputStream is = socket.getInputStream();
        // 通过ByteArrayOutputStream读取服务器传来的字节流数据，ByteArrayOutputStream会将从指定byte[]数组读取到的字节数据
        // 保存在内部的数组中，当我们想要在控制台输出时只需要将其转化为字符串即可
        ByteArrayOutputStream baos = new ByteArrayOutputStream();
        byte[] buffer2 = new byte[20];
        int len1;
        while( (len1 = is.read(buffer) ) != -1){
            baos.write(buffer,0,len1);
        }
        System.out.println("接收到来自服务器端的数据信息：");
        System.out.println(baos.toString());

        os.close();
        fis.close();
        // 关闭socket表示不再与服务器通信，也即断开与服务器的连接
        socket.close();
    }
    
    
    @Test
    public void server() throws IOException {
        // 1.新建一个ServerSocket对象，并设置服务器的端口号
        ServerSocket ss = new ServerSocket(5300);

        System.out.println("正在等待接收数据...");
        // 2.监听一个客户端的连接：也即获得一个Socket对象
        Socket sk = ss.accept();// 并且该方法是一个阻塞方法：如果没有客户端连接，将一直等待

        // 3.获取输入流：用来接收来自客户端的数据信息
        InputStream is = sk.getInputStream();
        // 4.新建一个字节输出流对象用来将接收的客户端数据信息写入到指定的磁盘文件中
        FileOutputStream fos = new FileOutputStream("qqcopy.jpg");

        // 4.从is中接收数据存在buffer中，fos再从bufffer中读取数据写到copy.jpg磁盘文件中
        byte[] buffer = new byte[1024];
        int len;
        while( (len = is.read(buffer)) != -1){
            fos.write(buffer,0,len);// 表示从指定的byte数组buffer中偏移量off的位置开始读取len个字节的数据写入到输出流指定的文件中
            // 并不是写入到buffer中
        }
        System.out.println("成功接收到来自" + sk.getInetAddress()+ "的数据，并成功写入到指定文件中。");

        OutputStream os = sk.getOutputStream();
        os.write("你好，我已经成功收到了你发来的数据信息。".getBytes());

        //5.关闭资源
        os.close();
        fos.close();
        is.close();
        // 关闭socket，表示不再与该客户端进行通信
        sk.close();
        // 如果不在接收任何客户端的通信，可以关闭ServerSocket对象
        ss.close();

    }
    
}
