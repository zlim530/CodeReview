package com.zlim;

import org.junit.Test;

import java.time.*;
import java.time.format.DateTimeFormatter;
import java.time.format.FormatStyle;
import java.time.temporal.TemporalAccessor;

/**
 * @author zlim
 * @create 2020-03-01 17:45
 */
public class JDK8DateTimeTest {

    @Test
    public void test1(){
        LocalDate localDate = LocalDate.now();
        LocalTime localTime = LocalTime.now();
        LocalDateTime localDateTime = LocalDateTime.now();

        System.out.println(localDate);
        System.out.println(localTime);
        System.out.println(localDateTime);

        // of():设置指定的年 月 日 时  分 秒，没有偏移量
        LocalDateTime localDateTime1 = LocalDateTime.of(2020, 10, 3, 15, 56, 7);
        System.out.println(localDateTime1);

        // withXxx()：设置相关的属性
        // 体现了不可变性
        LocalDate localDate1 = localDate.withDayOfMonth(22);
        System.out.println(localDate1);
        System.out.println(localDate);

        LocalDateTime localDateTime2 = localDateTime.plusHours(2);
        System.out.println(localDateTime2);
        System.out.println(localDateTime);
    }

    @Test
    // 因为中国是东八区
    public  void  test2(){
        Instant instant = Instant.now();
        System.out.println(instant);    // 获得的是当前本初子午线的时间:2020-03-01T10:12:35.524Z

        // 添加时间的偏移量
        OffsetDateTime offsetDateTime = instant.atOffset(ZoneOffset.ofHours(8));
        System.out.println(offsetDateTime); // 2020-03-01T18:12:35.524+08:00

        // 获取对应的毫秒数：从1970年1.1 00:00:00(UTC)开始
        long l = instant.toEpochMilli();
        System.out.println(l);  // 1583057664566

        Instant instant1 = Instant.ofEpochMilli(1583057664566L);
        System.out.println(instant1);   // 2020-03-01T10:14:24.566Z

    }

    @Test
    public void test3(){
        //  java.time.format.DateTimeFormatter 类：该类提供了三种格式化方法：
        //  预定义的标准格式。如：
        //      ISO_LOCAL_DATE_TIME;ISO_LOCAL_DATE;ISO_LOCAL_TIME
        DateTimeFormatter formatter = DateTimeFormatter.ISO_LOCAL_DATE_TIME;
        // 格式化：日期 --> 字符串
        LocalDateTime localDateTime = LocalDateTime.now();
        String str1 = formatter.format(localDateTime);
        System.out.println(localDateTime);
        System.out.println(str1);

        // 解析：字符串 --> 日期
        TemporalAccessor parse = formatter.parse(str1);
        System.out.println(parse);
        //  本地化相关的格式。如：ofLocalizedDateTime(FormatStyle.LONG)
        DateTimeFormatter formatter1 = DateTimeFormatter.ofLocalizedDateTime(FormatStyle.LONG);
        String str2 = formatter1.format(localDateTime);
        System.out.println(str2);   // 20-3-1 下午6:30:SHORT  2020年3月1日 下午06时31分07秒:LONG


        //  自定义的格式。如：ofPattern(“yyyy-MM-dd hh:mm:ss”)
        DateTimeFormatter formatter3 = DateTimeFormatter.ofPattern("yyyy-MM-dd hh:mm:ss");
        String str4 = formatter3.format(localDateTime);
        System.out.println(str4);   // 2020-03-01 06:34:32
        TemporalAccessor parse1 = formatter3.parse(str4);
        System.out.println(parse1);
    }


}
























