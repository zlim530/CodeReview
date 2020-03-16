package com.zlim.dbutils;

import com.zlim.bean.Customer;
import com.zlim.util.JDBCUtils;
import org.apache.commons.dbutils.QueryRunner;
import org.apache.commons.dbutils.ResultSetHandler;
import org.apache.commons.dbutils.handlers.*;
import org.junit.Test;

import java.sql.Connection;
import java.sql.Date;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;
import java.util.Map;

/**
 * commons-dbutils 是 Apache 组织提供的一个开源 JDBC工具类库,封装了针对于数据库的增删改查操作
 * @author zlim
 * @create 2020-03-16 2:26
 */
public class QueryRunnerTest {
    //测试插入
    @Test
    public void testInsert()  {
        Connection conn = null;
        try {
            QueryRunner runner = new QueryRunner();
            conn = JDBCUtils.getConnectionByDruid();
            String sql = "insert into customers(name,email,birth) values(?,?,?)";
            int insertCount = runner.update(conn, sql, "Lisa", "lisa@edu.com", "1995-06-08");
            System.out.println("成功添加了" + insertCount + "条记录");
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(conn,null);
        }
    }


    //测试查询
    /*
     * BeanHander:是ResultSetHandler接口的实现类，用于封装表中的一条记录。
     * BeanHandler<T>:是一个泛型类
     */
    @Test
    public void testQuery(){
        Connection conn = null;
        try {
            QueryRunner runner = new QueryRunner();
            conn = JDBCUtils.getConnectionByDruid();
            String sql = "select id,name,email,birth from customers where id = ?";
            BeanHandler<Customer> handler = new BeanHandler<>(Customer.class);
            Customer customer = runner.query(conn, sql, handler, 3);
            System.out.println(customer);
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }finally{
            JDBCUtils.closeResource(conn, null);

        }

    }



    /*
     * BeanListHandler:是ResultSetHandler接口的实现类，用于封装表中的多条记录构成的集合。
     * BeanListHandler<T> ：是一个泛型类
     */
    @Test
    public void testQuery2(){
        Connection conn = null;
        try {
            QueryRunner runner = new QueryRunner();
            conn = JDBCUtils.getConnectionByDruid();
            String sql = "select id,name,email,birth from customers where id < ?";
            BeanListHandler<Customer> hander = new BeanListHandler<>(Customer.class);
            List<Customer> list = runner.query(conn, sql, hander, 23);
            list.forEach(System.out::println);
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(conn,null);
        }

    }


    /**
     * MapHander:是 ResultSetHandler<Map<String,Object>> 接口的实现类，对应表中的一条记录。
     * 将字段及相应字段的值作为map中的key和value进行存储,它不是一个泛型类
     * @throws Exception
     */
    @Test
    public void testQuery3()  {
        Connection conn = null;
        try {
            QueryRunner runner = new QueryRunner();
            conn = JDBCUtils.getConnectionByDruid();
            String sql = "select id,name,email,birth from customers where id = ?";
            MapHandler handler = new MapHandler();
            Map<String, Object> map = runner.query(conn, sql, handler, 22);
            System.out.println("map = " + map);
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(conn,null);
        }


    }



    /*
     * MapListHander:是ResultSetHandler接口的实现类，继承自AbstractListHandler<Map<String,Object>>,
     * 对应表中的多条记录,它不是一个泛型类。
     * 将字段及相应字段的值作为map中的key和value。将这些map添加到List中
     */
    @Test
    public void testQuery4(){
        Connection conn = null;
        try {
            QueryRunner runner = new QueryRunner();
            conn = JDBCUtils.getConnectionByDruid();
            String sql = "select id,name,email,birth from customers where id < ?";
            MapListHandler handler = new MapListHandler();
            List<Map<String, Object>> list = runner.query(conn, sql, handler, 23);
            list.forEach(System.out::println);
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(conn,null);
        }

    }



    /*
     * ScalarHandler:用于查询特殊值,是ResultSetHandler<Object>接口的实现类
     */
    @Test
    public void testQuery5()  {
        Connection conn = null;
        try {
            QueryRunner runner = new QueryRunner();
            conn = JDBCUtils.getConnectionByDruid();
            String sql = "select count(3) from customers";
            ScalarHandler handler = new ScalarHandler();
            Long count = (Long) runner.query(conn, sql, handler);
            System.out.println("count = " + count);
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(conn,null);
        }

    }


    @Test
    public void testQuery6()  {
        Connection conn = null;
        try {
            QueryRunner runner = new QueryRunner();
            conn = JDBCUtils.getConnectionByDruid();
            String sql = "select max(birth) from customers";
            ScalarHandler handler = new ScalarHandler();
            Date maxBirth = (Date) runner.query(conn, sql, handler);
            System.out.println("maxBirth = " + maxBirth);
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(conn,null);
        }

    }


    /*
     * 自定义ResultSetHandler的实现类
     */
    @Test
    public void testQuery7() {

        Connection conn = null;
        try {
            QueryRunner runner = new QueryRunner();
            conn = JDBCUtils.getConnectionByDruid();
            String sql = "select id,name,email,birth from customers where id = ?";
            //为什么后面的<>中要指明Customer：因为里面的handle()方法中的形参与返回值均由后面的<>菱形中的参数类型决定
            //Java10之后才可以不用写
            ResultSetHandler<Customer> handler = new ResultSetHandler<Customer>() {

                // 实际上handle()方法的返回值其实就是query()方法的返回值
                // 下面其实就是BeanHandler的实现
                @Override
                public Customer handle(ResultSet rs) throws SQLException {
//					System.out.println("handle");
//					return null;

//					return new Customer(12, "成龙", "Jacky@126.com", new Date(234324234324L));

                    if( rs.next()){
                        int id = rs.getInt("id");
                        String name = rs.getString("name");
                        String email = rs.getString("email");
                        Date birth = rs.getDate("birth");
                        Customer customer = new Customer(id, name, email, birth);
                        return customer;
                    }
                    return null;
                }
            };

            Customer customer = runner.query(conn, sql, handler, 12);
            System.out.println("customer = " + customer);
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(conn,null);
        }



    }




}
