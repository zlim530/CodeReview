package com.zlim.servlet;

import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.Properties;

/**
 * @author zlim
 * @create 2020-03-25 0:41
 */
@WebServlet("/set")
public class HelloServlet  extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        // super.doGet(req, resp);

        // this.getInitParameter()  初始化参数
        // this.getServletConfig()  Servlet配置
        // this.getServletContext() Servlet上下文
        ServletContext servletContext = this.getServletContext();
        String name = "lim";
        // 将一个数据保存在ServletContext中，名字为：name，值为：lim
        servletContext.setAttribute("name",name);
        // new Properties();

        System.out.println("Hello,Servlet.");
    }
}
