package com.zlim.thread;

// 方式一：通过继承的方式来创建线程
class MyThread extends Thread {
    // 将此线程执行的操作声明在run()方法中
    @Override
    public void run() {
        for (int i = 0; i < 100; i++) {
            if (i % 2 == 0) {
                System.out.println(i);
            }
        }
    }
}

public class ThreadTest {

    public static void main(String[] args) {
        // 主线程创建Thread类的子类对象：与即主线程创建了子线程对象
        MyThread thread = new MyThread();
        // 通过此对象调用start()：1.启动当前线程 2.调用子线程中的run()方法
        thread.start();
        // 不能通过直接调用子线程对象的run()的方式启动线程
        // 并且不能通过重复调用同一子线程对象的start()方法来再次创建新线程
        // thread.start(); // 否则会抛出IllegalThreadStateException异常
        // 以下操作仍然在main线程也即主线程中执行
        System.out.println("hello");
    }
}
