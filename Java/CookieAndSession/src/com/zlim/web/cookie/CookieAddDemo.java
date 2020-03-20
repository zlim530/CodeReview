package com.zlim.web.cookie;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.net.URLEncoder;

/**
 * @author zlim
 * @create 2020-03-21 0:22
 */
@WebServlet("/cookieAddDemo")
public class CookieAddDemo extends HttpServlet {
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        Cookie cookie = new Cookie("msg", "hello,cookie");
        System.out.println("URL编码前："+cookie.getValue());
        String value = cookie.getValue();
        String value_encode = URLEncoder.encode(value, "utf-8");
        System.out.println("URL编码后："+value_encode);
        cookie.setValue(value_encode);
        response.addCookie(cookie);

    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        this.doPost(request, response);
    }
}
