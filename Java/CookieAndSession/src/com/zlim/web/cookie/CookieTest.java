package com.zlim.web.cookie;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.net.URLDecoder;
import java.net.URLEncoder;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
 *  在服务器中的Servlet判断是否有一个名为lastTime的cookie
 *  1. 有：不是第一次访问
 *      1. 响应数据：欢迎回来，您上次访问时间为:2018年6月10日11:50:20
 *      2. 写回Cookie：lastTime=2018年6月10日11:50:01
 *  2. 没有：是第一次访问
 *      1. 响应数据：您好，欢迎您首次访问
 *      2. 写回Cookie：lastTime=2018年6月10日11:50:01
 *
 * @author zlim
 * @create 2020-03-21 0:36
 */
@WebServlet("/cookieTest")
public class CookieTest extends HttpServlet {
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        response.setContentType("text/html;charset=utf-8");

        // 创建Cookie对象
        Cookie[] cookies = request.getCookies();
        boolean flag = false;
        if( cookies!= null && cookies.length > 0){
            for (Cookie cookie : cookies) {
                String name = cookie.getName();
                if( "lastTime".equals(name)){
                    flag = true;

                    // 响应数据
                    String value = cookie.getValue();
                    System.out.println("URL解码前："+value);
                    String value_encode = URLDecoder.decode(value, "utf-8");
                    System.out.println("URL解码后："+value_encode);
                    response.getWriter().write("<h1>欢迎回来，您上次的访问时间为：" + value_encode + "</h1>");

                    // 更新Cookie的值
                    Date date = new Date();
                    SimpleDateFormat sdf = new SimpleDateFormat("yyyy年MM月dd日 HH:mm:ss");
                    String str_date = sdf.format(date);
                    System.out.println("URL编码前："+str_date);
                    str_date = URLEncoder.encode(str_date, "utf-8");
                    System.out.println("URL编码后："+str_date);
                    cookie.setValue(str_date);
                    cookie.setMaxAge(60 * 60 * 24 * 15);// 半个月
                    response.addCookie(cookie);


                    break;
                }
            }

        }
        
        if( cookies == null || cookies.length == 0 || flag == false){

            // 设置Cookie的value
            Date date = new Date();
            SimpleDateFormat sdf = new SimpleDateFormat("yyyy年MM月dd日 HH:mm:ss");
            String str_date = sdf.format(date);
            System.out.println("URL编码前："+str_date);
            str_date = URLEncoder.encode(str_date, "utf-8");
            System.out.println("URL编码后："+str_date);

            // 新建Cookie对象
            Cookie lastTime = new Cookie("lastTime", str_date);
            lastTime.setMaxAge(60 * 60 * 24 * 15);// 半个月

            // 添加Cookie对象
            response.addCookie(lastTime);

            // 响应数据
            response.getWriter().write("<h1>您好，欢迎您首次访问</h1>");
        }

    }

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        this.doPost(request, response);
    }
}
