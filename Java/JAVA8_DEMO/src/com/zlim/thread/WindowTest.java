package com.zlim.thread;


/**
 * @author zlim
 * @create 2020-02-28 16:44
 */

class Window extends Thread{
    private static int ticket = 100;

    @Override
    public void run() {
        while (true){
            if (ticket > 0 ){
                System.out.println(getName() + ":sell tickets ,number is " + ticket);
                ticket--;
            }else {
                break;
            }
        }
    }
}


public class WindowTest {
    public static void main(String[] args) {
        Window w1 = new Window();
        Window w2 = new Window();
        Window w3 = new Window();

        w1.setName("Window1");
        w2.setName("Window2");
        w3.setName("Window3");

        w1.start();
        w2.start();
        w3.start();
    }
}
