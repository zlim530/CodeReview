package com.zlim;

import org.junit.Test;

import javax.lang.model.SourceVersion;
import java.util.Scanner;

/**
 * 从键盘中获取不同类型的变量：使用Scanner类
 * 1.导包：import java.util.Scanner;
 * 2.实例化Scanner：Scanner scan = new Scanner(System.in);
 * 3.调用Scanner类的相关方法：next()/ nextXxx()：来获取指定类型的变量
 *
 * 注意：
 * 需要根据相应的方法，来输入指定类型的值：如果输入的数据类型与要求的类型不匹配，则会报InputMisMatchException异常
 * 从而导致程序不正常终止
 *
 * @author zlim
 * @create 2020-03-03 23:44
 */
public class ScannerTest {

    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        System.out.println("pls input your name:");
        String name = scan.next();
        System.out.println("name = " + name);

        System.out.println("pls input your age:");
        int age = scan.nextInt();
        System.out.println("age = " + age);

        System.out.println("pls input your weight:");
        double weight = scan.nextDouble();
        System.out.println("weight = " + weight);

        System.out.println("pls tell me weather you love me(true/false):");
        boolean isLove = scan.nextBoolean();
        System.out.println("isLove = " + isLove);

        // Scanner没有对char型提供相应的方法。只能获取一个字符串
        System.out.println("pls input your gender:");
        String gender = scan.next();
        char genderChar = gender.charAt(0);
        System.out.println("genderChar = " + genderChar);


    }

    

    public static void main1(String[] args) {
        Scanner scan = new Scanner(System.in);

        while (true){
            System.out.println("请输入字符串：");
            String s = scan.nextLine();
            if ("e".equalsIgnoreCase(s) || "exit".equalsIgnoreCase(s)){
                System.out.println("程序结束。");
                break;
            }
            String date = s.toUpperCase();
            System.out.println(date);
        }

    }



}





