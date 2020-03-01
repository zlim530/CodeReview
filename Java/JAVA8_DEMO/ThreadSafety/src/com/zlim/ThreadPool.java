package com.zlim;

import java.lang.reflect.Executable;
import java.util.concurrent.Executor;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/**
 * @author zlim
 * @create 2020-03-01 16:09
 */
public class ThreadPool {
    public static void main(String[] args) {
        // ExecutorService是接口，这其实是多态
        ExecutorService service = Executors.newFixedThreadPool(10);

        service.execute(new NumberThread());

        service.shutdown();
    }
}

class NumberThread implements Runnable{

    @Override
    public void run() {
        for (int i = 0; i < 100; i++) {
            if (i % 2 == 0){
                System.out.println(i);
            }
        }
    }
}