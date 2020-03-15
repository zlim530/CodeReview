package com.zlim.dao;

import com.zlim.bean.Customer;

import java.sql.Connection;
import java.sql.Date;
import java.util.List;

/**
 * 此接口用于规范针对于customers表(具体的表)的常用操作
 * 按理说我们只需要创建一个具体类继承于BaseDAO即可，但是为了编程更加规范，我们先定义具体类的接口
 * 然后再定义此具体接口的具体实现类
 *
 * @author zlim
 * @create 2020-03-16 0:36
 */
public interface CustomerDAO {

    /**
     * 将cust对象添加到数据库中
     * @param conn
     * @param cust
     */
    void insert(Connection conn, Customer cust);

    /**
     * 针对指定的id，删除表中的一条记录
     * @param conn
     * @param id
     */
    void deleteById(Connection conn,int id);

    /**
     * 针对内存中的cust对象，去修改数据表中指定的记录
     * @param conn
     * @param cust
     */
    void update(Connection conn,Customer cust);

    /**
     * 针对指定的id查询得到对应的Customer对象
     * @param conn
     * @param id
     * @return
     */
    Customer getCustomerById(Connection conn,int id);

    /**
     * 查询表中的所有记录构成的集合
     * @param conn
     * @return
     */
    List<Customer> getAll(Connection conn);

    /**
     * 返回数据表中的数据的条目数：count(*) 的返回值是Long(long的包装类)类型的
     * @param conn
     * @return
     */
    Long getCount(Connection conn);

    /**
     * 返回数据表中最大的生日：不同业务逻辑需要实现的不同的功能
     * @param conn
     * @return
     */
    Date getMaxBirth(Connection conn);


}
