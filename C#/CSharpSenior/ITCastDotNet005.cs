using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

/**
 * @author zlim
 * @create 2020/5/24 12:56:03
 */

/*
序列化只会序列化数据；
序列化之后只是将对象的存储格式改变了，但是对象的实际存储内容并没有改变。
对象序列化（二进制序列化）：
    对象序列化是将对象转换为二进制数据（字节流），反序列化是将二进制数据还原为对应的对象；
    对象是稍纵即逝的，不仅程序重启、操作系统冲洗会造成对象的消失，就连退出函数范围等原因
    也可能造成对象的消失，序列化和反序列化就是为了保持对象的持久化，就像用 DV 录像(序列
    化)而后用播放器播放(反序列化)一样
对象序列化，只能针对对象的字段进行序列化
BinaryFormatter 类有两个方法：
    void Serialize(Stream stream, object graph) 将对象 graph 系列化到 stream 中
    object Deserialize(Stream stream) 将对象从 stream 中反序列化，返回值为反序列后得到的对象
不是所有的对象都能进行序列化操作，只有可序列化的对象才能序列化
    条件：在类声明前面添加 [Serizlizable] 特征，并且对象的属性、字段的类型也必须可序列化
为什么要序列化?
    将一个复杂的对象转换为文件流,方便存储与信息交换
*/
namespace 对象序列化 {
    public class Program {
        /// <summary>
        /// JSON 序列化和 xml 序列化
        /// </summary>
        /// <param name="args"></param>
        static void Main0(string[] args) {
            Person p = new Person();
            p.Name = "Tim";
            p.Age = 23;
            p.Email = "tim@163.com";

            // JSON 序列化
            string json = JsonConvert.SerializeObject(p);
            Console.WriteLine(json);
            // {"Name":"Tim","Age":23,"Email":"tim@163.com"}

            // xml 序列化
            XmlSerializer xml = new XmlSerializer(typeof(Person));
            using (FileStream fs = new FileStream("person.xml",FileMode.Create)) {
                xml.Serialize(fs,p);
            }
            /*
            person.xml:
            <?xml version="1.0"?>
            <Person xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
              <Name>Tim</Name>
              <Age>23</Age>
              <Email>tim@163.com</Email>
            </Person>
            */
            Console.WriteLine("OK");
        }

        /// <summary>
        /// 二进制序列化
        /// </summary>
        /// <param name="args"></param>
        // 二进制序列化注意点：
        // 1.被序列化的对象的类型必须标记为“可序列化”，即 [Serializable];
        // 2.被序列化的类的所有父类也必须标记为“可序列化”，即 [Serializable];
        // 3. 要求被序列化的对象的类型中的所有字段（属性）的类型也必须标记为“可序列化的”
        // ，即 [Serializable]
        // 4.序列化只会对类中的字段序列化：即只能序列化一些状态信息
        // 5.不建议使用自动属性:因为自动属性每次生成的字段名称都可能不一样,会影响反序列化
        static void Main1(string[] args) {
            Person p = new Person();
            p.Name = "Tim";
            p.Age = 23;
            p.Email = "tim@163.com";

            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fsWriter = new FileStream("person.data",FileMode.Create)) {
                // try{
                //     bf.Serialize(fsWriter,p);
                // }catch (Exception e){
                //     System.Console.WriteLine(e.Message);
                // }
                bf.Serialize(fsWriter,p);

            }
            Console.WriteLine("OK");
        }

        
        /// <summary>
        /// 二进制反序列化
        /// </summary>
        /// <param name="args"></param>
        // 二进制反序列化注意点：
        // 1.必须获取被序列化对象类型的所在的程序集，因为：反序列化要根据序列化之后的文件
        // 重新还原该对象，而序列化文件中只包含对象的数据信息，并不包含对象的类型的相关信息
        // ，例如：该对象是继承至那个父类，实现了哪些接口，拥有哪些方法 ...，这些信息在对
        // 想序列化之后的文件中并不包含，因为在对象序列化时只是序列化字段数据，要获取这些信
        // 息则必须通过该类型所在的程序集来获取 
        static void Main2(string[] args){

            BinaryFormatter bf = new BinaryFormatter();
            using(FileStream fsRead = new FileStream("person.data",FileMode.Open)){
                object obj = bf.Deserialize(fsRead);
                Person p = obj as Person;
                System.Console.WriteLine(string.IsNullOrEmpty(p.Name));// True 因为 _name 字段被标记为 NonSerialized
                System.Console.WriteLine(p.Age);
                System.Console.WriteLine(p.Email);
            }

        }

    }


    [Serializable]
    public class Animal {
        
    }

    [Serializable]
    public class Person:Animal {
        public Person() : this(null, 0, null) {

        }

        public Person(string name, int age, string email) {
            this.Name = name;
            this.Age = age;
            this.Email = email;
        }

        public Car Car { get; set; }

        [NonSerialized]
        // NonSerialized 特性只能标记在字段上
        private string _name;

        public string Name { 
            get{
                return _name;
            }
            set{
                _name = value;
            }
        }
        public int Age { get; set; }
        public string Email { get; set; }
    }

    [Serializable]
    public class Car {
        public string Name { get; set; }
    }

}


/*
压缩流：GZipStream
压缩对象：图片、文本文件、电影、字符串等
压缩：
    创建读取流 File.OpenRead()  ：快速创建读取文件流的方法
        File.OpenRead() 内部：return new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.Read);
    创建写入流 File.OpenWrite() ：快速创建写入文件流的方法
    创建压缩流 new GZipStream() ：将写入流作为参数
    每次通过读取流读取一部分数据，并通过压缩流压缩之后写入新文件中
解压：
    创建读取流：File.OpenRead()
    创建压缩流：new GZipStream(); 将读取流作为参数
    创建写入流：File.OpenWrite()
    每次通过压缩流读取解压后数据，再通过写入流将解压后的数据写入到新文件中
*/
namespace 压缩流 {
    public class Program {
        /// <summary>
        /// 压缩单个文本文件
        /// </summary>
        /// <param name="args"></param>
        static void Main0(string[] args) {
            // 将文本文件2.txt 压缩
            // 创建读取文本文件的流
            using (FileStream fsRead = File.OpenRead("2.txt")) {
                // 创建写入文本文件的流
                using (FileStream fsWrite = File.OpenWrite("2.zip")) {
                    // 创建压缩流
                    using (GZipStream zipStream = new GZipStream(fsWrite,CompressionMode.Compress)) {
                        byte[] buffer = new byte[1024];// 每次读取1024 byte
                        int len = 0;
                        while ((len = fsRead.Read(buffer,0,buffer.Length)) > 0) {
                            // 通过压缩流对读取到的文件进行压缩，并通过 fsWrite 写入流写入到新文件（即压缩文件）中
                            zipStream.Write(buffer,0,len);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 使用 StreamReader 和 StreamWriter 读写文本文件
        /// </summary>
        /// <param name="args"></param>
        static void Main1(string[] args) {
            // 工资文件中的工资翻倍输出到新文件中
            using (StreamReader reader = new StreamReader("salary.txt",Encoding.Default)) {
                // 创建一个写文件的文件流
                using (StreamWriter writer = new StreamWriter("newSalary.txt",false,Encoding.Default)) {
                    string line = null;
                    while ((line = reader.ReadLine()) != null) {
                        string[] parts = line.Split('|');
                        string newLine = string.Format("{0}|{1}",parts[0],Convert.ToInt32(parts[1]) * 2 );
                        // 将新行写入
                        writer.WriteLine(newLine);
                        // File.AppendAllText("",""); 不要调用这个方法，这样做每次都会 new 一个 StreamWriter
                    }
                }
            }
            Console.WriteLine("OK");
        }

        /// <summary>
        /// 对象初始化器-集合初始化器
        /// </summary>
        /// <param name="args"></param>
        static void Main2(string[] args) {
            Person p = new Person();
            p.Name = "Tom";
            p.Age = 23;
            p.Email = "tom@163.com";

            // 实际上是调用了构造函数，并不是初始化器
            Person p2 = new Person("Jack",25,"jack@163.com");

            // { ... } 就是对象初始化器，它与对象的构造函数一定关系都没有
            // 在编译后会像对象 p 赋值那样的语句
            Person p3 = new Person() { Name = "Tim",Age = 22,Email = "tim@163.com" };

            // 集合初始化器
            List<int> list = new List<int>() { 1,2,3,4,5,5,6,7,8,9};

        }

        /// <summary>
        /// 解压单个文本文件
        /// </summary>
        /// <param name="args"></param>
        static void Main3(string[] args) {
            // 创建读取文件流读取压缩文件
            using (FileStream fsRead = File.OpenRead("2.zip")) {
                // 创建压缩流解压读取的压缩文件
                using (GZipStream zipStream = new GZipStream(fsRead,CompressionMode.Decompress) ) {
                    // 创建写入文件流将解压后的文件写入到新文件（解压后的文件）中
                    using (FileStream fsWrite = File.OpenWrite("decompress2.txt")) {
                        byte[] buffer = new byte[1024 * 10];// 10KB
                        int len = 0;
                        // 解压读取后的文件
                        while ((len = zipStream.Read(buffer,0,buffer.Length)) > 0) {
                            // 写入新文件
                            fsWrite.Write(buffer,0,len);
                        }
                    }
                }
            }
            Console.WriteLine("OK");
        }

        /// <summary>
        /// 利用 File.WriteAllText(string path,string content) 方法写入文件
        /// </summary>
        /// <param name="args"></param>
        static void Main4(string[] args) {
            StringBuilder msg = new StringBuilder("Tim");
            for (int i = 0; i < 5; i++) {
                msg.Append(msg);
            }
            // 创建一个新文件，想其中写入指定的字符串，然后关闭文件。如果目标文件已存在，则覆盖该文件。
            // 第二个参数：要写入文件的字符串
            File.WriteAllText("test.txt",msg.ToString());
            Console.WriteLine("OK");

        }

    }

    public class Person {

        public Person() : this(null,0,null){

        }

        public Person(string name,int age,string email) {
            this.Name = name;
            this.Age = age;
            this.Email = email;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }

}


/*
文件IO流：
    输入 input：读取外部数据（磁盘、光盘等存储设备的数据）到程序（即内存）中：读
    输出 output：将程序（内存）中的数据输出到磁盘、光盘等存储设备中：写

解码：字节(二进制) ---> 字符串
编码：字符串      ---> 字节

编码表的由来：
    计算机只能识别二进制数据，早期由来是电信号。为了方便应用计算机，让它可以识别各个的文字，
    就将各个国家的文字用数字来表示，并一一对应，形成一张表。中额就是编码表
常见的编码表：
    ASCII：美国标准信息交换码
        用一个字节的7位可以表示
    ISO8859-1：拉丁码表，欧洲码表
        用一个字节的8位表示
    GB2312：中国的中文编码表，最多两个字节编码所有字符
    GBK：中国的中文编码表升级，融合了更多的中文文字符号，同样是最多两个字节编码
    Unicode：国际标准码，融合了目前人类使用的使用字符。为每个字符分配唯一的字符码，所有的文字都用两个字节来表示。
            在内存层面表示没有问题，但是存入文件时有问题：因为 Unicode 完全向下兼容 ASCII 码，而所有 ASCII 码
            只需要一个字节即可，因此在 Unicode 中两个字节到底是表示两个 ASCII 码还是作为一个整体只表示一个 ASCII 
            码字符有歧义；实际上对于上述三种编码而言也有这样的问题，但是上面三个编码规定最高位为0时表示 ASCII 码，
            即只需要一个字节，而其他的则按照对应字符集的编码规则进行编码
    UTF-8：变长的编码方式，可以用1-4个字节来表示一个字符。向下兼容ASCII码。
        Unicode只是定义了一个庞大的、全球通用的字符集，并为每个字符规定了唯
        一确定的编号，具体存储成什么样的字节流，取决于字符编码方案。
        推荐的Unicode编码是UTF-8和UTF-16。
        Unicode字符集只是定义了字符的集合和唯一编号，Unicode编码，则是对UTF-8、
        UCS-2/UTF-16等具体编码方案的统称而已，并不是具体的编码方案。

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

        // 流操作的都是字节，不能直接操作字符串。
        
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

        /// <summary>
        /// 使用 StreamReader 逐行读取文本文件
        /// </summary>
        /// <param name="args"></param>
        static void Main5(string[] args) {
            // StreamReader 是用来逐行读取文本文件的
            using (StreamReader reader = new StreamReader("encodings.txt",Encoding.Default)) {
                // 一直读到文件的末尾
                //while (!reader.EndOfStream) {
                //    Console.WriteLine(reader.ReadLine());
                //}
                // 或者写为下面这种形式：
                string line = null;
                while ((line = reader.ReadLine()) != null) {
                    Console.WriteLine(line);
                }
            }

        }

        /// <summary>
        /// 使用 StreamWriter 逐行写入文本文件
        /// </summary>
        /// <param name="args"></param>
        static void Main6(string[] args) {
            // 第二个参数：append：若要追加数据到改文件中，则为 true;若要覆盖该文件，则为 false。
            // 如果指定的文件不存在，该参数无效，且构造函数将创建一个新文件
            using (StreamWriter writer = new StreamWriter("text.txt",true,Encoding.Default)) {
                for (int i = 0; i < 10; i++) {
                    writer.WriteLine($"{i} Time(s)");
                }
            }
            Console.WriteLine("OK");
            /*
            0 Time(s)
            1 Time(s)
            2 Time(s)
            3 Time(s)
            4 Time(s)
            5 Time(s)
            6 Time(s)
            7 Time(s)
            8 Time(s)
            9 Time(s)
            */
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
