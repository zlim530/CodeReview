package com.zlim.connectionpool;

import com.mchange.v2.c3p0.ComboPooledDataSource;
import com.mchange.v2.c3p0.DataSources;
import org.junit.Test;

import java.sql.Connection;
import java.sql.SQLException;

/**
 *
 * JDBC 的数据库连接池使用 javax.sql.DataSource 来表示，DataSource 只是一个接口，该接口通常由服务器
 * (Weblogic, WebSphere, Tomcat)提供实现，也有一些开源组织提供实现：
 * 1.DBCP 是Apache提供的数据库连接池。tomcat 服务器自带dbcp数据库连接池。速度相对c3p0较快，但因
 * 自身存在BUG，Hibernate3已不再提供支持。
 * 2.C3P0 是一个开源组织提供的一个数据库连接池，速度相对较慢，稳定性还可以。hibernate官方推荐使用
 * Proxool 是sourceforge下的一个开源项目数据库连接池，有监控连接池状态的功能，稳定性较c3p0差一
 * 点
 * 3.BoneCP 是一个开源组织提供的数据库连接池，速度快
 * 4.Druid 是阿里提供的数据库连接池，据说是集DBCP 、C3P0 、Proxool 优点于一身的数据库连接池，但是
 * 速度不确定是否有BoneCP快
 *
 * DataSource 通常被称为数据源，它包含连接池和连接池管理两个部分，习惯上也经常把 DataSource 称为连接池
 * DataSource用来取代DriverManager来获取Connection，获取速度快，同时可以大幅度提高数据库访问速度。
 *特别注意：
 * 数据源和数据库连接不同，数据源无需创建多个，它是产生数据库连接的工厂，因此整个应用只需要一个
 * 数据源即可。
 * 当数据库访问结束后，程序还是像以前一样关闭数据库连接：conn.close(); 但conn.close()并没有关闭数
 * 据库的物理连接，它仅仅把数据库连接释放，归还给了数据库连接池。
 *
 * @author zlim
 * @create 2020-03-16 1:24
 */
public class C3T0PoolTest {
    //方式一：不建议使用：因为是连接数据库使用了硬编码的方法：即将数据库连接的配置信息直接写在的代码中
    @Test
    public void testGetConnection() throws Exception {
        //获取c3p0数据库连接池
        ComboPooledDataSource cpds = new ComboPooledDataSource();
        cpds.setDriverClass("com.mysql.jdbc.Driver");
        cpds.setJdbcUrl("jdbc:mysql://localhost:3306/test");
        cpds.setUser("root");
        cpds.setPassword("");

        //通过设置相关的参数，对数据库连接池进行管理：
        //设置初始时数据库连接池中的连接数
        cpds.setInitialPoolSize(10);

        Connection conn = cpds.getConnection();
        System.out.println("conn = " + conn);

        //销毁c3p0数据库连接池:一般情况下是不会关连接池的
        // 如果某一个连接不用了,可以close掉,即将此连接归还到连接池
        // DataSources.destroy(cpds);
    }


    //方式二：使用配置文件
    @Test
    public void testGetConnectionByFile() throws SQLException {
        ComboPooledDataSource cpds = new ComboPooledDataSource("helloc3p0");
        Connection conn = cpds.getConnection();
        System.out.println("conn = " + conn);

    }

}
