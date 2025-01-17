package com.zlim.servlet;

import com.zlim.util.Constant;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

/**
 * @author zlim
 * @create 2020-03-28 21:55
 */
@WebServlet("/logout")
public class LogoutServlet extends HttpServlet {

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        // Object user_session = req.getSession().getAttribute("USER_SESSION");
        // System.out.println(req.getSession().getAttribute("USER_SESSION"));
        //
        // if( user_session != null){
        //     req.getSession().removeAttribute("USER_SESSION");
        //     resp.sendRedirect("/filter/login.jsp");
        //     System.out.println(req.getSession().getAttribute("USER_SESSION"));
        //
        // }else {
        //     resp.sendRedirect("/filter/login.jsp");
        // }

        // 移除用户的Session：通过属性名Constants.USER_SESSION
        req.getSession().removeAttribute("USER_SESSION");
        // 返回登录页面：req.getContextPath()动态获取项目路径：即项目虚拟目录
        resp.sendRedirect(req.getContextPath() + "/login.jsp");
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        doGet(req, resp);
    }
}
