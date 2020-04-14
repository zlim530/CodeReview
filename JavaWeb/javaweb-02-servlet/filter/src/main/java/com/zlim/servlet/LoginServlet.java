package com.zlim.servlet;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

/**
 *
 * @author zlim
 * @create 2020-03-28 21:45
 */
@WebServlet("/login")
public class LoginServlet extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String username = req.getParameter("username");

        if( username.equals("admin")){
            req.getSession().setAttribute("USER_SESSION",req.getSession().getId());
            // System.out.println(req.getSession().getAttribute("USER_SESSION"));
            // System.out.println(req.getSession().getId());
            resp.sendRedirect("/filter/sys/success.jsp");
        } else{
            resp.sendRedirect("/filter/error.jsp");
        }
    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        doGet(req, resp);
    }
}
