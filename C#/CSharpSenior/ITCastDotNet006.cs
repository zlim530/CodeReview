using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

/**
 * @author zlim
 * @create 2020/6/14 18:58:55
 */

/*
正则表达式入门：天堂https://regex101.com/
- 正则表达式是用来进行文本处理的技术，是语言无关的，在几乎所有语言中都有实现
 【正则表达式时对文本、字符串操作的】
- 一个正则表达式就是由普通字符与特殊字符（称为元字符）组成的文字模式。该模式描述在
 查找文字主体时待匹配的一个或多个字符串。正则表达式作为一个模板，将某个字符模式与
 所搜索的字符串进行匹配，正则表达式用来描述字符串的特征。
- 就像通配符".jpg"、"%ab%"，它是对字符串进行匹配的特殊字符串
- 正则表达式非常的复杂，正则表达式可以用来做：字符串的匹配、字符串的提取、字符串的替换，掌握常用的正则表达式用法

元字符：
    . ：匹配除 \n 以外的任意的单个字符，必须有字符
       b.g：big、bug、b g(中间的空格不匹配)
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
/* 字符串匹配：
Regex.IsMatch(str,"^[0-9]{6}$");// 验证给定的字符串是否为一个合法的邮政编码
//注意：要想完全匹配，必须加上 ^ 和 $，否则只要字符串中有一部分与给定的正则表达式匹配都会返回 True

Regex.IsMatch(str,"^(1[0-9]|2[0-5])$");
// 匹配任意一个10(含)-25(含)的数字
// 对于这种要求，要学会找规律写正则表达式，不能像数学一个使用的大于小于等方法，而只能找规律

Regex.IsMatch(str,@"^\d{11}$");// 验证是否为合法的手机号，因为 \ 在 C# 中是转义符号，
因此在使用简写表达式时需要加上 @ 符号或者 使用 \\d

Regex.IsMatch(str,"^z|food$");// 表示要么以 z 开头要么以 food 结尾的字符串

Regex.IsMatch(str,"^(z|food)$");// 只能匹配 z 或者 food
 * 
 */
namespace 正则表达式_字符串匹配 {
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
        /// \d 既能匹配1,2...等 ASCII 数字，也可以匹配全角数字“1，2，3...”
        /// \w 既能匹配[a-zA-Z0-9_]也能匹配中文字符
        /// \s 既能匹配“英文空格”、制表符等，也能匹配“全角空格”
        /// 如果要想让只匹配 ASCII 字符，则需要指定 RegexOptions.ECMAScript 选项
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
        /// 判断是否为合法的日期格式:"2008-08-09" 粗略规律：四位数字-两位数字-两位数字
        /// </summary>
        /// <param name="args"></param>
        public static void Main5(string[] args) {
            while (true) {
                Console.WriteLine("pls input a date:");
                string date = Console.ReadLine();
                // bool b = Regex.IsMatch(date,@"^[0-9]{4}-[0-9]{2}-[0-9]{2}$");
                
                /*
                 * 限制月份只能是1-12
                 * 0[1-9]
                 * 1[0-2]
                 */
                bool b = Regex.IsMatch(date, @"^[0-9]{4}-(0[1-9]|1[0-2])-[0-9]{2}$");
                Console.WriteLine(b);
            }
        }

        
        /// <summary>
        /// 判断是否为合法的 URL 地址
        /// </summary>
        /// <param name="args"></param>
        public static void Main6(string[] args) {
            /*
             * 判断是否为合法的 URL 地址:
             * http://www.test.com/a.html?id=3&name=aaa
             * ftp://127.0.0.1/1.txt
             * 规律：字符串序列：//字符串序列
             * 可能的协议：http\https\ftp\file\thunder\ed2k
             */
            while (true) {
                Console.WriteLine("pls input a url:");
                string url = Console.ReadLine();
                bool b = Regex.IsMatch(url,@"^[a-zA-Z0-9]+://.+$");
                // 使用：^(http(s)?|ftp|file|thunder|edk2)://\w+$ 不可以，因为 \w 不包含 .
                Console.WriteLine(b);
            }
            
        }

        
        
    }
}


/*
如果想要对已经匹配的字符串再进行分组提取，就用到了“提取组”的功能
通过添加 () 就能实现提取组
在正则表达式中只要出现了 () 就表示进行了分组，() 既有改变优先级的作用又具有提取组的功能

// Regex.Match 只能提取一个匹配
Match match = Regex.Match(msg,"[0-9]");// 一般字符串提取不加 ^ 和 $
Console.WriteLine(match.Value);

// Regex.Matchs() 提取字符串中的所有匹配
MatchCollection matches = Regex.Matchs(msg,"[0-9]+");
foreach(var item in mathces){
	Console.WriteLine(item.Value);
}
 */
namespace 正则表达式_字符串提取 {
    public class Program {
        /// <summary>
        /// 提取 html 网页中的邮箱地址
        /// </summary>
        /// <param name="args"></param>
        public static void Main0(string[] args) {
            
            // string html = File.ReadAllText("1.html");
            string html = "zlim530@126.com";
            // MatchCollection matches = Regex.Matches(html, @"[-a-zA-Z_0-9.]+@[-a-zA-Z0-9_]+(\.[a-zA-z+]+)+");
            // 如果想要对已经匹配的字符串再进行分组提取，就用到了“提取组”的功能
            // 通过添加 () 就能实现提取组
            // 在正则表达式中只要出现了 () 就表示进行了分组，() 既有改变优先级的作用又具有提取组的功能
            MatchCollection matches = Regex.Matches(html, @"([-a-zA-Z_0-9.]+)@([-a-zA-Z0-9_]+(\.[a-zA-Z]+)+)");
            // (第1组)@(第2组(第三组))：第2组是包含第3组的
            foreach (Match match in matches) {
                /*
                 * match.Value:表示本次提取到的字符串
                 * match.Groups：此集合中存储的就是所有的分组信息
                 * match.Group[0].Value 与 match.Value 等价都表示本次提取到的完整的字符串，也即表示提取到整个邮箱字符串，而 match.Group[1].Value 则表示第一组的字符串 
                 */
                // Console.WriteLine(match.Value);
                Console.WriteLine($"第0组：{match.Groups[0].Value}");
                Console.WriteLine($"第1组：{match.Groups[1].Value}");
                Console.WriteLine($"第2组：{match.Groups[2].Value}");
                Console.WriteLine($"第3组：{match.Groups[3].Value}");
                /*
                第0组：zlim530@126.com
                第1组：zlim530
                第2组：126.com
                第3组：.com
                */
            }
        }

        /// <summary>
        /// 关于 C# 字符串中的 \ 转义问题与正则表达式中的 \ 转义问题
        /// </summary>
        /// <param name="args"></param>
        public static void Main1(string[] args) {
            /*
             * "\\d" -> \d
             * "\\\\d" -> \\d
             */
            // string reg = "\d"; // 因为 \ 在 C# 中是一个转义字符，因此 C# 会认为 \ 是一个转义字符，将 \ 与后面的字符组合去解析它的含义，而 \d 并不是一个转义字符，因此 C# 无法解释其含义,因此会运行时会报错：无法识别的转义序列
            string reg2 = "\\d";// 此时运行完毕后其实就是 \d，此时 C# 仍让会把 \ 认为是一个字符串的转义字符，但是 \ 后面跟的 \，即表示不再使用 \ 转义含义，而是直接使用 \ 字符本身，因此输出为 \d
            Console.WriteLine(reg2);// \d
            string reg3 = "\\\\d";// 同理，\\ 表示一个 \ 字符，那么 \\\\ 就表示 \\ 字符
            Console.WriteLine(reg3);// \\d
            bool b = Regex.IsMatch(@"\d", reg3);// True
            /*
             * 即 bool b = Regex.IsMatch(@"\d", "\\\\d");
             * 其中第一个参数为 input：表示要搜索匹配项的字符串，这里为 @"\d"，即表示待匹配字符串为 \d(@"\d" 等价于 "\\d")
             *  而第二个参数为 pattern：表示指定的正则表达式，在这里为 reg3，即为 "\\\\d"，解析的含义为 \\d，而"\\d"又相当于将 "\d" 中的 "\" 转义意义消除掉，因此此时会匹配字符 "\d" 而不会再匹配数字，因此返回结果为 True
             */             
            Console.WriteLine(b);
            bool b2 = Regex.IsMatch(@"\d", reg2);// False
            /*
             * 此时 pattern 参数为 reg2 = "\\d" 即 \d，在正则表达式中即表示匹配数字0-9，因此字符串 \d 不满足匹配，返回 False
             */
            
            Console.WriteLine(b2);
        }

        /// <summary>
        /// 提取文件中的文件名
        /// </summary>
        /// <param name="args"></param>
        public static void Main2(string[] args) {

            string path = @"C:\360极速浏览器下载\pic\wei.jpg";
            Match match = Regex.Match(path,@".+\\(.+)");
            // 此处是因为有“贪婪模式”的存在，因此正则表达式中的 \\ 一定会匹配文件路径中最后一个 \ 
            Console.WriteLine(match.Groups[1].Value);// wei.jpg

        }

        /// <summary>
        /// 练习
        /// </summary>
        /// <param name="args"></param>
        public static void Main3(string[] args) {

            #region 从"June      26     ,       1951       "中提取出月份 June、26、1951。使用 @"^([a-zA-Z]+)\s*(\d{1,2})\s*,\s*(\d{4})\s*$" 正则表达式进行匹配，其中月份和日之间必须要有空格分割，所以使用空白符号"\s"匹配所有的空白字符，此处的空格是必须有的，所以使用"+"标识为匹配1至多个空格。之后的","与年份之间的空格是可有可无的，所以使用"*"标识为匹配0至多个
            // string date = "June         26     ,       1951       ";
            // Match match = Regex.Match(date, @"([a-zA-Z]+)\s*([0-9]{2})\s*,\s*([0-9]{4})\s*");
            // //Match match = Regex.Match(date, "[a-zA-Z0-9]+");
            // //Match match = Regex.Match(date, @"^([a-zA-Z]+)\s*(\d{1,2})\s*,\s*(\d{4})\s*$");
            // //Console.WriteLine(match.Value);
            // for (int i = 0; i < match.Groups.Count; i++) {
            //     Console.WriteLine(match.Groups[i].Value);
            // }
            #endregion

            #region 从 Email 中提取出用户名与域名，如从 tim@163.com 中提取出 tim 和 163.com

            // while (true) {
            //     Console.WriteLine("pls input a email:");
            //     string email = Console.ReadLine();
            //     // 因为是从已经确认格式的 email 地址中进行提取，故我们这里不需要再使用复杂的正则表达式再判断一次输入的字符串是否满足 email 地址格式要求
            //     Match match = Regex.Match(email,@"(.+)@(.+)");
            //     Console.WriteLine($"用户名:{match.Groups[1].Value},域名:{match.Groups[2].Value}");
            // }

            #endregion

            #region "192.168.10.5[port=21,type=ftp]",这个字符串表示的 IP 地址为192.168.10.5的服务器的21端口提供的是 ftp 服务，其中如果",type=ftp"部分被省略，则默认为 http 服务。请使用程序解析此字符串，然后分别打印出 IP 地址、端口号与服务类型

            // string msg = "192.168.10.5[port=21,type=ftp]";
            // Match match = Regex.Match(msg,@"(.+)\[port=([0-9]{2,5})(,type=(.+))?\]");
            // Console.WriteLine($"IP:{match.Groups[1].Value}");
            // Console.WriteLine($"Port:{match.Groups[2].Value}");
            // Console.WriteLine("Services Type:{0}",match.Groups[4].Value.Length == 0 ? "http" : match.Groups[5].Value);
            // IP:192.168.10.5
            // Port:21
            // Services Type:ftp

            #endregion

        }

        /*
         * 贪婪模式与非贪婪模式：
         * 贪婪：. + 等元字符默认为贪婪模式，即尽可能的多匹配
         * 非贪婪：.+?：在 . + 等元字符后面加上一个 ? 即表示非贪婪模式，尽可能的少匹配
         */
        public static void Main4(string[] args) {

            #region 贪婪模式

            string msg = "1111.11.111.111111.";
            // .+ 默认是按照贪婪模式来匹配，尽可能多的去匹配
            Match match = Regex.Match(msg,".+");
            Console.WriteLine(match.Value);// 1111.11.111.111111.
            // 终止贪婪模式：当在“限定符”后面使用 ? 之后，表示终止贪婪模式
            // 而当终止贪婪模式时，会尽可能少的匹配
            Match match2 = Regex.Match(msg, ".+?");
            Console.WriteLine(match2.Value);// 1

            #endregion

        }

        /// <summary>
        /// 贪婪模式2
        /// </summary>
        /// <param name="args"></param>
        public static void Main5(string[] args) {
            string str = "abbb";
            Match match = Regex.Match(str, "ab*");
            Match match2 = Regex.Match(str, "ab*?");
            Console.WriteLine(match.Value);// abbb
            Console.WriteLine(match2.Value);// a

            string msg = "1111。11。111。111111。";
            Match match3 = Regex.Match(msg, ".+?。");// 1111。
            Console.WriteLine(match3.Value);

            string msg2 = "大家好。我们是S.E.H。我是S。我是H。我是E。我是杨中科。我是苏坤。我是杨洪波。我是Tim。我是N.L.L。我是☆姜坤☆。呜呜呜。ffff";

            MatchCollection matches = Regex.Matches(msg2, "我是(.+?)。");
            foreach (Match match1 in matches) {
                Console.WriteLine(match1.Groups[1].Value);
            }
            /*
              S
              H
              E
              杨中科
              苏坤
              杨洪波
              Tim
              N.L.L
              ☆姜坤☆
            */
                
        }

    }

    
}


/*
 * 使用正则表达式的建议：
 * 1.不要过度使用正则表达式，简单的操作能用字符串方式直接操作的就用字符串方式来操作，例如：
 * IndexOf()、StartWith()、EndWith()、Path.GetFileName()...
 *     因为很多基本的字符串操作方式已经有很高效的算法了，用了正则表达式反而效率低下
 * 2.如果多次使用同样的正则表达式，则缓存该对象，或者使用 new Rgeex 对象的方式创建一个 Regex 对象
 * 3.正则表达式是对字符串操作的，不要试图用正则表达式来验证字符串的意义，比如：验证是否为
 * 闰年等，这种操作用程序更高效，更容易
 */
namespace 正则表达式_正则提取与替换 {
    public class Program {
        /// <summary>
        /// 通过 WebClient 来提取 Email 地址
        /// </summary>
        /// <param name="args"></param>
        public static void Main0(string[] args) {
            // 通过 WebClient 下载字符串
            WebClient client = new WebClient();
            string html = client.DownloadString("http://localhost:8080/留下你的Email.html");
            // 从 html 字符串中提取邮箱地址
            MatchCollection matches = Regex.Matches(html, @"[-a-zA-Z0-9_.]+@[-a-zA-Z0-9]+(\.[a-zA-Z]+){1,2}");
            foreach (Match match in matches) {
                Console.WriteLine(match.Value);
            }

            Console.WriteLine($"共{matches.Count}个邮箱地址");
            
        }

        /// <summary>
        /// 通过 WebClient 提取网页的图片
        /// </summary>
        /// <param name="args"></param>
        public static void Main1(string[] args){
            WebClient wb = new WebClient();
            string html = wb.DownloadString("http://localhost:8080/mm.html");
            // 提取里面的 <img/> 标签
            // <img alt="",src="hotgirls/00_00.jpg">
            MatchCollection matches = Regex.Matches(html,@"<img\s+alt="""" src=""(.+)"" />");
            foreach (Match match in matches){
                System.Console.WriteLine(match.Value + "   " + match.Groups[1].Value);
                string pathImg = "http://localhost/mm" + match.Groups[1].Value;
                // 通过拼接路径实现下载图片
                wb.DownloadFile(pathImg,@"d:\" + System.DateTime.Now.ToFileTime() + ".jpg");
            }
        }


        /// <summary>
        /// 提取超链接
        /// </summary>
        /// <param name="args"></param>
        public static void Main2(string[] args) {
            WebClient we = new WebClient();
            string html = we.DownloadString("html://localhost:8080/test1.html");
            // <a href="www.baidu.com">baidu</a>
            MatchCollection matches = Regex.Matches(html, @"<a\s*href="".+?"">.+?</a>", RegexOptions.IgnoreCase);
            foreach (Match match in matches) {
                Console.WriteLine(match.Value);
            }
            
        }


        /// <summary>
        /// 字符串替换
        /// </summary>
        /// <param name="args"></param>
        public static void Main3(string[] args) {
            // string msg = "你aaa好aa哈哈a你";
            // msg = msg.Replace("a", "A");
            // Console.WriteLine(msg);// 你AAA好AA哈哈A你
            //
            // msg = Regex.Replace(msg,"a+","A");// 你AAA好AA哈哈A你
            // Console.WriteLine(msg);
            
            // 将 hello 'welcome' to 'china' 替换成 hello 【welcome】 to 【china】
            string msg = "hello 'welcome' to 'china' 'less'     'ls'    'szj'    ";
            msg = Regex.Replace(msg, "'(.+?)'", "【$1】");
            Console.WriteLine(msg);
            // hello 【welcome】 to 【china】 【less】     【ls】    【szj】
            
            // 隐藏手机号
            string msg2 = "杨中科13409876543黄林18276354908杨硕87654321345红卫红98761234654";
            msg2 = Regex.Replace(msg2, @"([0-9]{3})[0-9]{4}([0-9]{4})", "$1****$2");
            Console.WriteLine(msg2);
            // 杨中科134****6543黄林182****4908杨硕876****1345红卫红987****4654
            
            // 隐藏邮箱名：
            string msg3 = "我的邮箱zlim530@126.com他的邮箱tim_zhao@163.com某某的邮箱xx@itcast.cn";
            msg3 = Regex.Replace(msg3, @"\w+(@\w+\.\w+)","****$1",RegexOptions.ECMAScript);
            Console.WriteLine(msg3);
            // 我的邮箱****@126.com他的邮箱****@163.com某某的邮箱****@itcast.cn

        }

        /// <summary>
        /// 单词边界 \b ：只判断是够匹配而不是真正的匹配，属性“断言”的一种
        /// </summary>
        /// <param name="args"></param>
        public static void Main4(string[] args) {
            string msg = "The day after tomorrow is wedding day.The row we are looking for is .row. number 10.";

            // \b：表示单词的边界
            // 什么叫做单词？
            // 即在这个单词的两个边界中均是一边是[a-zA-Z]组成而另一边是非[a-zA-Z]组成
            // welcome come come
            // 当我们选择 \bcome\b 那么 welcome 中的 come 就不会匹配，因为在 welcome 中左边的单词边界不管是左边还是右边都是组成单词的部分
            msg = Regex.Replace(msg, @"\brow\b", "line");
            Console.WriteLine(msg);
            // The day after tomorrow is wedding day.The line we are looking for is .line. number 10.
            
            // 请提取出3个字母的单词
            string msg2 = "Hi,how are you?Welcome to our country!";
            MatchCollection matches = Regex.Matches(msg2, @"\b[a-z]{3}\b", RegexOptions.IgnoreCase);
            foreach (Match match in matches) {
                Console.WriteLine(match.Value);
                // how
                // are
                // you
                // our
            }
            
            string msg3 = "# ## ### ## # ## ### # ###.";
            MatchCollection matches2 = Regex.Matches(msg3, @"\b###\b");
            Console.WriteLine(matches2.Count);
            // 0 : 因为 # 不是组成单词的一部分，也即 # 不能组成单词，因此一个都提取不到
            // MatchCollection matches3 = Regex.Matches(msg3, @" ### ");
            // ###
            // ###
            MatchCollection matches3 = Regex.Matches(msg3, @"(?<= )###(?= )");
            // ###
            // ###
            foreach (Match match in matches3) {
                Console.WriteLine(match.Value);
            }
        }

        
        /// <summary>
        /// 反向引用
        /// </summary>
        /// <param name="args"></param>
        public static void Main5(string[] args) {
            // while (true) {
            //     Console.WriteLine("请输入叠词：");
            //     string msg = Console.ReadLine();
            //     msg = Regex.Replace(msg, @"(.)\1+", "$1");
            //     Console.WriteLine(msg);
            // }

            // string msg = "您们喜欢杨杨杨杨杨杨中中中中中科科科科科科";
            // msg = Regex.Replace(msg, @"(.)\1+", "$1");
            /*
             \1：表示反向引用分组：是在正则表达式匹配时进行引用
            (()())\1\2\3：
            ((a)x(b))\1\2\3：可以匹配：axbaxbab
            上述正则表达式的含义：
            ((a)x(b))表示必须出现 axb，而其中最外层的小括号分组为第一组，使用 \1 表示反向引用第一组中的内容，也就是 axb；随后的 (a) 为第2组中的内容，\2 即表示引用 (a) 中的内容，也就是 a；同理 x 后面的 (b) 分组为第三组，因此 \3 即表示引用第三组的内容，也就是 (b) 即 b，因此上述正则表达式可以匹配字符串 axbaxbab
            www wwwwwwwww：可以使用下述正则表达式匹配：(.{3})\1{3}
            $1：也表示引用第一组的内容：是在正则表达式替换时进行引用
            */
            // Console.WriteLine(msg);
            // 您们喜欢杨中科

            #region 练习：将一段文本中的MM/DD/YYYY格式的日期转换为YYYY-MM-DD格式，比如“我的生日是05/21/2010耶”转换为“我的生日是2010-05-21耶”

            // string msg2 = "我的生日是05/21/2010耶我的生日是2010-05-21耶";
            // msg2 = Regex.Replace(msg2, @"(\d{2})/(\d{2})/(\d{4})", "$3-$1-$2");
            // Console.WriteLine(msg2);

            #endregion

            #region 练习2：给一段文本匹配的url添加超链接，比如把http://www.test.com替换为<a href="http://www.test.com"> http://www.test.com</a>.

            // string msg = "给一段文本匹配的url添加超链接，比如把http://www.test.com替换为http://www.sina.com.cn哈哈http://www.google.com";
            // msg = Regex.Replace(msg, @"[a-zA-Z0-9]+://[-a-zA-Z0-9.?&=#\/_]+", "<a href=\"$0\">$0<\a>");
            // Console.WriteLine(msg);

            #endregion

            #region 提取单词

            // string txt = File.ReadAllText("2.txt", Encoding.Default);
            // MatchCollection matches = Regex.Matches(txt, @"[a-zA-Z]*([a-zA-Z])\1+[a-zA-Z]*");
            // foreach (Match match in matches) {
            //     Console.WriteLine(match.Value);
            // }

            #endregion

            #region 提取叠词

            // string msg = File.ReadAllText("1.txt", Encoding.UTF8);
            // AABB 型
            // MatchCollection matches = Regex.Matches(msg, @"(.)\1(.)\2");
            // foreach (Match match in matches) {
            //     Console.WriteLine(match.Value);
            // }

            #endregion

        }

        
        /// <summary>
        /// 敏感词过滤
        /// </summary>
        /// <param name="args"></param>
        public static void Main6(string[] args) {
            // 用来存储需要审核的关键词
            StringBuilder sbMode = new StringBuilder();
            // 用来存储绝对禁止的关键词
            StringBuilder sbBanned = new StringBuilder();
            string[] lines = File.ReadAllLines("1.txt", Encoding.Default);
            for (int i = 0; i < lines.Length; i++) {
                string[] parts = lines[i].Split('=');
                if (parts[1] == "{MOD}") {
                    sbMode.AppendFormat("{0}|", parts[0]);
                }else if (parts[1] == "{BANNED}") {
                    sbBanned.AppendFormat("{0}|", parts[0]);
                }
            }

            sbMode.Remove(sbMode.Length - 1, 1);
            sbBanned.Remove(sbBanned.Length - 1, 1);
            string input = Console.ReadLine();
            // 验证是否有禁止发帖的关键词
            // "xxx" => "x|x|x" => 符合正则表达式语法
            if (Regex.IsMatch(input,sbBanned.ToString())) {
                Console.WriteLine("禁止发帖！");
            }else if (Regex.IsMatch(input,sbMode.ToString())) {
                Console.WriteLine("需要审核！");
            }
            else {
                Console.WriteLine("可以发帖！");
            }


        }
    
        
        /// <summary>
        /// 正则表达式提取职位信息
        /// </summary>
        /// <param name="args"></param>
        public static void Main7(string[] args) {
            WebClient wc = new WebClient();
            string html = wc.DownloadString("http://localhost:8080/【上海,IT-管理,计算机软件招聘,求职】-前程无忧.html");

            MatchCollection matches =
                Regex.Matches(html, "<a href=\"http://search.51job.com/job/[0-9]+,c.html\".+?>(.+?)</a>");
            foreach (Match match in matches) {
                Console.WriteLine(match.Groups[1].Value);
            }

            Console.WriteLine($"total count:{matches.Count}");
            
        }
        
        
    }
    
}



















