package com.zlim.thread;

/**
 * @author zlim
 * @create 2020-02-28 15:45
 */
public class ThreadDemo {
    public static void main(String[] args) {
        new Thread() {
            @Override
            public void run() {
                for (int i = 0; i < 100; i++) {
                    if (i % 2 == 0) {
                        System.out.println(Thread.currentThread().getName() + ": " + i);
                    }
                }
            }
        }.start();

    }
}
