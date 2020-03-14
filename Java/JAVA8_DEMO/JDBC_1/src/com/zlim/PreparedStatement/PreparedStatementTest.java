package com.zlim.PreparedStatement;

import com.zlim.PreparedStatement.util.JDBCUtils;
import com.zlim.bean.User;
import org.junit.Test;

import java.lang.reflect.Field;
import java.sql.*;
import java.util.Scanner;

/**
 * 在讲解完PreparedStatementQueryTest后：
 * 演示使用PreparedStatement替换Statement，解决SQL注入问题
 * @author zlim
 * @create 2020-03-14 23:24
 *
 * PreparedStatement:是 Statement的一个子接口
 * 除了解决Statement的拼串、sql问题之外，PreparedStatement还有哪些好处呢？
 * 1.PreparedStatement操作Blob的数据，而Statement做不到。
 * 2.PreparedStatement可以实现更高效的批量操作。
 *
 */
public class PreparedStatementTest {

    /**
     * 使用main方法进行测试，但如果是在main()方法中测试，则相对路径即为当前的project下
     * 故在getInstance()方法中无法使用JDBCUtils.getConnection()方法获得数据库连接
     * 而是手动获取连接
     * @param args
     */
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.println("pls input username：");
        String user = scanner.nextLine();
        System.out.println("pls input password :");
        String password = scanner.nextLine();
        String sql = "select user,password from user_table where user = ? and password = ?";
        User instance = getInstance(User.class, sql, user, password);
        if( instance != null){
            System.out.println("Login successful.");
        } else{
            System.out.println("Username doesn't exits or password was wrong.");
        }
    }

    /**
     * junit不支持键盘输入
     */
    @Test
    public void testLogin(){
        Scanner scanner = new Scanner(System.in);
        System.out.print("请输入用户名：");

        String user = scanner.nextLine();
        System.out.print("请输入密码：");
        String password = scanner.nextLine();
        //SELECT user,password FROM user_table WHERE user = '1' or ' AND password = '=1 or '1' = '1'
        String sql = "SELECT user,password FROM user_table WHERE user = ? and password = ?";
        User returnUser = getInstance(User.class,sql,user,password);
        if(returnUser != null){
            System.out.println("登录成功");
        }else{
            System.out.println("用户名不存在或密码错误");
        }
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
    public static <T> T getInstance(Class<T> clazz,String sql,Object ...args){
        Connection conn = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            // conn = JDBCUtils.getConnection();
            // 1.获取Driver实现类的对象
            Class driverClass = Class.forName("com.mysql.jdbc.Driver");
            Driver driver = (Driver) driverClass.newInstance();

            // 2.提供另外三个连接的基本信息：
            String url = "jdbc:mysql://localhost:3306/test";
            String user = "root";
            String password = "";

            // 注册驱动
            DriverManager.registerDriver(driver);

            // 获取连接
            conn = DriverManager.getConnection(url, user, password);

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
