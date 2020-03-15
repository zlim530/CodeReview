package com.zlim.PreparedStatement;

import com.zlim.PreparedStatement.util.JDBCUtils;
import org.junit.Test;

import java.sql.Connection;
import java.sql.PreparedStatement;

/**
 * 使用PreparedStatement实现批量数据的操作
 *
 * update、delete本身就具有批量操作的效果：即当我们执行这两个命令时如果没有添加过滤条件则所有匹配的数据都会执行相应的操作
 * 此时的批量操作，主要指的是批量插入。使用PreparedStatement如何实现更高效的批量插入？
 *
 * 题目：向goods表中插入20000条数据
 * CREATE TABLE goods(
 *    id INT PRIMARY KEY AUTO_INCREMENT,
 *    NAME VARCHAR(25)
 *    );
 * 方式一：使用Statement：不要使用了
 * Connection conn = JDBCUtils.getConnection();
 * Statement st = conn.createStatement();
 * for(int i = 1;i <= 20000;i++){
 * 		String sql = "insert into goods(name)values('name_" + i + "')";
 * 		st.execute(sql);
 * }
 *
 * @author zlim
 * @create 2020-03-15 15:48
 */
public class BatchInsertTest {

    //批量插入的方式二：使用PreparedStatement
    // PreparedStatement 接口是 Statement 的子接口，它表示一条预编译过的 SQL 语句：这就是PreparedStatement最大的特点
    @Test
    public void testBatchInsert1()  {
        Connection conn = null;
        PreparedStatement ps = null;
        try {
            long begin = System.currentTimeMillis();

            conn = JDBCUtils.getConnection();
            String sql = "insert into goods(name) values(?)";
            // 统一用一个SQL语句，只是后续填充的占位符不一样
            // DBServer会对预编译语句提供性能优化。因为预编译语句有可能被重复调用，所以语句在被DBServer的
            // 编译器编译后的执行代码被缓存下来，那么下次调用时只要是相同的预编译语句就不需要编译，只要将参
            // 数直接传入编译过的语句执行代码中就会得到执行。

            ps = conn.prepareStatement(sql);
            for (int i = 0; i < 20000; i++) {
                ps.setObject(1,"name_" + i);// 填充占位符：只有一个占位符
                ps.execute();// 执行SQL语句
            }

            long end = System.currentTimeMillis();
            System.out.println("花费的时间为：" + (end - begin) + "毫秒");//花费的时间为：79849毫秒 ~ 1.33min
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(conn,ps);
        }


    }



    /*
     * 批量插入的方式三：
     * 1.addBatch()、executeBatch()、clearBatch()
     * 2.mysql服务器默认是关闭批处理的，我们需要通过一个参数，让mysql开启批处理的支持。
     * 		 ?rewriteBatchedStatements=true 写在配置文件的url后面
     * 3.使用更新的mysql 驱动：mysql-connector-java-5.1.37-bin.jar（本来是1.7）
     */
    @Test
    public void testBatchInsert2()  {
        Connection conn = null;
        PreparedStatement ps = null;
        try {
            long begin = System.currentTimeMillis();

            conn = JDBCUtils.getConnection();
            String sql = "insert into goods(name) values(?)";
            ps = conn.prepareStatement(sql);

            for (int i = 0; i < 1_000_000; i++) {
                ps.setObject(1,"name_" + i);

                //1."攒"sql
                ps.addBatch();

                // 表示攒够500条时就执行：类似于read(byte[] buffer)字符数组的长度
                if( i % 500 == 0){
                    ps.executeBatch();
                    ps.clearBatch();
                }
            }

            long end = System.currentTimeMillis();
            System.out.println("花费的时间为：" + (end - begin) / 1000 + "秒");// 1_000_000：15秒
            // System.out.println("花费的时间为：" + (end - begin)  + "毫秒");// 20000:1139毫秒
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(conn,ps);
        }


    }


    //批量插入的方式四：设置连接不允许自动提交数据
    @Test
    public void testBatchInsert4()  {
        Connection conn = null;
        PreparedStatement ps = null;
        try {
            long begin = System.currentTimeMillis();

            conn = JDBCUtils.getConnection();
            //设置不允许自动提交数据:默认为自动提交
            conn.setAutoCommit(false);

            String sql = "insert into goods(name) values(?)";
            ps = conn.prepareStatement(sql);
            for (int i = 0; i < 1000000; i++) {
                ps.setObject(1,"name_" + i);

                ps.addBatch();

                //1."攒"sql
                if( i % 500 == 0){
                    //2.执行batch
                    ps.executeBatch();
                    //3.清空batch
                    ps.clearBatch();
                }
            }

            //统一提交数据
            conn.commit();

            long end = System.currentTimeMillis();
            System.out.println("花费的时间为：" + (end - begin) / 1000 + "秒");// 1_000_000：8秒
            // System.out.println("花费的时间为：" + (end - begin)  + "毫秒");// 20000:925毫秒
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            JDBCUtils.closeResource(conn,ps);
        }



    }



}
