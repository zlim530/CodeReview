using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpSenior {

    /*
    DateTime表示时间点，TimeSpan表示 时间段。在C#中，这两个都是结构体，结构体与类都能继承并实现接口，
    但是与类不同的是结构体在内存中存放在栈里。


    */
    class _010_CSharp常见类库介绍之日期时间类 {

        static void Main() {

            #region 1.创建一个 DateTime

            //DateTime time = new DateTime(2018, 1, 20);// 2018/1/20 0:00:00
            //DateTime time2 = new DateTime(2019, 11, 30, 12, 01, 59);// 2019/11/30 12:01:59
            //DateTime time3 = DateTime.Now;// 表示代码运行到这里的系统时间，一般意义上的当前时间
            ///*
            //需要注意的是，DateTime.Now在每次运行结果都不一样，这个属性返回的是系统运行时当前时间，而不是程序编写时的时间。
            //与之对应的还有一个DaTime.Year，这个属性返回结果是当天，也就是Now去掉时分秒的时间
            //*/
            //Console.WriteLine(time);
            //Console.WriteLine(time2);
            //Console.WriteLine(time3);
            /*
            2018/1/20 0:00:00
            2019/11/30 12:01:59
            2020/5/6 18:40:56
            */

            #endregion


            #region 日期变更

            /*
            DateTime提供了很多可以变更日期的方法，这些方法可以获取一个计算之后的时间值：
            public DateTime AddDays (double value);// 计算天数，正数代表 天数增加，负数代表 天数减少
            public DateTime AddHours (double value);// 返回一个新的 DateTime，它将指定的小时数加到此实例的值上。
            public DateTime AddMilliseconds (double value);//返回一个新的 DateTime，它将指定的毫秒数加到此实例的值上。
            public DateTime AddMonths (int months);//返回一个新的 DateTime，它将指定的月数加到此实例的值上
            public DateTime AddSeconds (double value);/// 返回一个新的 DateTime，它将指定的秒数加到此实例的值上。
            public DateTime AddTicks (long value);// 返回一个新的 DateTime，它将指定的刻度数加到此实例的值上，也就是构造函数里的ticks
            public DateTime AddYears (int value);//返回一个新的 DateTime，它将指定的年份数加到此实例的值上。

            以上是DateTime类对日期计算的支持，其中参数如果是正的表示时间后移，如果是负的则表示时间向前移。

            需要注意的一点就是，日期的变更不会在原有的DateTime元素上变更，会返回一个计算之后的日期类型。
            */

            #endregion


            #region 日期算术运算之 TimeSpan

            /*
            TimeSpan表示一个时间间隔，也就是两个DateTime之间的差值。

            TimeSpan的属性：
            public int Days { get; }//获取当前 TimeSpan 结构所表示的时间间隔的天数部分。
            public int Hours { get; }// 获取当前 TimeSpan 结构所表示的时间间隔的小时数部分。
            public int Milliseconds { get; }//获取当前 TimeSpan 结构所表示的时间间隔的毫秒数部分。
            public int Minutes { get; }//获取当前 TimeSpan 结构所表示的时间间隔的分钟数部分。
            public int Seconds { get; }//获取当前 TimeSpan 结构所表示的时间间隔的秒数部分。
            */
            //DateTime lastYear = new DateTime(2019,03,08,20,49,36);
            //DateTime now = DateTime.Now;
            //TimeSpan span = now - lastYear;
            //DateTime newTime = lastYear + span;
            //Console.WriteLine(span.Days);
            //Console.WriteLine(newTime);

            /*
            下面几组属性表示以 XXX 为单位，返回的 TimeSpan 的值，与之相对应的 TimeSpan 提供了一组 FromXXX 的方法，可以将
            double 类型的值还原成 TimeSpan。

            public double TotalDays { get; }//获取以整天数和天的小数部分表示的当前 TimeSpan 结构的值。
            public double TotalHours { get; }//获取以整小时数和小时的小数部分表示的当前 TimeSpan 结构的值。
            public double TotalMinutes { get; }//获取以整分钟数和分钟的小数部分表示的当前 TimeSpan 结构的值。
            public double TotalSeconds { get; }//获取以整秒数和秒的小数部分表示的当前 TimeSpan 结构的值。
            public double TotalMilliseconds { get; }//获取以整毫秒数和毫秒的小数部分表示的当前 TimeSpan 结构的值。

            因为TimeSpan表示时间间隔，所以TimeSpan也允许两个TimeSpan进行加法运算，并提供了一个Add(TimeSpan ts)的方法。

            这些是TimeSpan最常用的一些属性和方法。不过在使用TimeSpan中需要注意的地方是，TimeSpan计算返回的值可正可负，
            正值表示时间间隔的头在前尾在后，负值表示头在后尾在前；TimeSpan中没有提供TotalMonths这个方法，这是因为每个月
            具体有多少天不是固定值，如果想计算两个日期直接相差几个月，则需要自定义对应的计算方法了。
            */
            //Console.WriteLine(span);
            //Console.WriteLine(span.TotalDays);

            #endregion


            #region DateTime 与字符串之间的恩怨情仇

            DateTime now = DateTime.Now;
            Console.WriteLine("DateTime.Now.ToString : {0}",now.ToString());
            Console.WriteLine("DateTime.Now.ToLongDateString : {0}", now.ToLongDateString());
            Console.WriteLine("DateTime.Now.ToLongTimeString : {0}", now.ToLongTimeString());
            Console.WriteLine("DateTime.Now.ToShortDateString : {0}", now.ToShortDateString());
            Console.WriteLine("DateTime.Now.ToShortTimeString : {0}",now.ToShortTimeString());
            /*
            DateTime.Now.ToString : 2020/5/6 18:58:22
            DateTime.Now.ToLongDateString : 2020年5月6日
            DateTime.Now.ToLongTimeString : 18:58:22
            DateTime.Now.ToShortDateString : 2020/5/6
            DateTime.Now.ToShortTimeString : 18:58

            这是DateTime的默认输出结果，当然会根据系统的语言和地区等设置变化而产生不同的变化。
            为了避免这种变化，C#提供了一种日期类型的格式化模板，这里介绍几个常用的格式代表：

            yy          :年份。00~99，如果年份大于99，值保留后两位。如2020年则显示20，2019年则显示19
            yyyy        :年份，显示四位。0000~9999
            M           :月份，显示1~12
            MM          :月份，显示01~12
            d           :天，1~31（具体看月份允许的最大天数）
            dd          :与 d 一致，显示为01~31
            h           :小时，12小时制显示1~12
            hh          :与 h 一致，显示为01~12
            H           :小时，24小时制，显示0~23
            HH          :小时，24小时制，显示00~23
            m           :分钟，显示0~59
            mm          :分钟，显示00~59
            s           :秒，显示0~59
            ss          :秒，显示00~59
            f           :表示日期和时间值的十分之几秒，显示0~9
            ff          :表示百分之几秒，显示00~99

            最后的f，f的次数越多精度越细，但需要考虑系统的时钟精度。
            在实际开发中，最常用的格式是：yyyy-MM-dd HH:mm:ss，显示效果就是：2020-04-25 12:00:00。其中连接符可以根据开发需求更换。

            上述是时间转字符串，反过来也有字符串转时间。

            使用DateTime.Parse或者Convert.ToDateTime就可以将字符串转换为时间类型。
            C#并不需要在字符串转日期的时候指定字符串的显示格式，这是因为一个约定优于配置的设计理念。
            C#通过分析字符串，然后将字符串转换成对应的时间类型。当然，在正确解析不到时间的时候，C#会抛出异常。
            为此，C#提供了DateTime.TryParse方法，该方法不会抛出异常，会返回一个是否正确转换的bool值。
            它的声明如下：
            public static bool TryParse(string s,out DateTime result); 使用了 result 作为实际转换结果

            虽然C# 不用提供转换格式就可以读取，但是如果时间字符串的格式比较少见呢，或者说就想指定一个格式字符串，怎么办？
            别急，C#还提供了一个方法：

            public static DateTime ParseExact(string s,string format,IFormatProvider provider);

            不过，这个方法需要指定一个区域性的格式信息：provider。这个信息可以通过CultureInfo.CurrentCulture来获取，
            这个属性表示系统的当前区域信息
            */

            #endregion
        }
    }
}
