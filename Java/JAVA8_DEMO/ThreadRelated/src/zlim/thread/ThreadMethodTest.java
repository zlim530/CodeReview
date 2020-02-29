package zlim.thread;

/**
 * 测试Thread中的常用方法：
 * 1.start()：启动当前线程：并调用当前线程的run()方法
 * 2.run()：通常需要重写Thread类中的此方法，将创建的线程要执行的操作声明在此方法中：也即子线程中需要执行的逻辑代码
 * 3.currentThread():静态方法：返回执行当前代码的(子)线程
 * 4.getName()：获取当前线程的名字
 * 5.setName()：设置当前线程的名字
 * 6.yeild():释放当前线程的CPU执行权
 * 7.join():在线程a中调用线程b的join()，则此时线程a就进入阻塞状态，直到线程b完全执行完以后，线程a才结束阻塞状态
 * 8.stop():强制结束当前线程，现在已经过时
 * 9.sleep(long millitime):让当前线程“睡眠”指定的millitime毫秒时间。
 * 10.isAlive():判断当前线程是否存活
 *
 *
 *
 * @author zlim
 * @create 2020-02-28 15:50
 */
public class ThreadMethodTest {
    public static void main(String[] args) {
        HelloThread h1 = new HelloThread();
        h1.start();
        for (int i = 0; i < 50; i++) {
            if (i % 2 == 0){
                System.out.println(Thread.currentThread().getName() + ":"+Thread.currentThread().getPriority() + ":"+i);
            }

            if (i == 20){
                try {
                    h1.join();  // join()方法抛出了一个异常，故线程对象h1调用此方法时需要处理异常
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }
        }
    }
}


class HelloThread extends  Thread{
    @Override
    public void run() {
        for (int i = 0; i < 100; i++) {
            if (i % 2 == 0 ){
                System.out.println(Thread.currentThread().getName() + ":"+getPriority()+":" + i);
            }
        }
    }

    // public HelloThread(String name) {
    //     super(name);
    // }
}