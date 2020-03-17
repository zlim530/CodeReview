package com.zlim.web.servlet;

import com.zlim.bean.User;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

/**
 * @author zlim
 * @create 2020-03-18 1:48
 */
@WebServlet("/failServlet")
public class FailServlet extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        // super.doGet(req, resp);
        this.doPost(req,resp);
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
       // 设置编码
       resp.setContentType("text/html;charset=utf-8");
       //输出
       resp.getWriter().write("登录失败，用户名或密码错误");

    }


}
