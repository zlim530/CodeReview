package com.zlim.daoversion2;

import com.zlim.PreparedStatement.util.JDBCUtils;

import java.lang.reflect.Field;
import java.lang.reflect.ParameterizedType;
import java.lang.reflect.Type;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;

/**
 * DAO: data(base) access object
 * 封装了针对于数据表的通用的操作：优化版
 * @author zlim
 * @create 2020-03-16 1:12
 */
public abstract class  BaseDAO<T> {

    private Class<T> clazz = null;
    // 因为在具体的实现类中我们就只会去造这个类的对象
    // 因此可以优化为在具体类的具体方法的逻辑中我们不需要显示的去传入当前类的Class，而是将
    // 父类BasDAO声明为泛型类，并通过反射获取其子类继承父类(就是它自己)时具体的泛型参数的值(也即具体的子类对象)
    // 而BaseDAO的通用方法中需要用到Class对象，因为我们在BaseDAO类内部声明了一个Class<T> 类对象 clazz
    // 并且要保证这个对象在子类对象调用之前是有值的，也即在对象之前有值,不能为null
    // 想要实现在对象之前有值,可以在类内部显示赋值 或者 通过构造器 或者 通过代码块来进行赋值
    // 因为上述这三中赋值均是在类加载时进行操作的,故在类的实例化对象之前就可以设置clazz的值

    //	public BaseDAO(){
//
//	}

    // 代码块:不能用静态代码块,因为clazz是非静态的:在静态代码中不能访问非静态对象

    {
        //获取当前BaseDAO的子类继承的父类中的泛型：核心操作!
        // 这里的this并不是表示BaseDAO类的对象,是哪个的对象调用了BaseDAO类中的这些方法,这个this就表示谁
        // 因为BaseDAO是抽象类,是不能实例化的,故这里的this其实就表示具体的子类对象调用BaseDAO类中的通用方法的那个子类对象
        // 其实这块代码也可以写在子类中,但是如果写在子类中,则每一个实现子类都要写一次,这样就会很麻烦
        Type genericSuperclass = this.getClass().getGenericSuperclass();
        ParameterizedType paramType = (ParameterizedType) genericSuperclass;

        Type[] typeArguments = paramType.getActualTypeArguments();//获取了父类的泛型参数
        clazz = (Class<T>) typeArguments[0];//泛型的第一个参数：因为这里只有一个泛型参数
        // 其实就是public class CustomerDAOImpl extends BaseDAO<Customer> implements CustomerDAO
        // 中的 BaseDAO<Customer> 的 Customer
    }


    public int update(Connection conn, String sql, Object... args) {// sql中占位符的个数与可变形参的长度相同！
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
            // 4.资源的关闭
            JDBCUtils.closeResource(null, ps);

        }
        return 0;

    }



    //通用的查询操作，用于返回数据表中的一条记录（version 2.0：考虑上事务）
    public T getInstance(Connection conn,String sql, Object... args) {
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

    //通用的查询操作，用于返回数据表中的一条记录（version 2.0：考虑上事务）
    public List<T> getForList(Connection conn, String sql, Object... args) {
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
            ArrayList<T> list = new ArrayList<>();
            while (rs.next()) {
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
                list.add(t);
            }
            return list;
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(null, ps, rs);

        }

        return null;
    }



    //用于查询特殊值的通用的方法:如使用count()函数查询等等
    // E ：这里的E不是表示有一个类叫E 而是表示泛型参数
    // 故我们需要在方法签名处加上 <E> 表示这是一个泛型方法
    public <E> E  getValue(Connection conn,String sql,Object...args)  {
        PreparedStatement ps = null;
        ResultSet rs = null;
        try {
            ps = conn.prepareStatement(sql);
            for (int i = 0; i < args.length; i++) {
                ps.setObject(i + 1,args[i]);
            }

            rs = ps.executeQuery();
            if( rs.next()){
                return (E) rs.getObject(1);
            }
        } catch (SQLException e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(null,ps,rs);// 一定记得不要关闭连接!!!!!!!
        }

        return null;
    }
}
