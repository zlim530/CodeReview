package com.zlim.connectionpool;

import com.alibaba.druid.pool.DruidDataSourceFactory;
import org.junit.Test;

import javax.sql.DataSource;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.sql.Connection;
import java.util.Properties;

/**
 * @author zlim
 * @create 2020-03-16 2:18
 */
public class DruidPoolTest {
    @Test
    public void testGetConnectionByDruid() throws Exception {
        Properties pros = new Properties();
        FileInputStream fis = new FileInputStream("src/druid.properties");

        pros.load(fis);

        DataSource dataSource = DruidDataSourceFactory.createDataSource(pros);
        Connection conn = dataSource.getConnection();
        System.out.println("conn = " + conn);
    }
}
