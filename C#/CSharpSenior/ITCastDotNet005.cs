using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/**
 * @author zlim
 * @create 2020/5/24 12:56:03
 */

/*
拷贝文件的两种方式：
    将源文件内容全部度到内存中，再写到目标文件中；
    读取源文件的1KB 内容，写到目标文件中，再读取源文件的1KB 内容，再写到目标文件中，直到将源文件的所有内容读取
    完毕。
其中第二种方法其实就是一种流的操作。
用 File.ReadAllText、File.WriteAllText 进行文件的读写操作是一次性进行读写，如果文件非常大时会占用大量的内存
，速度慢；而流（Stream）则是一种读取一部分再处理一部分的机制，Stream 仅会读取指定位置处指定长度的字节内容。
Stream 不会一次性将所有内容读到内存中，而是有一个指针，指针指到哪里就对哪里进行读、写操作。
流有很多种类，文件流（FileStream）是其中一种，FileStream 可读可写，可以使用 File.OpenRead 或者 
File.OpenWrite 方法快速创建文件流对象。
byte[] 是任何数据的最根本表达形式，任何数据在计算机中进行存储最终都是二进制。
FileStream 的 Position 属性为当前文件指针位置，每写一次就要移动 Position 以备下次读写时在正确的位置。Write
用于向当前位置写入若干字节，Read 则用于在当前位置读取若干字节。
使用 using 来简化操作：
    注意：不是任何类型对象都可以写在 using() 的小括号中
    只有实现了 IDispose 接口类型的对象才可以写，当 using{ ... } 执行完毕后会自动调用对象的 Dispose() 方法来释放资源


*/
namespace 文件流操作 {
    public class Program {
        /// <summary>
        /// byte[] 与字符串之间的转换
        /// </summary>
        /// <param name="args"></param>
        static void Main0(string[] args) {
            string msg = "Hello,World!";
            // string 转换为 byte[] 数组
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(msg);
            // 把 byte[] 数组转换为字符串
            string msg2 = System.Text.Encoding.UTF8.GetString(bytes);
            Console.WriteLine("OK");
        }

        // 流操作的都是字节，不能字节操作字符串。
        
        /// <summary>
        /// 通过 FileStream 来写文件
        /// </summary>
        /// <param name="args"></param>
        static void Main1(string[] args){

            // 注意：当我们使用相对路径来表示文件时，会默认在 CSharpSenior\bin\Debug\netcoreapp3.0 下进行读写操作
            // 这里相对路径的访问位置对于不同的编译器是不同的，在 VSCode 中默认在当前项目 \CSharpSenior 下
            // 而 VS 则默认在 \CSharpSenior\bin\Debug\netcoreapp3.0
            // 创建文件流
            FileStream fsWrite = new FileStream("first.txt",FileMode.Create,FileAccess.Write);
            string msg = "今天是个好日子！";
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(msg);
            fsWrite.Write(bytes,0,bytes.Length);
            /* 
            第一个参数：表示将指定的字节数组中的内容写入到文件中
            第二个参数：表示参数1中字节数据的偏移量，一般为0，0表示从数组的最开始开始写入文件中
            第三个参数：本次文件写入操作写入文件的实际字节个数
            */

            // 清空缓冲区；关闭文件流；释放资源
            fsWrite.Flush();
            // 清空缓冲区：因为程序是无权对进行磁盘文件的读写操作的，实际上程序是调用操作系统提供的API，因此当我们在程序
            // 使用文件流进行读写操作时，实际上并不是实时的，而是操作系统也会有一个缓冲区，使用此方法就表示告诉操作系统
            // 不要再等待缓冲区填满，而是现在就进行文件的读写操作
            fsWrite.Close();
            fsWrite.Dispose();
            System.Console.WriteLine("OK");
        }

        /// <summary>
        /// 使用 FileStream 来读文件
        /// </summary>
        /// <param name="args"></param>
        static void Main2(string[] args) {

            // 使用 FileStream 文件流读取文本文件时不需要指定编码，因为编码是在 byte[] 数组转换为字符串时才需要指定的
            // 而这里是直接读取 byte[] 数组中的字节数据，因此不需要使用编码
            // using( ... ){ ... } 会自动调用相关资源的 Dispose() 方法
            using (FileStream fsRead = new FileStream("first.txt",FileMode.Open,FileAccess.Read)) {
                // 根据文件流的总字节数创建 byte[] 数组，这种方式会将文件中的内容一次性全部读取出来，并存入 byte[] 数组中
                byte[] bytes = new byte[fsRead.Length];
                fsRead.Read(bytes,0,bytes.Length);// 从流中读取字节块并将数据写入给定缓冲区中,count:最多读取的字节数。
                /*
                第一个参数：表示将指定文件流中读取到的内容写入 byte[] 数组中；
                第二个参数：表示从文件流中读取数据的偏移量；
                第三个参数：表示最多读取的字节数。
                */
                string msg = System.Text.Encoding.UTF8.GetString(bytes);
                Console.WriteLine(msg);
            }

        }

        /// <summary>
        /// 测试文件拷贝 CopyFile 方法
        /// </summary>
        /// <param name="args"></param>
        static void Main3(string[] args) {
            string source = @"C:\迅雷下载\致命魔术BD中英双字1280高清.rmvb";
            string target = @"C:\Users\Lim\Desktop\test.rmvb";
            CopyFile(source,target);
            Console.WriteLine("OK");
        }

        /// <summary>
        /// 通过文件流实现将 source 文件拷贝到 target :至少需要两个文件流对象
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void CopyFile(string source,string target) {
            // 创建一个读取源文件的文件流
            using (FileStream fsRead = new FileStream(source,FileMode.Open,FileAccess.Read)) {
                // 创建一个写入新文件的文件流
                using (FileStream fsWrite = new FileStream(target,FileMode.Create,FileAccess.Write)) {
                    // 拷贝文件时，创建一个缓冲区
                    byte[] buffer = new byte[1024 * 1025 * 5];// 5MB
                    // Read() 方法的返回值表示实际读取到字节数
                    int len = fsRead.Read(buffer,0,buffer.Length);
                    while (len > 0) {
                        // 将读取到的内容写入到 target 文件中，第三个参数应该为实际读取到字节数而不是缓冲区数组的长度
                        fsWrite.Write(buffer,0,len);
                        /*
                        如果想显式拷贝进度：
                        double d = (fsWrite.Postion) / (double)fsRead.Length * 100;
                        Console.WriteLine($"{d}");
                        */
                        len = fsRead.Read(buffer,0,buffer.Length);
                    }
                }

            }

        }

        /// <summary>
        /// 测试文件加密 Encrypt 方法
        /// </summary>
        /// <param name="args"></param>
        static void Main4(string[] args) {
            string source = @"C:\Users\Lim\Desktop\encrypt.jpg";
            string target = @"C:\Users\Lim\Desktop\decrypt.jpg";
            Encrypt(source,target);
            Console.WriteLine("OK");
        }

        /// <summary>
        /// 对文件进行加密操作并生成新的文件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void Encrypt(string source,string target) {
            // 创建一个读取源文件的文件流
            using (FileStream fsRead = new FileStream(source,FileMode.Open,FileAccess.Read)) {
                // 创建一个写入新文件的文件流
                using (FileStream fsWrite = new FileStream(target,FileMode.Create,FileAccess.Write)) {

                    byte[] buffer = new byte[1024 * 4];// 4KB
                    int count = 0;
                    while ((count = fsRead.Read(buffer,0,buffer.Length)) > 0) {
                        // 加密：实际就是按照一定的规则将缓冲区中读到的文件字节内容改变一下，然后再将改变完了字节内容写入新文件
                        for (int i = 0; i < count; i++) {
                            buffer[i] = (byte)((byte.MaxValue) - buffer[i]);// buffer[i] = 255 - buffer[i];
                            // 或者用：buffer[i] = Convert.ToByte(255- buffer[i]);
                        }
                        // 写入新文件
                        fsWrite.Write(buffer,0,count);
                    }

                }

            }

        }



    }
}


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
