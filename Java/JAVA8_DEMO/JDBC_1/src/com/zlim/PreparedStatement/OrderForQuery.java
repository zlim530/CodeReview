package com.zlim.PreparedStatement;

import com.zlim.PreparedStatement.util.JDBCUtils;
import com.zlim.bean.Order;
import org.junit.Test;

import java.lang.reflect.Field;
import java.sql.*;

/**
 * 针对于Order表的通用的查询操作
 * @author zlim
 * @create 2020-03-14 22:53
 */
public class OrderForQuery {
    /*
     * 针对于表的字段名与类的属性名不相同的情况：
     * 1. 必须声明sql时，使用类的属性名来命名字段的别名
     * 2. 使用ResultSetMetaData时，需要使用getColumnLabel()来替换getColumnName(),
     *    获取列的别名。
     *  说明：如果sql中没有给字段其别名，getColumnLabel()获取的就是列名
     *
     *
     */
    @Test
    public void testForCommonQuery(){
        // 给列名取别名：使得返回的结果集中的列名一定与Java代码中对应类的字段名一致：由于两者有不同的命名规范
        // 当发生列名与字段名不一致的问题时:通过给列名取别名的方式进行匹配
        String sql = "select order_id orderId,order_name orderName,order_date orderDate from `order` where order_id = ?";
        Order order = orderForQuery(sql, 1);
        System.out.println("order = " + order);

        sql = "select order_id orderId,order_name orderName,order_date orderDate from `order` where order_name = ?";
        Order order1 = orderForQuery(sql, "AA");
        System.out.println("order1 = " + order1);
    }


    /**
     * 通用的针对于order表的查询操作
     * @param sql
     * @param args
     * @return
     */
    public Order orderForQuery(String sql,Object ...args) {

        Connection conn = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            conn = JDBCUtils.getConnection();
            ps = conn.prepareStatement(sql);
            for (int i = 0; i < args.length; i++) {
                ps.setObject(i+1,args[i]);
            }

            //执行，获取结果集
            rs = ps.executeQuery();
            //获取结果集的元数据
            ResultSetMetaData rsmd = rs.getMetaData();
            //获取列数
            int columnCount = rsmd.getColumnCount();
            if( rs.next()){
                Order order = new Order();
                for (int i = 0; i < columnCount; i++) {

                    //获取每个列的列值:通过ResultSet
                    Object columnValue = rs.getObject(i + 1);

                    //通过ResultSetMetaData
                    //获取列的列名：getColumnName() --不推荐使用
                    // 给列名取别名：使得返回的结果集中的列名一定与Java代码中对应类的字段名一致：由于两者有不同的命名规范
                    // 当发生列名与字段名不一致的问题时:通过给列名取别名的方式进行匹配
                    //获取列的别名：getColumnLabel()
                    // 如果没有取别名则默认为列名
//					String columnName = rsmd.getColumnName(i + 1);
                    String columnLabel = rsmd.getColumnLabel(i + 1);

                    //通过反射，将对象指定名columnName的属性赋值为指定的值columnValue
                    Field field = Order.class.getDeclaredField(columnLabel);
                    field.setAccessible(true);
                    field.set(order,columnValue);
                }
                return order;
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(conn,ps,rs);
        }

        return null;
    }




    @Test
    public void testQuery1()  {
        Connection conn = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            conn = JDBCUtils.getConnection();
            String sql = "select order_id,order_name,order_date from `order` where order_id = ?";
            ps = conn.prepareStatement(sql);
            ps.setObject(1,1);

            rs = ps.executeQuery();
            if( rs.next()){
                int id = (int) rs.getObject(1);
                String name = (String) rs.getObject(2);
                Date date = (Date) rs.getObject(3);

                Order order = new Order(id, name, date);
                System.out.println("order = " + order);

            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(conn,ps,rs);
        }


    }

        
}
