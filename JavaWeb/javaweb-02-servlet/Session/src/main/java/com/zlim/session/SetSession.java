package com.zlim.session;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;

/**
 * @author zlim
 * @create 2020-03-27 20:16
 */
@WebServlet("/set")
public class SetSession extends HttpServlet {

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        req.setCharacterEncoding("UTF-8");
        resp.setContentType("text/html;charset=utf-8");

        HttpSession session = req.getSession();

        session.setAttribute("msg","hello,i'm msg from SetSession.");

        String id = session.getId();

        if( session.isNew() ){
            resp.getWriter().write("Session 创建成功，ID为:" + id);
        } else {
            resp.getWriter().write("Session 已经在服务器中存在，ID为:" + id);
        }

    }

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        doGet(req, resp);
    }
}
