package com.zlim.connectionpool;

import org.apache.commons.dbcp.BasicDataSource;
import org.apache.commons.dbcp.BasicDataSourceFactory;
import org.junit.Test;

import javax.sql.DataSource;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.sql.Connection;
import java.sql.SQLException;
import java.util.Properties;

/**
 * 测试DBCP的数据库连接池技术
 * @author zlim
 * @create 2020-03-16 2:06
 */
public class DBCPPoolTest {

    @Test
    public void testGetConnectionByDBCP() throws SQLException {
        //创建了DBCP的数据库连接池
        BasicDataSource source = new BasicDataSource();

        //设置基本信息
        source.setDriverClassName("com.mysql.jdbc.Driver");
        source.setUrl("jdbc:mysql:///test");
        source.setUsername("root");
        source.setPassword("");

        //还可以设置其他涉及数据库连接池管理的相关属性：
        source.setInitialSize(10);
        source.setMaxActive(10);

        Connection conn = source.getConnection();
        System.out.println("conn = " + conn);

    }


    //方式二：推荐：使用配置文件
    @Test
    public void testGetConnectionByFile() throws Exception {
        Properties pros = new Properties();

        //方式1：
//		InputStream is = ClassLoader.getSystemClassLoader().getResourceAsStream("dbcp.properties");
        //方式2：
        FileInputStream fis = new FileInputStream("src/dbcp.properties");

        pros.load(fis);
        DataSource source = BasicDataSourceFactory.createDataSource(pros);

        Connection conn = source.getConnection();
        System.out.println("conn = " + conn);

    }

}
