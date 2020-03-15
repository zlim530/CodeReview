package com.zlim.transaction;

import com.zlim.PreparedStatement.util.JDBCUtils;
import com.zlim.bean.Customer;
import com.zlim.bean.User;
import org.junit.Test;
import sun.management.jdp.JdpController;

import java.lang.reflect.Field;
import java.sql.*;
import java.util.Set;

/**
 * @author zlim
 * @create 2020-03-15 23:26
 */
public class TransactionTest {

    //******************未考虑数据库事务情况下的转账操作**************************
    /*
     * 针对于数据表user_table来说：
     * AA用户给BB用户转账100
     *
     * update user_table set balance = balance - 100 where user = 'AA';
     * update user_table set balance = balance + 100 where user = 'BB';
     */
    @Test
    public void testUpdate(){
        String sql1 = "update user_table set balance = balance - 100 where user = ?";
        update(sql1,"AA");

        //模拟网络异常:则此时数据库中的信息为AA的余额为900，而BB的余额是1000，那100在这个异常中丢失了
        //故对于转账等这样的操作我们要求它一个事务，而一个事务就要求它在commit提交之前完成，如果无法完成还可以进行回滚
        //回到最初状态
        System.out.println( 10 / 0);

        String sql2 = "update user_table set balance = balance + 100 where user = ?";
        update(sql2,"BB");

        System.out.println("转账成功");

    }


    //通用的增删改操作:使用可变形参
    // 通用的增删改操作---version 1.0
    public int update(String sql,Object ...args)  {//sql中占位符的个数与可变形参的长度相同！
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
            // ps.execute();
            // executeUpdate() 返回被影响的记录数，如果没有被影响则返回0
            return ps.executeUpdate();
        } catch (Exception e) {
            e.printStackTrace();
        } finally {

            //修改其为自动提交数据
            //主要针对于使用数据库连接池的使用
            // 若此时 Connection 没有被关闭，还可能被重复使用，则需要恢复其自动提交状态
            // setAutoCommit(true)。尤其是在使用数据库连接池技术时，执行close()方法前，建议恢复自动提交状
            // 态。
            try {
                conn.setAutoCommit(true);
            } catch (SQLException e) {
                e.printStackTrace();
            }

            //5.资源的关闭
            JDBCUtils.closeResource(conn,ps);
        }
        return 0;
    }


    //********************考虑数据库事务后的转账操作*********************


    @Test
    public void testUpdateWithTx() throws Exception {
        Connection conn = null;
        try {
            conn = JDBCUtils.getConnection();
            System.out.println(conn.getAutoCommit());//true：默认是自动提交的

            //1.取消数据的自动提交
            conn.setAutoCommit(false);

            String sql1 = "update user_table set balance = balance - 100 where user = ?";
            update(conn,sql1,"AA");

            //模拟网络异常
            System.out.println(10 / 0);

            String sql2 = "update user_table set balance = balance + 100 where user = ?";
            update(conn,sql2,"BB");

            System.out.println("转账成功");

            //2.手动提交数据
            conn.commit();
        } catch (Exception e) {
            e.printStackTrace();

            try {
                //3.回滚数据：如果出现异常
                conn.rollback();
            } catch (SQLException ex) {
                ex.printStackTrace();
            }

        } finally {

            JDBCUtils.closeResource(conn,null);
        }

    }



    // 通用的增删改操作---version 2.0 （考虑上事务）：传入数据库连接，使其在同一个连接中执行多个DML操作
    public int update(Connection conn,String sql, Object... args) {// sql中占位符的个数与可变形参的长度相同！
        PreparedStatement ps = null;
        try {
            // 1.预编译sql语句，返回PreparedStatement的实例
            ps = conn.prepareStatement(sql);
            // 2.填充占位符
            for (int i = 0; i < args.length; i++) {
                ps.setObject(i + 1, args[i]);// 小心参数声明错误！！
            }
            // 3.执行
            return ps.executeUpdate();
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            // 4.资源的关闭：一定不要将conn传入！！！！ 哪里创建的连接资源就在哪里关闭，如果在这里关闭，则有可能后面的代码还要继续使用到这个数据库连接
            JDBCUtils.closeResource(null, ps);

        }
        return 0;

    }



    //**********************MySQL数据库隔离级别在Java代码中的体现*******************************
    // 只要保证数据库的隔离级别可以解决脏读问题即可，也即没有提交的数据不允许读取
    // 故隔离级别只要为 READ COMMITTED(读已提交数据)即可
    @Test
    public void testTransactionSelect() throws Exception {

        Connection conn = JDBCUtils.getConnection();
        //获取当前连接的隔离级别
        System.out.println(conn.getTransactionIsolation());// 1：TRANSACTION_READ_UNCOMMITTED
        //设置数据库的隔离级别：虽然数据库本身设置了 TRANSACTION_READ_UNCOMMITTED 读未提交级别，但是在Java代码层面我们可以通过代码设置此次数据库连接的隔离级别
        conn.setTransactionIsolation(Connection.TRANSACTION_READ_COMMITTED);// TRANSACTION_READ_COMMITTED：读已提交：只有提交之后的数据才可以读取
        //取消自动提交数据
        conn.setAutoCommit(false);

        String sql = "select user,password,balance from user_table where user = ?";
        User user = getInstance(conn, User.class, sql, "CC");

        System.out.println(user);
    }


    @Test
    public void testTransactionUpdate() throws Exception {
        Connection conn = JDBCUtils.getConnection();

        conn.setAutoCommit(false);

        String sql = "update user_table set balance = ? where user = ?";
        int rs = update(conn, sql, 52000, "CC");
        // conn.commit();

        Thread.sleep(15000);
        System.out.println("修改完成");
    }


    //通用的查询操作，用于返回数据表中的一条记录（version 2.0：考虑上事务）
    public <T> T getInstance(Connection conn,Class<T> clazz,String sql, Object... args) {
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {

            ps = conn.prepareStatement(sql);
            for (int i = 0; i < args.length; i++) {
                ps.setObject(i + 1, args[i]);
            }

            rs = ps.executeQuery();
            // 获取结果集的元数据 :ResultSetMetaData
            ResultSetMetaData rsmd = rs.getMetaData();
            // 通过ResultSetMetaData获取结果集中的列数
            int columnCount = rsmd.getColumnCount();

            if (rs.next()) {
                T t = clazz.newInstance();
                // 处理结果集一行数据中的每一个列
                for (int i = 0; i < columnCount; i++) {
                    // 获取列值
                    Object columValue = rs.getObject(i + 1);

                    // 获取每个列的列名
                    // String columnName = rsmd.getColumnName(i + 1);
                    String columnLabel = rsmd.getColumnLabel(i + 1);

                    // 给t对象指定的columnName属性，赋值为columValue：通过反射
                    Field field = clazz.getDeclaredField(columnLabel);
                    field.setAccessible(true);
                    field.set(t, columValue);
                }
                return t;
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(null, ps, rs);

        }

        return null;
    }


}
