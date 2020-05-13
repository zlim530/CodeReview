package com.zlim;

/**
 * 一、枚举类的使用：类的对象只有有限个，确定的
 * 当需要定义一组常量时，强烈建议使用枚举类
 * 如果枚举类只有一个对象，则可以作为单例模式的实现方式
 * 二、枚举类的实现
 * JDK1.5之前需要自定义枚举类
 * JDK 1.5 新增的 enum  关键字用于定义枚举类
 *
 * @author zlim
 * @create 2020-03-01 20:10
 */
public class SeasonTest {
    public static void main(String[] args) {
        Season spring = Season.SPRING;
        System.out.println(spring);
    }

    public static void main1(String[] args) {
        int n = m1();// 101
        System.out.println("n = " + n);
    }

    public static int m1(){
        int result = 100;
        try {
            result = result + 1;
            return result;
        } catch (Exception e){
            System.out.println("Exception happening ...");
        } finally{
            result = result + 1;
        }
        return result;

    }
}

class Season{

    // 声明Season对象的属性
    private final String seasonName;
    private final String seasonDesc;

    // 私有化类的构造器，并给对象属性赋值
    private Season(String seasonName,String seasonDesc){
        this.seasonName = seasonName;
        this.seasonDesc = seasonDesc;
    }

    // 提供当前枚举类的多个对象
    public  static  final Season SPRING = new Season("春天","春暖花开");
    public  static  final Season SUMMER = new Season("夏天","夏日炎炎");
    public  static  final Season AUTUMN = new Season("秋天","秋高气爽");
    public  static  final Season WINTER = new Season("冬天","冰天雪地");

    // 其他诉求：获取当前对象的属性值

    public String getSeasonName() {
        return seasonName;
    }

    public String getSeasonDesc() {
        return seasonDesc;
    }

    @Override
    public String toString() {
        return "Season{" +
                "seasonName='" + seasonName + '\'' +
                ", seasonDesc='" + seasonDesc + '\'' +
                '}';
    }
}

































