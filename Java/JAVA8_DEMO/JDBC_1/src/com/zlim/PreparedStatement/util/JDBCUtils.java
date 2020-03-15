package com.zlim.PreparedStatement.util;

import com.zlim.connection.ConnectionTest;
import org.junit.Test;

import java.io.FileInputStream;
import java.io.InputStream;
import java.sql.*;
import java.util.Properties;

/**
 * 操作数据库的工具类
 *
 * @author zlim
 * @create 2020-03-14 1:48
 */
public class JDBCUtils {

    // @Test
    // public void test() throws Exception {
    //     Connection connection = getConnection();
    //     System.out.println("connection = " + connection);
    // }

    //获取数据库的连接
    public static Connection getConnection() throws Exception {
        //1.读取配置文件中的4个基本信息：通过类的加载器来读取
        // InputStream is = ConnectionTest.class.getClassLoader().getResourceAsStream("C:\\Users\\Lim\\Desktop\\code\\CodeReview\\Java\\JAVA8_DEMO\\JDBC_1\\src\\jdbc.properties");
        FileInputStream is = new FileInputStream("C:\\Users\\Lim\\Desktop\\code\\CodeReview\\Java\\JAVA8_DEMO\\JDBC_1\\src\\jdbc.properties");
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
        return connection;
    }


    //关闭连接和Statement的操作
    public static void closeResource(Connection conn, Statement ps){
        try {
            if (ps!= null)
                ps.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        try {
            if (conn!= null)
                conn.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    //关闭资源操作
    public static void closeResource(Connection conn, Statement ps, ResultSet rs){
        try {
            if (ps!= null)
                ps.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        try {
            if (conn!= null)
                conn.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        try {
            if (rs!= null)
                rs.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

}
