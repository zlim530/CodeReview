package com.zlim.PreparedStatement;

import com.zlim.PreparedStatement.util.JDBCUtils;
import com.zlim.bean.Customer;
import com.zlim.bean.Order;
import org.junit.Test;

import java.lang.reflect.Field;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.util.ArrayList;
import java.util.List;

/**
 * 总结：面向接口编程的思想：1.在整个代码中看不到第三方的jar包；2.所有方法都是通过接口调用方法
 * 关于第2点的说明：
 * 		虽然下面的通用方法中均是用接口去调用方法，但是在第一个步骤获取数据库连接时(JDBCUtils.getConnection())
 * 		其实就获得了一个实现了java.sql.Driver接口的MySQL的具体的Driver接口的实现类对象
 * 		而后所有的通过接口去调用方法实际上都是通过具体实现类中重写的方法，而返回值也是相应的接口的具体实现类
 *
 *  使用PreparedStatement实现针对于不同表的通用的查询操作
 * @author zlim
 * @create 2020-03-14 23:10
 */
public class PreparedStatementQueryTest {

    @Test
    public void testGetForList(){
        String sql = "select id,name,email from customers where id < ?";
        List<Customer> list = getForList(Customer.class, sql, 12);
        // Iterable接口中的默认方法forEach(Consumer<? super T> action)：内部仍然使用for(元素变量:集合)的方法
        // 使用方法引用实现Consumer接口
        list.forEach(System.out::println);
        System.out.println(list.size());

        // 占位符由我们自行决定可以有也可以没有：占位符是用于过滤条件处，如果没有过滤条件也即如果想查所有的数据就可以不写占位符
        String sql1 = "select order_id orderId,order_name orderName from `order`";
        List<Order> list1 = getForList(Order.class, sql1);
        list1.forEach(System.out::println);
    }
    
    

    public <T> List<T> getForList(Class<T> clazz, String sql, Object... args){
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
            ArrayList<T> list = new ArrayList<T>();
            while( rs.next()){
                // Order order = new Order();
                T t = clazz.newInstance();
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
                    Field field = clazz.getDeclaredField(columnLabel);
                    field.setAccessible(true);
                    field.set(t,columnValue);
                }
                // return t;
                list.add(t);
            }
            return list;
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(conn,ps,rs);
        }

        return null;
    }


    
    @Test
    public void testGetInstance(){
        String sql = "select id,name,email from customers where id = ?";
        Customer customer = getInstance(Customer.class,sql,12);
        System.out.println(customer);

        String sql1 = "select order_id orderId,order_name orderName from `order` where order_id = ?";
        Order order = getInstance(Order.class, sql1, 1);
        System.out.println(order);
    }


    /**
     * 针对于不同的表的通用的查询操作，返回表中的一条记录
     * 这是一个泛型方法，方法声明中的 <T> 表示方法第一个形参中的T不是一个类，而是泛型参数
     * @param clazz
     * @param sql
     * @param args
     * @param <T>
     * @return
     */
    public <T> T getInstance(Class<T> clazz,String sql,Object ...args){
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
                // Order order = new Order();
                T t = clazz.newInstance();
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
                    Field field = clazz.getDeclaredField(columnLabel);
                    field.setAccessible(true);
                    field.set(t,columnValue);
                }
                return t;
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(conn,ps,rs);
        }

        return null;
    }


}
