package com.zlim;

import java.io.IOException;
import java.net.Inet4Address;
import java.net.InetAddress;

/**
 *
 * InetAddress类主要表示IP地址，两个子类：Inet4Address、Inet6Address。
 * InetAddress 类对象含有一个 Internet 主机地址的域名和IP地址：
 *      www.atguigu.com 和 202.108.35.210。
 *
 * InetAddress 类没有提供公共的构造器，而是提供了如下几个静态方法来获取InetAddress 实例
 * public static InetAddress getLocalHost():获得本机InetAddress对象
 * public static InetAddress getByName(String host)
 * InetAddress 提供了如下几个常用 的 方法
 * public String getHostAddress() ：返回 IP 地址字符串（以文本表现形式）。
 * public String getHostName() ：获取此 IP 地址的主机名
 * public boolean isReachable(int timeout)： ：测试是否可以达到该地址
 *
 *
 * @author zlim
 * @create 2020-03-04 21:07
 */
public class InetAddressTest_01 {

    public static void main(String[] args) {
        //InetAddress类私有化了构造器，故不能通过new的方式创建实例对象
        try {
            // File file = new File("Hello.txt");
            InetAddress localhost = Inet4Address.getByName("127.0.0.1");
            System.out.println("localhost = " + localhost);

            InetAddress inet1 = Inet4Address.getByName("www.baidu.com");
            System.out.println("inet1 = " + inet1);

            InetAddress inet2 = Inet4Address.getLocalHost();
            System.out.println("inet2 = " + inet2); // LIM-T460S/192.168.42.1

            InetAddress inet3 = Inet4Address.getByName("www.google.com");
            String hostAddress = inet3.getHostAddress();
            System.out.println("hostAddress = " + hostAddress);
            String hostName = inet3.getHostName();
            System.out.println("hostName = " + hostName);
            // System.out.println(inet3.isReachable(3000));
            boolean reachable = inet3.isReachable(3000);
            System.out.println("reachable = " + reachable);

        } catch (IOException e) {
            e.printStackTrace();
        }


    }


}














