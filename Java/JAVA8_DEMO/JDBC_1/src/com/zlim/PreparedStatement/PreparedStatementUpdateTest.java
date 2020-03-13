package com.zlim.PreparedStatement;

import com.zlim.PreparedStatement.util.JDBCUtils;
import org.junit.Test;

import java.io.IOException;
import java.io.InputStream;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Properties;

/**
 * 使用PreparedStatement来替换Statement,实现对数据表的增删改操作
 *
 * 增删改；查(CRUD:增加(Create)、读取(Retrieve)、更新(Update)和删除(Delete))
 * @author zlim
 * @create 2020-03-14 1:27
 */
public class PreparedStatementUpdateTest {


    @Test
    public void testCommonUpdate(){
        // String sql = "delete from customers where id = ?";
        // update(sql,19);

        // update order set order_name = ? where order_id = ?：SQL语句报错：因为order是关键字
        String sql = "update `order` set order_name = ? where order_id = ?";// order要用反引号(``)引起来
        update(sql,"DD",2);

    }


    //通用的增删改操作:使用可变形参
    public void update(String sql,Object ...args)  {//sql中占位符的个数与可变形参的长度相同！
        Connection conn = null;
        PreparedStatement ps = null;
        try {
            //1.获取数据库的连接
            conn = JDBCUtils.getConnection();
            //2.预编译sql语句，返回PreparedStatement的实例
            ps = conn.prepareStatement(sql);

            //3.填充占位符
            // 把可变形参看做一个数组即可
            for (int i = 0; i < args.length; i++) {
                ps.setObject(i+1,args[i]);//小心参数声明错误！！
            }
            ps.execute();
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            //5.资源的关闭
            JDBCUtils.closeResource(conn,ps);
        }
    }


    @Test
    public void testUpdate()  {
        Connection conn = null;
        PreparedStatement ps = null;
        try {
            //1.获取数据库的连接
            conn = JDBCUtils.getConnection();
            //2.预编译sql语句，返回PreparedStatement的实例(对象)
            String sql = "update customers set name = ? where id = ?";
            // 之所以叫预编译是因为创建PreparedStatement实例对象时就已经知道了SQL语句
            ps = conn.prepareStatement(sql);
            //3.填充占位符
            ps.setObject(1,"Jerry");
            ps.setObject(2,19);
            //4.执行
            ps.execute();
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            //5.资源的关闭
            JDBCUtils.closeResource(conn,ps);
        }

    }



    // 向customers表中添加一条记录
    @Test
    public void testInsert()  {
        InputStream is = null;
        Connection connection = null;
        PreparedStatement ps = null;
        try {
            // 1.读取配置文件中的4个基本信息
            // 通过ClassLoader类中的静态方法getSystemClassLoader()获得系统加载器
            is = ClassLoader.getSystemClassLoader().getResourceAsStream("src\\jdbc.properties");
            Properties pros = new Properties();
            pros.load(is);

            String user = pros.getProperty("user");
            String password = pros.getProperty("password");
            String url = pros.getProperty("url");
            String driverClass = pros.getProperty("driverClass");

            // 2.加载驱动
            Class.forName(driverClass);

            // 3.获取连接
            connection = DriverManager.getConnection(url, user, password);

            //4.预编译sql语句，返回PreparedStatement的实例
            String sql = "insert into customers(name,email,birth) values(?,?,?)";//?:占位符
            ps = connection.prepareStatement(sql);
            //5.填充占位符
            // 与MySQL数据库交互的所有API所涉及到的索引值均是从1开始:1表示第一个索引值
            ps.setString(1,"Tom");
            ps.setString(2,"tom@edu.com");
            SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
            Date date = sdf.parse("1999-09-09");
            ps.setDate(3,new java.sql.Date(date.getTime()));// Date类实例化:需要传入一个long类型的毫秒值

            //6.执行操作
            ps.execute();
        } catch  (Exception e) {
            e.printStackTrace();
        } finally {
            //7.资源的关闭
            try {
                if (ps!= null)
                    ps.close();
            } catch (SQLException e) {
                e.printStackTrace();
            }
            try {
                if (connection!= null)
                    connection.close();
            } catch (SQLException e) {
                e.printStackTrace();
            }
            try {
                if (is!= null)
                    is.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }

    }

}
