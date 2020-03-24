package com.zlim.servlet;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

/**
 * @author zlim
 * @create 2020-03-24 21:10
 */
public class HelloServlet extends HttpServlet {

    // 由于GET或者POST只是请求实现的不同的方法，可以相互调用，因为业务逻辑都一样

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        resp.setContentType("text/html;charset=utf-8");
        // 响应流：字符流
        PrintWriter writer = resp.getWriter();
        // ServletOutputStream sos = resp.getOutputStream();// 字节流
        writer.print("hello,Servlet");

    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        doGet(req, resp);
    }
}
