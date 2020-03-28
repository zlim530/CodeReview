package com.zlim.test;

import com.zlim.entity.Account;
import org.apache.ibatis.session.SqlSession;
import org.apache.ibatis.session.SqlSessionFactory;
import org.apache.ibatis.session.SqlSessionFactoryBuilder;

import java.io.InputStream;

/**
 * @author zlim
 * @create 2020-03-28 0:13
 */
public class Test {
    public static void main1(String[] args) {
        InputStream is = Test.class.getClassLoader().getResourceAsStream("config.xml");
        SqlSessionFactoryBuilder ssfb = new SqlSessionFactoryBuilder();
        SqlSessionFactory ssf = ssfb.build(is);
        SqlSession sqlSession = ssf.openSession();
        String statement = "com.zlim.mapper.AccountMapper.save";
        Account account = new Account(1L,"李四","123123",22);
        sqlSession.insert(statement,account);
        sqlSession.commit();
        sqlSession.close();
    }




}
