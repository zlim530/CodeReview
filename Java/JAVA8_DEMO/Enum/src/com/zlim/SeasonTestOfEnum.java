package com.zlim;

/**
 * Enum类的主要方法：
 *  values() 方法：返回枚举类型的对象数组。该方法可以很方便地遍历所有的
 * 枚举值。
 *  valueOf(String str)：可以把一个字符串转为对应的枚举类对象。要求字符
 * 串必须是枚举类对象的“名字”。如不是，会有运行时异常：
 * IllegalArgumentException。
 *  toString()：返回当前枚举类对象常量的名称
 *
 * 和普通 Java 类一样，枚举类可以实现一个或多个接口
 * 若每个枚举值在调用实现的接口方法呈现相同的行为方式，则只
 * 要统一实现该方法即可。
 * 若需要每个枚举值在调用实现的接口方法呈现出不同的行为方式,
 * 则可以让每个枚举值分别来实现该方法
 *
 * @author zlim
 * @create 2020-03-01 20:22
 */
public class SeasonTestOfEnum {
    public static void main(String[] args) {
        Season1 summer = Season1.SUMMER;
        System.out.println(summer);
        System.out.println("====================");
        Season1[] values = Season1.values();
        for (int i = 0; i < values.length; i++) {
            System.out.println(values[i]);
        }

        System.out.println("====================");
        Thread.State[] values1 = Thread.State.values();
        for (int i = 0; i < values1.length; i++) {
            System.out.println(values1[i]);
        }

        System.out.println("====================");
        Season1 winter = Season1.valueOf("WINTER");
        System.out.println(winter);
        winter.show();

    }
}

interface Info{
    void  show();
}

// 使用enum关键字定义枚举类：定义的枚举类默认继承与java.lang.Enum类
enum Season1 implements Info{
    SPRING("春天","春暖花开"){
        @Override
        public void show() {
            System.out.println("spring new .");
        }
    },
    SUMMER("夏天","夏日炎炎"){
        @Override
        public void show() {
            System.out.println("summer sadness.");
        }
    },
    AUTUMN("秋天","秋高气爽"){
        @Override
        public void show() {
            System.out.println("autumn autumn...");
        }
    },
    WINTER("冬天","冰天雪地"){
        @Override
        public void show() {
            System.out.println("winter is coming.");
        }
    };

    // 声明Season对象的属性
    private final String seasonName;
    private final String seasonDesc;

    // 私有化类的构造器，并给对象属性赋值
    private Season1(String seasonName,String seasonDesc){
        this.seasonName = seasonName;
        this.seasonDesc = seasonDesc;
    }

    // @Override
    // public void show() {
    //     System.out.println("this is season.");
    // }
}
