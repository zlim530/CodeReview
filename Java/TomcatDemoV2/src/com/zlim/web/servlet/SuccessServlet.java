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
 * @create 2020-03-18 1:52
 */
@WebServlet("/successServlet")
public class SuccessServlet extends HttpServlet {
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        //1.通过getAttribute(String name)方法传入数据属性名获得request域中的共享数据值
        User user = (User) request.getAttribute("user");

        if( user != null){
            // 设置响应编码
            response.setContentType("text/html;charset=utf-8");
            // 给页面输出(显示)一句话
            response.getWriter().write("登录成功！"+ user.getUsername()+",欢迎您");
        }
    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        this.doPost(request,response);
    }
}
