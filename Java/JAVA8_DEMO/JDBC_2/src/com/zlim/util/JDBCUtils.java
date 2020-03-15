package com.zlim.util;

import com.alibaba.druid.pool.DruidDataSourceFactory;
import com.mchange.v2.c3p0.ComboPooledDataSource;
import com.mchange.v2.c3p0.DataSources;
import org.apache.commons.dbcp.BasicDataSourceFactory;
import org.junit.Test;

import javax.sql.DataSource;
import java.io.File;
import java.io.FileInputStream;
import java.sql.*;
import java.util.Properties;

/**
 * @author zlim
 * @create 2020-03-16 1:39
 */
public class JDBCUtils {

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

    @Test
    public void test() throws Exception {
        Connection connection = getConnectionByDruid();
        System.out.println("connection = " + connection);
    }

    /**
     * 使用C3P0的数据库连接池技术
     */
    //数据库连接池只需提供一个即可。
    private static ComboPooledDataSource cpds = new ComboPooledDataSource("helloc3p0");
    public static Connection getConnectionByC3T0() throws SQLException {
        Connection conn = cpds.getConnection();
        return conn;
    }


    /**
     * 使用DBCP数据库连接池技术获取数据库连接
     */
    //创建一个DBCP数据库连接池
    private static DataSource source;
    static{
        try {
            Properties pros = new Properties();
            FileInputStream is = new FileInputStream(new File("src/dbcp.properties"));
            pros.load(is);
            source = BasicDataSourceFactory.createDataSource(pros);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
    public static Connection getConnectionByDBCP() throws SQLException {

        Connection conn = source.getConnection();
        return conn;
    }


    /**
     * 使用Druid数据库连接池技术
     */
    private static DataSource source1;
    static {
        try {
            Properties pros = new Properties();
            FileInputStream fis = new FileInputStream("src/druid.properties");

            pros.load(fis);

            source1 = DruidDataSourceFactory.createDataSource(pros);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
    public static Connection getConnectionByDruid() throws SQLException {

        Connection conn = source1.getConnection();
        return conn;
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
