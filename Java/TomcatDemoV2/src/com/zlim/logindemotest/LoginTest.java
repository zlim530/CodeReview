package com.zlim.logindemotest;

import com.zlim.bean.User;
import com.zlim.dao.UserDAO;
import org.junit.Test;

/**
 * @author zlim
 * @create 2020-03-18 1:29
 */
public class LoginTest {

    /**
     * 测试UserDAO中的login方法的正确性
     */
    @Test
    public void test(){
        User loginUser = new User();
        loginUser.setUsername("zlim");
        loginUser.setPassword("1234561");

        UserDAO dao = new UserDAO();
        User returnUser = dao.login(loginUser);
        System.out.println("returnUser = " + returnUser);
    }


}
