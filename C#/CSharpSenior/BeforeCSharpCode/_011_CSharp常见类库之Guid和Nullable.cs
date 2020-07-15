using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpSenior {
    class _011_CSharp常见类库之Guid和Nullable {

        static void Main() {

            #region 创建一个 Guid

            /*
            Guid(Globally Unique Identifier) 全局唯一标识，是一种由算法生成的二进制长度为128位的字符串，
            但字符串的长度是36其中32位16进制的数字和四个连接符。其作用是用来表示全局唯一标识，当多个系统或者数据量大的时候，
            用来做唯一标识，比如说数据库的主键。Guid并不是C#独有的，所以可以放心使用，不用担心跟其他系统交互时遇到对方无法识别的尴尬局面。

            Guid应用非常广泛，如果有查看过Windows系统注册表的同学应该见过如下类型的数据：efa4bcc8-b293-48d5-9278-924b9c015b97,
            这就是Guid。Guid甚至被Windows用作组件注册，网络接口标识等。

            简单来讲，Guid适合需要不重复标识的场景。
            */

            Guid guid = Guid.NewGuid();
            //Console.WriteLine(guid);
            /*
            多次运行以上代码将会出现不同的结果
            到这里，创建Guid就可以认为达到目的了，但是我们一起来看下Guid有哪些构造函数吧:
                public Guid(byte[] b);
            用长度为16的字节数组初始化一个Guid，其中Guid的值与字节数组相关。
            （根据定义来理解，C#会将字节数组b转换为128位的二进制数据，再转换为字符串格式）。
            */

            var bytes = new byte[16] {
                12,32,59,29,93,22,22,19,45,37,53,38,54,46,33,11
            };
            Guid guid1 = new Guid(bytes);
            //Console.WriteLine(guid1);
            // 多次运行，打印结果都是：1d3b200c-165d-1316-2d25-3526362e210b


            /*
            以上可以得知，是通过一个字节数组创建一个Guid元素，这个元素的值就是这个字节数组的值。
            继续介绍第二个构造方法，通过格式化的字符串创建：
                public Guid(string g);
            g 表示 Guid 数据，有一下几种格式：
            1.dddd_dddd_dddd_dddd_dddd_dddd_dddd_dddd 表示32个连续数字
            2.dddddddd-dddd-dddd-dddd-ddddddddddddd 表示8、4、4、4和12位数字的分组，可以用小括号和大括号包裹起来
            3.{0xdddddddd,   0xdddd,      0xdddd, {0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd} }8、4和4位数组的分组
                和一个8组2位数字的子集，每组都带有前缀"0x"或"0X"，以逗号分隔
            */
            string[] guidStrings = { 
                "ca761232ed4211cebacd00aa0057b223",
                "CA761232-ED42-11CE-BACD-00AA0057B223",
                "{CA761232-ED42-11CE-BACD-00AA0057B223}",
                "(CA761232-ED42-11CE-BACD-00AA0057B223)",
                "{0xCA761232,0xED42,0x11CE,{0xBA,0xCD,0x00,0xAA,0x00,0x57,0xB2,0x23}}"
            };

            foreach (var guidString in guidStrings) {
                var guid2 = new Guid(guidString);
                //Console.WriteLine($"Original string:{guidString}");
                //Console.WriteLine($"Guid           {guid2}");
                //Console.WriteLine();
            }
            /*
            打印结果如下：
            Original string:ca761232ed4211cebacd00aa0057b223
            Guid           ca761232-ed42-11ce-bacd-00aa0057b223

            Original string:CA761232-ED42-11CE-BACD-00AA0057B223
            Guid           ca761232-ed42-11ce-bacd-00aa0057b223

            Original string:{CA761232-ED42-11CE-BACD-00AA0057B223}
            Guid           ca761232-ed42-11ce-bacd-00aa0057b223

            Original string:(CA761232-ED42-11CE-BACD-00AA0057B223)
            Guid           ca761232-ed42-11ce-bacd-00aa0057b223

            Original string:{0xCA761232,0xED42,0x11CE,{0xBA,0xCD,0x00,0xAA,0x00,0x57,0xB2,0x23}}
            Guid           ca761232-ed42-11ce-bacd-00aa0057b223
            */

            /*
            通过制定整数和字节数组初始化：
                public Guid(int a,short b,short c,byte[] d);
            其中a 表示前四个字节，也就是第一个分隔符前面的八位，b表示之后两个字节，c表示b之后的两个字节，d表示其余八个字节
            依次指定各个位置的值：
                public Guid (int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k);
            这个方法与上一个类似，不过分的更细致了，其中int四个字节，byte一个字节，与类型的实际字节长度一致。
            */

            //Console.WriteLine(new Guid(1, 2, 3, new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 }));
            // 创建对应于 "00000001-0002-0003-0001-020304050607" 的 Guid。

            #endregion

            #region 一个空的 Guid

            /*
            C# 为Guid结构体提供了一个静态只读属性：Empty，其值均为零，表示Guid的零值。很多接口或系统会为Guid
            类型的字段提供一个默认零值就是这个值，在一些业务场景中会遇到与零值的相等判断
            */

            #endregion

            #region Guid 与字符串之间一个转身

            /*
            根据Guid构造函数可以看到Guid的打印格式应该有三种，那么如何生成这三种呢？C#还有没有更多的格式支持呢？

            Guid的ToString方法有以下三个版本：
                public override string ToString();
                public string ToString(string format);
                public string ToString(string format,IFormatProvider provider);
            第一个是默认的转字符串的方法，格式在上文也有介绍。最后一个涉及到国际化，略过不提。第二个，则是用格式确定输出结果。
            C# 支持的format值和对应的意义如下：
            N           ：32位数：0000-0000-0000-0000-0000-0000-0000-0000
            D           ：32的数字，由连字符分隔：00000000-0000-0000-0000-000000000000
            B           ：32位，用连字符隔开，括在大/花括号中：{00000000-0000-0000-0000-000000000000}
            P           ：32位，用连字符隔开，括在括号中：（00000000-0000-0000-0000-000000000000）
            X           ：括在大括号中的四个十六进制值，其中第四个值是八个十六进制的子集（也括在大括号中）：
                            {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}
            如果fomat为NULL或者空字符串，则默认为D。

            这里介绍了Guid生成字符串的方法和对应的格式内容，而字符串转Guid除了使用构造函数以外还有两种方式：
                public static Guid Parse (string input);
                public static Guid ParseExact (string input, string format);
            第一个方法由C#自动解析字符串格式，第二种由调用方明确指出字符串的格式。格式仅支持N/D/B/P/X这五种。
            */

            #endregion


            #region 可空类型的使用

            /*
            我们常用的基本数据类型，包括这两篇介绍的类型除了string是类，其他都是struct类型。在C#中struct无法置为NULL，
            一般情况下并不影响程序的运行。但是，如果涉及到交互，无论是与人还是与其他的系统交互，都会出现数据不可用的情况。
            举例来说，一场数学考试，对于每个学生来说都会有一个数字类型的试卷成绩。如果有同学因为生病了缺考了，我们直接给
            他试卷上标记零分显然是不可取的，所以需要标记为NULL，意思是缺考。这时候如果在系统中简单的使用 int或者double
            存成绩就会出现NULL无法存入系统。

            C#为了解决此类问题，添加了Nullable，这是个结构体，C#为此添加了额外的支持。我们看下如何声明一个可空的int类型：
                Nullable<int> score;
            C# 除了以上的声明方式，还提供了一种特殊的语法，使用?：
                int? score;
            也就是 类型? 表示<类型> 的可空类型。

            可空类型可以跟其原类型一样正常使用，包括原类型支持的算术运算等。不过值得注意的一点是，如果可控类型的值为null，
            在和其他非null值进行计算后，最终结果只能是null。

            C# 为可空类型的值判断和读取提供了两个属性：
                public bool HasValue{ get; }
                public T Value { get; }
            如果HasValue为True，则表示Value可以正确读取到值，否则这个可控类型就是null。
            以上是Nullable的使用介绍，使用起来很简单，但是这是C#中一个很重要的地方。
            */

            #endregion




        }

    }
}
