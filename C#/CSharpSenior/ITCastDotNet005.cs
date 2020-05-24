using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/**
 * @author zlim
 * @create 2020/5/24 12:56:03
 */

/*
文件操作常用相关类：
File：操作文件，静态类，对文件整体操作：拷贝、删除、剪切等；
Directory：操作目录（文件夹），静态类；
Path：对文件或者目录的路径进行操作（很方便）【字符串】，静态类；
DirectoryInfo：文件夹的一个“类”，用来描述一个文件夹对象，获取指定目录下的所有目录时返回一个 DirectoryInfo 数据；
FileInfo：文件类，用来描述一个文件对象，获取指定目录下的所有文件时，返回一个 FileInfo 数据；
Stream：文件流，抽象类，下面是它的子类
    FileStream：文件流
    MemoryStream：内存流
    NetworkStream：网络流
    StreamReader：快速读取文本文件
    StreamWriter：快速写入文本文件
    GZipStream
*/
namespace 文件操作 {

    public class ITCastDotNet005 {
        /// <summary>
        /// Path 类基本操作
        /// </summary>
        /// <param name="args"></param>
        static void Main0(string[] args) {
            //string path = @"C:\Users\Lim\Desktop\code\feature\feature101.data";

            //Console.WriteLine(Path.GetFileName(path));

            //Console.WriteLine(Path.GetExtension(path));

            //Console.WriteLine(Path.GetFileNameWithoutExtension(path));

            //Console.WriteLine(Path.GetDirectoryName(path));

            //Console.WriteLine(Path.ChangeExtension(path,".exe"));
            // 只是将字符串的一个操作，而不会修改实际文件，与实际文件无关，设置的文件路径实际上根本不存储这个文件也没关系

            string s1 = @"c:\abc\x\y";
            string s2 = "hello.txt";
            Console.WriteLine(Path.Combine(s1,s2));

            // 相对路径
            // VS 编辑器的相对路径是对于项目的 bin/debug/netcoreapp3.0 路径下的
            string path = "CSharpSenior.dll";
            // 返回相对路径所对应的的绝对路径
            Console.WriteLine(Path.GetFullPath(path));
            // C:\Users\Lim\Desktop\code\CodeReview\C#\CSharpSenior\bin\Debug\netcoreapp3.0\CSharpSenior.dll

            // 获取系统的临时目录路径
            Console.WriteLine(Path.GetTempPath());
            //Console.WriteLine(Path.GetTempFileName());// 会在系统临时文件存放目录下创建一个新的临时文件并且可以保证一定不会和已有的文件重名
            string path2 = Path.GetRandomFileName();// 获得一个随机的文件名，并不会再系统临时文件夹中创建新的文件
            Console.WriteLine(path2);

            Guid.NewGuid().ToString();// 获取一个新的 GUID 
        }

        /// <summary>
        /// Directory 类的基本操作
        /// </summary>
        /// <param name="args"></param>
        static void Main1(string[] args) {

            // 获取指定目录下的所有的子【文件】（注意：是直接子文件，而不会获得子目录）
            string path = @"C:\Users\Lim\Desktop\code";
            string[] files = Directory.GetFiles(path);
            foreach (var file in files) {
                Console.WriteLine(file);
            }

            Console.WriteLine(@"获取 C:\Users\Lim\Desktop\code 下的所有子目录/文件夹");
            // 获取指定目录下的所有子目录/文件夹
            string[] dirs = Directory.GetDirectories(path);
            foreach (var dir in dirs) {
                Console.WriteLine(dir);
            }

        }

        /// <summary>
        /// File 类的基本操作
        /// </summary>
        /// <param name="args"></param>
        static void Main2(string[] args) {

            // 判断文件是否存在
            if (File.Exists(@"c:\xx.txt")) {
                Console.WriteLine("yes");
            } else {
                Console.WriteLine("no");
            }

            // 如果文件不存在删除也不会报异常
            File.Delete(@"c:\xx.txt");
            File.Copy("source","targetFileName",true);
            // 文件拷贝：true 表示当文件存在时“覆盖”，如果不加 true，则目标文件存在时会报异常

            File.Move("source","targetFileName");
            // 移动（剪切）：文件的剪切是可以跨磁盘的

            File.Create("path");// 创建文件
        }

        /// <summary>
        /// 文件乱码问题
        /// </summary>
        /// <param name="args"></param>
        static void Main3(string[] args) {
            //string msg = File.ReadAllText("2.txt");
            //Console.WriteLine(msg);
            // ?????????????й???Hello,Welcome to China! 中文字符发生了乱码

            // 表示使用系统默认的编码读取文件
            string msg = File.ReadAllText("2.txt", Encoding.UTF8);
            Console.WriteLine(msg);

            //Encoding encoding = Encoding.GetEncoding("gb2312");
            EncodingInfo[] encodings = Encoding.GetEncodings();
            foreach (var encoding in encodings) {
                // 默认会在项目的 bin/Debug/netcoreapp3.0 下生成 encodings.txt 文件
                
                File.AppendAllText("encodings.txt", string.Format("{0},{1},{2}\r\n", encoding.CodePage, encoding.DisplayName, encoding.Name));
                /*
                1200,Unicode,utf-16
                1201,Unicode (Big-Endian),utf-16BE
                12000,Unicode (UTF-32),utf-32
                12001,Unicode (UTF-32 Big-Endian),utf-32BE
                20127,US-ASCII,us-ascii
                28591,Western European (ISO),iso-8859-1
                65000,Unicode (UTF-7),utf-7
                65001,Unicode (UTF-8),utf-8
                */
            }

        }

    }


}
