package com.zlim;

import redis.clients.jedis.Jedis;

/**
 * @author zlim
 * @create 2020-07-22 2:04
 */
public class TestPing {
    public static void main(String[] args) {
        // 1. new Jedis 对象即可
        Jedis jedis = new Jedis("127.0.0.1", 6379);
        // 2. jedis 中的所有命令就是我们之前学习的所有指令！
        System.out.println(jedis.ping());
    }
}
