package com.zlim.lambdaAndmethodref;

import org.junit.Test;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.function.Consumer;
import java.util.function.Predicate;

/**
 * java内置的4大核心函数式接口:只有一个抽象方法
 *
 * 消费型接口 Consumer<T>     void accept(T t)
 * 供给型接口 Supplier<T>     T get()
 * 函数型接口 Function<T,R>   R apply(T t)
 * 断定型接口 Predicate<T>    boolean test(T t)
 *
 * @author zlim
 * @create 2020-03-09 22:17
 */
public class FunctionalInterfaceTest {
    
    @Test
    public void testConsumer(){
        // Consumer接口的匿名实现类对象
        happyTime(500, new Consumer<Double>() {
            @Override
            public void accept(Double aDouble) {
                System.out.println("学习太累了，去天上人间买了瓶水，价格为：" + aDouble);
            }
        });

        System.out.println("*******************");

        happyTime(400, (Double aDouble) -> System.out.println("学习太累了，去天上人间喝了口水，价格为：" + aDouble));
    }
    

    public void happyTime(double money, Consumer<Double> con){
        con.accept(money);
    }
    
    
    @Test
    public void testPredicate(){
        List<String> list = Arrays.asList("北京","南京","天津","东京","西京","普京");

        List<String> filterStrs = filterString(list, new Predicate<String>() {
            @Override
            public boolean test(String s) {
                return s.contains("京");
            }
        });

        System.out.println(filterStrs);

        System.out.println("**************************");

        List<String> filterStrs1 = filterString(list, (String s) -> s.contains("京"));

        System.out.println(filterStrs1);


    }

    //根据给定的规则，过滤集合中的字符串。此规则由Predicate（断言型接口）的方法决定
    private List<String> filterString(List<String> list, Predicate<String> pre) {

        ArrayList<String> filterList = new ArrayList<>();

        for (String s : list) {
            if( pre.test(s)){
                filterList.add(s);
            }
        }

        return filterList;
    }


}
