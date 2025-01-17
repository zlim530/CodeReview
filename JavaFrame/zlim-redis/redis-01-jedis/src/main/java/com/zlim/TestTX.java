package com.zlim;

import com.alibaba.fastjson.JSONObject;
import redis.clients.jedis.Jedis;
import redis.clients.jedis.Transaction;

/**
 * @author zlim
 * @create 2020-07-23 16:41
 */
public class TestTX {
    public static void main(String[] args) {
        Jedis jedis = new Jedis("127.0.0.1", 6379);
        jedis.flushDB();
        JSONObject jsonObject = new JSONObject();
        jsonObject.put("hello","world");
        jsonObject.put("name","zlim");
        // 开启事务
        Transaction multi = jedis.multi();
        String result = jsonObject.toJSONString();
        // jedis.watch(result);
        try {
            multi.set("user1",result);
            multi.set("user2",result);
            // int i = 1/0;// 模拟代码抛出异常，事务执行失败
            multi.exec();// 执行事务
        } catch (Exception e) {
            multi.discard();// 放弃事务
            e.printStackTrace();
        } finally {
            System.out.println(jedis.get("user1"));
            System.out.println(jedis.get("user2"));
            jedis.close();// 关闭连接
        }

    }
}
