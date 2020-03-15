package com.zlim.PreparedStatement;

import com.zlim.PreparedStatement.util.JDBCUtils;
import com.zlim.bean.Customer;
import org.junit.Test;

import java.io.*;
import java.sql.*;
import java.time.format.FormatStyle;

/**
 * 测试使用PreparedStatement操作Blob类型的数据
 * @author zlim
 * @create 2020-03-15 15:23
 */
public class BlobTest {

    //向数据表customers中插入Blob类型的字段
    @Test
    public void testInsert() throws Exception {
        Connection conn = JDBCUtils.getConnection();
        String sql = "insert into customers(name,email,birth,photo)values(?,?,?,?)";

        PreparedStatement ps = conn.prepareStatement(sql);
        ps.setObject(1,"Jerry");
        ps.setObject(2,"jerry@edu.com");
        ps.setObject(3,"2000-04-08");

        FileInputStream fis = new FileInputStream(new File("mm.jpg"));
        ps.setBlob(4,fis);
        ps.execute();

        JDBCUtils.closeResource(conn,ps);

    }



    //查询数据表customers中Blob类型的字段
    @Test
    public void testQuery() {
        Connection conn = null;
        PreparedStatement ps = null;
        ResultSet rs = null;
        InputStream is = null;
        FileOutputStream fos = null;
        try {
            conn = JDBCUtils.getConnection();
            String sql = "select id,name,email,birth,photo from customers where id = ?";
            ps = conn.prepareStatement(sql);
            ps.setInt(1,21);// 填充占位符

            rs = ps.executeQuery();
            if( rs.next()){

                //			方式一：通过索引值获取结果集中的数据：这种类似于list通过索引值去查找
                //			int id = rs.getInt(1);
                //			String name = rs.getString(2);
                //			String email = rs.getString(3);
                //			Date birth = rs.getDate(4);
                //方式二：通过列名获取结果集中的数据：这种比较好：这样类似于map通过key的值去查找
                int id = rs.getInt("id");
                String name = rs.getString("name");
                String email = rs.getString("email");
                Date birth = rs.getDate("birth");

                // 将id, name, email, birth通过ORM的Customer接收
                Customer customer = new Customer(id, name, email, birth);
                System.out.println("customer = " + customer);

                //将Blob类型的字段下载下来，以文件的方式保存在本地：以流的方法来获取
                Blob photo = rs.getBlob("photo");
                is = photo.getBinaryStream();// 获取二进制输入流
                fos = new FileOutputStream("567.jpg");
                byte[] buffer = new byte[1024];
                int len;
                while( (len = is.read(buffer)) != -1){
                    fos.write(buffer,0,len);// 写到字节输出流中
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            // 资源的关闭
            try {
                if( fos!= null){
                    fos.close();
                }
            } catch (IOException e) {
                e.printStackTrace();
            }

            try {
                if( is!= null){
                    is.close();
                }
            } catch (IOException e) {
                e.printStackTrace();
            }

            JDBCUtils.closeResource(conn,ps,rs);
        }


    }

}
