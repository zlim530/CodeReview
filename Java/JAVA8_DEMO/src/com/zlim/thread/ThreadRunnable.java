package com.zlim.thread;

import com.sun.org.apache.bcel.internal.generic.NEW;

/**
 * 创建多线程的方式二：实现Runnable接口
 * 1.创一个实现了Runnable接口的类；
 * 2.实现类去实现Runnable接口中的抽象方法：run()
 * 3.创建实现类的对象
 * 4.将此对象作为参数传递给Thread类中的有参构造器，创建Thread类对象
 * 5.通过Thread类的对象调用start()方法
 *
 * @author zlim
 * @create 2020-02-28 16:54
 */

class MThread implements Runnable{

    @Override
    public void run() {
        for (int i = 0; i < 100; i++) {
            if (i % 2 == 0){
                System.out.println(i);
            }
        }
    }
}

public class ThreadRunnable {
    public static void main(String[] args) {
        MThread mThread = new MThread();
        Thread thread = new Thread(mThread);
        // 1.启动线程 2.调用当前线程的run() ---> 调用了Runnable接口中的run()方法
        thread.start();
    }
}
