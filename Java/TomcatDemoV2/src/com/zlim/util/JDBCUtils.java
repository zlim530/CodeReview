package com.zlim.util;

import com.alibaba.druid.pool.DruidDataSourceFactory;
import org.junit.Test;

import javax.sql.DataSource;
import java.io.FileInputStream;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.Properties;

/**
 * @author zlim
 * @create 2020-03-18 0:55
 */
public class JDBCUtils {

    /**
     * 使用Druid数据库连接池技术
     */
    private static DataSource source1;
    static {
        try {
            Properties pros = new Properties();
            FileInputStream fis = new FileInputStream("C:\\Users\\Lim\\Desktop\\code\\CodeReview\\Java\\TomcatDemoV2\\src\\druid.properties");

            pros.load(fis);

            source1 = DruidDataSourceFactory.createDataSource(pros);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * 获取数据库连接池对象：JDBCTemplate中使用
     * @return
     */
    public static DataSource getDataSource(){
        return source1;
    }

    public static Connection getConnectionByDruid() throws SQLException {
        Connection conn = source1.getConnection();
        return conn;
    }



    // @Test
    // public void test() throws SQLException {
    //     Connection conn = JDBCUtils.getConnectionByDruid();
    //     System.out.println("conn = " + conn);
    // }

}
