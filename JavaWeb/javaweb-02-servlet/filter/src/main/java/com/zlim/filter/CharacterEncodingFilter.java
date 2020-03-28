package com.zlim.filter;

import javax.servlet.*;
import java.io.IOException;

/**
 * @author zlim
 * @create 2020-03-28 20:27
 */
public class CharacterEncodingFilter implements Filter {
    @Override
    public void init(FilterConfig filterConfig) throws ServletException {

    }

    @Override
    public void doFilter(ServletRequest req, ServletResponse resp, FilterChain chain) throws IOException, ServletException {
        req.setCharacterEncoding("UTF-8");
        resp.setContentType("text/html;charset=utf-8");
        chain.doFilter(req,resp);
    }

    @Override
    public void destroy() {

    }
}
