package com.zlim.filter;

import javax.servlet.*;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

/**
 * @author zlim
 * @create 2020-03-28 22:01
 */
// @WebFilter("/sys/*")
public class SysFilter implements Filter {
    @Override
    public void init(FilterConfig filterConfig) throws ServletException {

    }

    @Override
    public void doFilter(ServletRequest req, ServletResponse resp, FilterChain filterChain) throws IOException, ServletException {
        System.out.println("Filter is doing ... ");
        HttpServletRequest request = (HttpServletRequest) req;
        HttpServletResponse response = (HttpServletResponse) resp;

        if( request.getSession().getAttribute("USER_SESSION") == null){
            System.out.println(request.getContextPath());// 就是项目的虚拟目录，在这里就是/filter
            response.sendRedirect("/filter/error.jsp");
        }else {
            // 一定不要传错了，要传送ServletRequest对象和ServletResponse对象
            filterChain.doFilter(req,resp);
        }
    }

    @Override
    public void destroy() {

    }
}
