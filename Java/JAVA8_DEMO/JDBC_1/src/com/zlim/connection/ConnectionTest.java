package com.zlim.connection;

import org.junit.Test;

import java.io.InputStream;
import java.sql.Connection;
import java.sql.Driver;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.Properties;

/**
 * 获取数据库连接
 *
 * @author zlim
 * @create 2020-03-13 22:33
 */
public class ConnectionTest {
    // 最终版：
    //方式五(final版)：将数据库连接需要的4个基本信息声明在配置文件中，通过读取配置文件的方式，获取连接
    /*
     * 此种方式的好处？
     * 1.实现了数据与代码的分离。实现了解耦
     * 2.如果需要修改配置文件信息，可以避免程序重新打包(即对程序进行编译)。
     */
    @Test
    public void testGetConnection() throws Exception {
        //1.读取配置文件中的4个基本信息：通过类的加载器来读取
        InputStream is = ConnectionTest.class.getClassLoader().getResourceAsStream("src\\jdbc.properties");

        Properties properties = new Properties();
        properties.load(is);

        String user = properties.getProperty("user");
        String password = properties.getProperty("password");
        String url = properties.getProperty("url");
        String driverClass = properties.getProperty("driverClass");

        //2.加载驱动
        Class.forName(driverClass);

        //3.获取连接
        Connection connection = DriverManager.getConnection(url, user, password);
        System.out.println("connection = " + connection);

    }


    // 方式一：
    @Test
    public void testConnection1() throws SQLException {
        // Driver是Java.sql包中定义的接口
        // 而com.mysql.jdbc.Driver类则是MySQL数据库定义的实现了Driver这个接口的具体实现类
        // 这实际上是一种多态的体现：当下面driver对象去调用connect()方法返回一个Connection接口的对象时
        // 实际上就是com.mysql.jdbc.Driver类里面重写connect()方法所返回的相应的Connection接口的实现类
        // 获取Driver实现类对象：new com.mysql.jdbc.Driver()
        Driver driver = new com.mysql.jdbc.Driver();

        // url:http://localhost:8080/gmall/keyboard.jpg
        // jdbc:mysql:协议
        // localhost:ip地址
        // 3306：默认mysql的端口号
        // test:test数据库
        String url = "jdbc:mysql://localhost:3306/test";
        // 将用户名和密码封装在Properties中
        Properties info = new Properties();
        info.setProperty("user", "root");
        info.setProperty("password", "abc123");

        Connection conn = driver.connect(url, info);

        System.out.println(conn);
    }

    // 方式二：对方式一的迭代(递进):在如下的程序中不出现第三方的api,使得程序具有更好的可移植性
    @Test
    public void testConnection2() throws Exception {
        // 1.获取Driver实现类对象：使用反射
        Class clazz = Class.forName("com.mysql.jdbc.Driver");
        Driver driver = (Driver) clazz.newInstance();

        // 2.提供要连接的数据库
        String url = "jdbc:mysql://localhost:3306/test";

        // 3.提供连接需要的用户名和密码
        Properties info = new Properties();
        info.setProperty("user", "root");
        info.setProperty("password", "abc123");

        // 4.获取连接
        Connection conn = driver.connect(url, info);
        System.out.println(conn);

    }

    // 方式三：使用DriverManager替换Driver
    // DriverManager：管理一组 JDBC 驱动程序的基本服务。
    @Test
    public void testConnection3() throws Exception {
        // 1.获取Driver实现类的对象
        Class clazz = Class.forName("com.mysql.jdbc.Driver");
        Driver driver = (Driver) clazz.newInstance();

        // 2.提供另外三个连接的基本信息：
        String url = "jdbc:mysql://localhost:3306/test";
        String user = "root";
        String password = "abc123";

        // 注册驱动
        DriverManager.registerDriver(driver);

        // 获取连接
        Connection conn = DriverManager.getConnection(url, user, password);
        System.out.println(conn);
    }

    // 方式四：可以只是加载驱动，不用显示的注册驱动过了：在方式三的基础上的优化
    @Test
    public void testConnection4() throws Exception {
        // 1.提供三个连接的基本信息：
        String url = "jdbc:mysql://localhost:3306/test";
        String user = "root";
        String password = "abc123";

        // 2.加载Driver(这里是加载MySQL中的Driver类)
        Class.forName("com.mysql.jdbc.Driver");
        //相较于方式三，可以省略如下的操作：
//		Driver driver = (Driver) clazz.newInstance();
//		// 注册驱动
//		DriverManager.registerDriver(driver);
        //为什么可以省略上述操作呢？
		/*
		 * 在mysql的Driver实现类中，声明了如下的操作：静态代码块随着类的加载而执行
		 * static {
				try {
					java.sql.DriverManager.registerDriver(new Driver());
				} catch (SQLException E) {
					throw new RuntimeException("Can't register driver!");
				}
			}
		 */

        // 3.获取连接
        Connection conn = DriverManager.getConnection(url, user, password);
        System.out.println(conn);
    }
    
}
