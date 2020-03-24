package com.zlim.servlet;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

/**
 * @author zlim
 * @create 2020-03-25 1:49
 */
@WebServlet("/gp")
public class GetProperties extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        InputStream is = this.getServletContext().getResourceAsStream("/WEB-INF/classes/com/zlim/servlet/dbjava.properties");

        Properties prop = new Properties();
        prop.load(is);

        String username = prop.getProperty("username");
        System.out.println("username = " + username);
        String password = prop.getProperty("password");
        System.out.println("password = " + password);

        resp.getWriter().print(username + ":"+password);
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        doGet(req, resp);
    }
}
