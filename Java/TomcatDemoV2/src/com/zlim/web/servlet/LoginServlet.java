package com.zlim.web.servlet;

import com.zlim.bean.User;
import com.zlim.dao.UserDAO;
import org.apache.commons.beanutils.BeanUtils;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.Map;

/**
 * @author zlim
 * @create 2020-03-18 1:38
 */
@WebServlet("/loginServlet")
public class LoginServlet extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        //1.设置编码
        req.setCharacterEncoding("utf-8");

        // //2.通过getParameter(String name)方法传入属性名获得相应的属性值
        // String username = req.getParameter("username");
        // String password = req.getParameter("password");
        //
        // //3.封装User对象
        // User loginUser = new User();
        // loginUser.setUsername(username);
        // loginUser.setPassword(password);

        User loginUser = new User();
        //使用BeanUtils工具类快速封装JavaBean类
        Map<String, String[]> map = req.getParameterMap();
        try {
            BeanUtils.populate(loginUser,map);
        } catch (Exception e) {
            e.printStackTrace();
        }

        //4.调用UserDAO的login方法：验证用户
        UserDAO userDAO = new UserDAO();
        User user = userDAO.login(loginUser);

        //5.判断user
        if( user == null){
            //登录失败
            req.getRequestDispatcher("/failServlet").forward(req,resp);
        }else {
            //登录成功
            //存储数据:设置request域
            req.setAttribute("user",user);
            //转发请求
            req.getRequestDispatcher("/successServlet").forward(req,resp);
        }
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        this.doGet(req,resp);
    }
}
