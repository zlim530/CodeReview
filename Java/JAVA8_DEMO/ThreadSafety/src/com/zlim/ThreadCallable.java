package com.zlim;

import java.util.concurrent.Callable;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.FutureTask;

/**
 * @author zlim
 * @create 2020-03-01 16:03
 */

class CallableThread implements Callable{

    @Override
    public Object call() throws Exception {
        int sum = 0;
        for (int i = 1; i <= 100; i++) {
            if ( i % 2 == 0){
                System.out.println(i);
                sum += i;
            }
        }
        return sum;
    }
}

public class ThreadCallable {
    public static void main(String[] args) {
        CallableThread callableThread = new CallableThread();
        FutureTask futureTask = new FutureTask(callableThread);
        new Thread(futureTask).start();

        try {
            Object o = futureTask.get();
            System.out.println("sum is " + o);
        } catch (InterruptedException e) {
            e.printStackTrace();
        } catch (ExecutionException e) {
            e.printStackTrace();
        }

    }
}
