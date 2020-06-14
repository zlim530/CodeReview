using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

/**
 * @author zlim
 * @create 2020/6/14 18:58:55
 */

/*
正则表达式入门：天堂
- 正则表达式是用来进行文本处理的技术，是语言无关的，在几乎所有语言中都有实现
 【正则表达式时对文本、字符串操作的】
- 一个正则表达式就是由普通字符与特殊字符（称为元字符）组成的文字模式。该模式描述在
 查找文字主体时待匹配的一个或多个字符串。正则表达式作为一个模板，将某个字符模式与
 所搜索的字符串进行匹配，正则表达式用来描述字符串的特征。
- 就像通配符".jpg"、"%ab%"，它是对字符串进行匹配的特殊字符串
- 正则表达式非常的复杂，正则表达式可以用来做：字符串的匹配、字符串的提取、字符串的替换，掌握常用的正则表达式用法

元字符：
    . ：匹配除 \n 以外的任意的单个字符，必须有字符
       b.g：big、bug、b g
   []：字符组：匹配表示在字符组中罗列出来的字符，任意取单个
       可以在括号中使用连字符"-"来指定字符的区间来简化表示，如：
       [0-9]：可以匹配任何数字字符
       [a-zA-Z]：可以匹配任何大小写字母
       [a-zA-Z0-9]：可以匹配任何大小写字母或者数字
       [a-z]
       [A-Z]
       [.]：. 出现在字符组中表示一个普通的 . 而不并不是一个元字符
       - 出现在[]中的非第一个字符时，认为是“元字符”，表示范围
       而 | 元字符、()、{}、+、*、? 等在[]中也只是普通的字符
   |：表示“或”的意思，其优先级最低，注意 ^ $ 的使用
       z|food：表示匹配 z 或者 food
       (z|f)ood：表示匹配 zood 或者 food
   ()：具有改变优先级或者定义“提取组”的作用
       将()之间括起来的表达式定义为“组”(group)，并且将匹配这个表达式的字符保存到一个临时区域，这个元字符在字符串提取的时候非常有用，可以把一些字符作为一个整体进行操作。

   限定符：限定前面的正则表达式的次数
   *：表示限定 * 前面的字符出现0次或者多次，等价于{0,}
       a.*b：ab、axb、axxxxxxb、axxb
       z(ab)*：z、zab、zabab(用括号定义提取组，因此此时 ab 字符作为了一个组进行0次或多次次数匹配)
   +：表示限定 + 前面的字符出现一次或者多次，至少出现一次，等价于{1,}
       a.+b：axb、axxxb、axyb 
       注意 ab 是不满足的，因为 + 前面是 . 字符，而 . 字符表示除 \n 之外的任意单个字符，因此只要除了 \n 之后的任意字符出现在 ab 之间即可
   ?：表示 ? 前面的字符可以出现0次或者1次，等价于{0,1}
       a.?b：ab、axb、ayb、a#b
       注意：axbb 整体来说不满足，因为 . 只能匹配单个字符，而在这里 a 与 b 之间有 xb 两个字符
   {m}：限定 {m} 前面的字符出现固定 m 次
       [0-9]{8}：00000000、12345678
   {m,}：限定 {m,} 前面的字符至少出现 m 次
   {m,n}：限定 {m,n} 前面的字符至少出现 m 次，最多出现 n 次
       h[0-9]{3,5}l：h123l、h1234l、h12345l

元字符：
   ^：匹配一行的开始，表示必须以 ^ 后面的字符开头，当它出现在字符组 [] 中时表示除了 ^ 后面字符以外的任意单个字符
       ^王..：表示必须以字符王开头，并且后面有两个任意的字符
       a[^xyz]b：表示除了 xyz 字符以外的任意字符：acb
       注意：ab 不满足，因为 a 字符与 b 字符之间至少需要一个字符
       ^ 的另一种意思：非!：[^0-9]：表示除了数字0到9以外的任意字符
   $：匹配行结束符，表示必须以 $ 前面的字符开头

简写表达式：注意这些简写表达式时不考虑转义的，这里的 \ 就表示字符 \ ，而不是 C# 字符串中的 \ 。在 C# 代码中需要使用@或者双重转义，注意区分 C# 级别的转义与正则表达式的转义：其实只是恰好 C# 的转义符和正则表达式的转义符都是 \ 而已。
   d：digit；s：space；w：word
   在 C# 中在字符串前面使用 @ 符号，字符串中可以使用两个双引号表示一个双引号，例如：Console.WriteLine(@"""");
   \d：表示数字，等价于[0-9]
   \D：表示除了数字[0-9]以外的所有字符，等价于[^0-9]
   \w：表示[a-zA-Z0-9_]字符
       匹配字母或数字或下划线或汉字，即能组成单词的字符，如果通过 ECMAScript 选项指定了符合 ECMAScript 的行为，则 \w 等效于[a-zA-Z0-9_]
   \W：表示除了[a-zA-Z0-9_]以外的任意字符，即非 \w ，等价于[^\w]
   \s：表示换行符、Tab 制表符、空白字符等所有不可见字符，例如 \r\n
   \S：表示所有非空白的字符(a0%$@)
       下列正则表达式均表示任意的单个字符，注意 . 并不表示任意单个字符，因为 . 不能表示 \n
       [\s\S]
       [\d\D]
       [\w\W]

.NET 中的正则表达式：
- 正则表达式在 .NET 就是用字符串表示，无论这个字符串格式的多么特殊，在 C# 语言看来都是普通的字符串，具体的含义由 Regex 类内部进行语法分析。
- 观察字符串，自己写正则表达式之前先仔细观察字符串，找规律，根据规律写出相应的正则表达式。
- 正则表达式（Regular Expression）的主要类：Regex
- 常用的3种情况：
   · 判断是否匹配：Regex.IsMatch("字符串","正则表达式");
   · 字符号提取：Regex.Match("字符串","要提取的字符串的正则表达式");
     注意：此方法只能提取一个(提取一次)
   · 字符串提取（循环提取所有）：Regex.Matches("字符串","要提取的字符串的正则表达式");可以提取所有匹配的字符串
   · 字符串替换：Regex.Replace("字符串","正则表达式","替换内容");
   · Regex.Split();


*/
namespace 正则表达式 {
    public class ITCastDotNet006 {
        /// <summary>
        /// 判断是否为身份证号码
        /// </summary>
        /// <param name="args"></param>
        public static void Main0(string[] args) {
            /*
            Regex.IsMatch();用来判断给定的字符号是否匹配某个正则表达式
            Regex.Match();用来从给定的字符串中按照正则表达式的要求提取【一个】匹配的字符串
            Regex.Matches();用来从给定的字符串中按照正则表达式的要求提取【所有】匹配的字符串
            Regex.Replace();替换所有正则表达式匹配的字符串为另外一个字符串
            */

            #region 判断是否为身份证号码
            // 1.长度为15位或者18的字符串，首位不能是0
            // 2.如果是15位，则全部是数字
            // 3.如果是18位，则前17位都是数字，末位可能是数字也可能是 X/x

            while (true) {
                Console.WriteLine("输入身份证号：");
                string idStr = Console.ReadLine();
                // 15位：[1-9]\d{14}
                // 18位：[1-9]\d{16}[0-9xX]
                Console.WriteLine(Regex.IsMatch(idStr, @"^([1-9]\d{16}[0-9Xx]|[1-9]\d{14})$"));
                // ^ $ 表示全匹配，而不是字符串中的一部分匹配就返回 True，一般在字符串匹配中都会使用 ^ $
                // 写法2：
                Console.WriteLine(Regex.IsMatch(idStr, @"^[1-9]\d{16}[0-9Xx]$|^[1-9]\d{14}$"));
                
            }
            #endregion
        }

        /// <summary>
        /// 验证是否为合法的邮箱
        /// </summary>
        /// <param name="args"></param>
        public static void Main1(string[] args) {
            while (true) {
                Console.WriteLine("pls input a email address:");
                string email = Console.ReadLine();
                Console.WriteLine(Regex.IsMatch(email,@"^[-0-9a-zA-Z_\.]+@[a-zA-Z0-9]+(\.[a-zA-Z]+){1,2}$"));
            }
        }

        /// <summary>
        /// .NET 默认使用的是 Unicode 匹配模式
        /// </summary>
        /// <param name="args"></param>
        public static void Main2(string[] args) {
            #region .NET 默认使用的是 Unicode 匹配模式
            //string msg = "123";
            string msg = "123";
            bool b = Regex.IsMatch(msg,@"\d+");
            bool b2 = Regex.IsMatch(msg, @"[0-9]");
            bool b3 = Regex.IsMatch(msg,@"\d+",RegexOptions.ECMAScript);
            // 这个可以准确判断 ASCII 字符123，不包含 Unicode 字符的123，Unicode 字符123其实就是在输入法中使用“全角”输出1 2 3
            // 注意这种写法等价于[0-9]，即[0-9]也不包含 Unicode 字符的123
            //Console.WriteLine(b);
            //Console.WriteLine(b2);

            string msg2 = "abd1231243_ESIFDI__你好你好你好你好";
            bool r = Regex.IsMatch(msg2,@"^\w+$");
            bool r2 = Regex.IsMatch(msg2,@"^\w+$",RegexOptions.ECMAScript);
            // 在 Unicode 下的 \w 是可以包含中文字符的，但是如果指定使用 ECMAScript 则 \w 就不能包含中文字符了

            Console.WriteLine(r);// True
            Console.WriteLine(r2);// False


            #endregion
        }

        /// <summary>
        /// 判断字符串是否为正确的国内电话号码，不考虑分机
        /// </summary>
        /// <param name="args"></param>
        public static void Main3(string[] args) {
            /*
            010-8888_888或010-8888_8880或010xxx_xxxx
            0335-8888_888或0335-8888_8888（区号-电话号）0335-8888_888
            138_8888_8888(11位都是数字：手机号码)
            */
            while (true) {
                Console.WriteLine("pls input a telephone number:");
                string number = Console.ReadLine();
                // \d{3,4}-?\d{7,8} :3+7=10 3+8=11 4+7=11 4+8=12
                // \d{5}
                // \d{11}
                // 总范围：{10,12}
                bool b = Regex.IsMatch(number, @"^(\d{3,4}-?\d{7,8}|\d{5})$");
                Console.WriteLine(b);
            }
        }

        /// <summary>
        /// 匹配 IP 地址
        /// </summary>
        /// <param name="args"></param>
        public static void Main4(string[] args) {
            /* 匹配 IP 地址，4段分，分割的最多三位数字 */
            while (true) {
                Console.WriteLine("pls input a IP address:");
                string ip = Console.ReadLine();
                bool b = Regex.IsMatch(ip, @"^([0-9]{1,3}\.){3}[0-9]{1,3}$");
                Console.WriteLine(b);
            }
        }

        /// <summary>
        /// 判断是否为合法的日期格式:"2008-08-09"
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) {
            while (true) {
                Console.WriteLine("pls input a date:");
                string date = Console.ReadLine();
                bool b = Regex.IsMatch(date,@"");
            }
        }

    }
}
