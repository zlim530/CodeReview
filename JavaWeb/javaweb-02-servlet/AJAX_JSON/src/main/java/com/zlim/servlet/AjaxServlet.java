package com.zlim.servlet;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

/**
 * @author zlim
 * @create 2020-03-30 1:39
 */
@WebServlet("/a3")
public class AjaxServlet extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String msg = "";
        String name = req.getParameter("name");
        String pwd = req.getParameter("pwd");
        if( name != null){
            if( "admin".equals(name)){
                msg = "ok";
            } else {
                msg = "用户名错误";
            }
        }
        req.getSession().setAttribute("name",msg);
        if( pwd != null){
            if( "123456".equals(pwd)){
                msg = "ok";
            } else {
                msg = "密码错误";
            }
        }
        req.getSession().setAttribute("pwd",msg);
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        doGet(req, resp);
    }
}
