package com.zlim.dao;

import com.zlim.bean.User;
import com.zlim.util.JDBCUtils;
import org.springframework.dao.DataAccessException;
import org.springframework.jdbc.core.BeanPropertyRowMapper;
import org.springframework.jdbc.core.JdbcTemplate;

/**
 * @author zlim
 * @create 2020-03-18 1:03
 */
public class UserDAO {

    // 声明JDBCTemplate对象共用
    private JdbcTemplate template = new JdbcTemplate(JDBCUtils.getDataSource());

    /**
     * 登录方法
     * @param loginUser 只有用户名与密码
     * @return user包含用户的全部数据，如果没有查询到，则返回null
     */
    public User login(User loginUser){
        try {
            // 1.编写sql
            String sql = "select id,username,password from user_demo where username = ? and password = ?";

            //2.调用query()方法
            User user = template.queryForObject(sql,
                    new BeanPropertyRowMapper<User>(User.class), loginUser.getUsername(), loginUser.getPassword());
            return user;
        } catch (DataAccessException e) {
            e.printStackTrace();// 如果有异常通常是记录日志
            return null;
        }

    }



}
