package com.zlim;

import java.util.Scanner;

public class HelloWorld {

    public static void main(String[] args) {
        String s1 = "hello";
        String s2 = "hello";
        String s3 = new String("hello");
        System.out.println(s1 == s2); // true
        System.out.println(s1 == s3); // false
        System.out.println(s2 == s3); // false
//        ctrl + alt + v :快速自动补全新建对象右边左边部分
        Scanner scanner = new Scanner(System.in);
        String str = scanner.nextLine();
        System.out.println(str);

//        System.out.println("Hello,world!");
//        Student stu = new Student();
//        stu.setAge(22);
//        stu.setName("ZLim");
//
//        Student stu2 = new Student(22,"YG");
//        System.out.println("Name:" + stu.getName() + " Age:" + stu.getAge());
//        System.out.println("Name:" + stu2.getName() + " Age:" + stu2.getAge());
//
//        for (int i = 0; i < 5; i++) {
////          因为每次循环，都是一次新的变量c
//            final int c = i;
//            System.out.println(i);
//        }
//        System.out.println("======================");
        int[] faces = new int[]{2};
        int n = 3;
//        System.out.println(coinChange1(faces,n));

//        Double d = 10.0;
//        if (d > 10)
//            System.out.println("d > 10");
//        else
//            System.out.println("d <= 10");

    }

    public static int coinChange1(int[] coins, int amount) {
        if ( amount < 1 || coins == null || coins.length == 0) {
            return -1;
        }
        int[] dp = new int[amount + 1];
        for (int i = 1; i < dp.length; i++) {
            int min = Integer.MAX_VALUE;
            for (int coin : coins) {
                if ( i < coin) {
                    continue;
                }
                int v = dp[i - coin];
                if ( v < 0 || v >= min) {
                    continue;
                }
                // min = Math.min(dp[i - coin], min);
                min = v;
            }
            // 来到这里时，如果min == Integer.MAX_VALUE，则表示兑换的零钱比所有存在的零钱都要小
            // 也faces = [2,3,5] 而amount = 1：这种情况，则无法兑换零钱
            if( min == Integer.MAX_VALUE){
                dp[i] = -1;
            } else{
                dp[i] = min + 1;
            }
        }
        return dp[amount];
    }

}
