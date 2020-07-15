using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/**
 * @author zlim
 * @create 2020/5/6 23:45:08
 */
namespace CSharpSenior {
    /*
    I/O 的全称是 input/output，翻译过来就是输入/输出。对于一个系统或者计算机来说，键盘、U盘、网络接口、显示器、
    音响、摄像头等都是 IO 设备。
    对于程序而言，I/O 就是与外界进行数据交换的方式。程序不生产数据，只是数据的搬运工。
    当然，正如滤水器还需要对水进行过滤、消毒等工序一样，程序也要对数据进行运算，所以也不完全是搬用工，
    严格来说是加工厂，那么，I/O 就是工厂的原料提供商和成品销售商。
    因为我们是程序员，因此我们需要站在程序的角度去看待数据，故：
        输入 input：读取外部数据（磁盘、光盘等存储设备中的数据）到程序（内存）中
        输出 output：将程序（内存）中的数据输出到磁盘、光盘等存储设备中
    在 C# 中，I/O 体系整体分为三个部分，后台存储流、装饰流、流适配器。具体划分如下所述：
    流适配器：
        StreamReader、StreamWriter
        BinaryReader、BinaryWriter
        XmlReader、XmlWriter
    装饰器流：
        DeflateStream、GZipStream、CryptoStream、BufferedStream
    后台存储流：
        FileStream、IsolatedStorageStream、MemoryStream、NetworkStream

    在流与流之间，都是采用字节数据进行交换，所以可以得到一个简单的结论，I/O在程序中表现为字节流，
    换句话说I/O就是将各种数据转成字节的工具。

    C#中，所有流都是继承自Stream类，Stream类定义了流应该具有的行为和属性，使得开发人员可以忽略底层的操作系统
    和基础设备的具体细节。C#对流的处理忽略了读流和写流的区别，使其更像是一个管道，方便数据通信。流涉及到三个基本操作：

        读取 - 将数据从流中传输到数据结构中

        写入 - 将数据从数据源写入流中

        查找 - 对流中操作的当前位置进行查找和修改

    因为流的特性，可能并不是所有的流都支持这三种操作，所以Stream提供了三个属性，以方便确认流是否支持这三种操作：
        public abstract bool CanRead { get; } // 获取指示当前流是否支持读取的值
        public abstract bool CanWrite { get; } // 获取指示当前流是否支持写入功能的值
        public abstract bool CanSeek { get; } // 获取指示当前流是否支持查找功能的值
    以上这三个属性均由子类根据自身特性确认是否支持读取、写入、查找，可能三个属性不会都为true，但绝对不会都为false。
    下面是一些常见的流：
        ·FileStream：用来操作文件的流
        ·MemoryStream：操作内存的流
        ·BufferedStream：缓存流，用来增强其他流的操作性能
        ·NetworkStream：使用网络套接字进行操作的流
        ·PipeStream：通过匿名和命名管道进行读取的写入
        ·CryptoStream：用于将数据流链接到加密转换

    C# 中I/O的操作都属于System.IO这个命名空间，在这个命名空间中C# 定义了文件相关的类、各种流、
    装饰器流、适配器以及其他一些相关的结构体。在以System.IO开头的命名空间中，C#对IO进一步扩展，
    并提供了流压缩和解压缩（System.IO.Compression），搜索和枚举文件系统元素（System.IO.Enumeration），
    提供用于使用内存映射文件的类（System.IO.MemoryMappedFiles）等内容。    
        
    先来看一下Stream类里重要的属性和方法：     
        
    1.流里数据的长度
        public abstract long Length { get; }
        当Stream对象的CanSeek为true时，也就是流支持搜索的时候，可以通过这个属性确认流的长度，也就是有多少个字节的数据。
    2.流的位置
        public abstract long Position { get; }
        同长度的前提条件一致，当Stream对象支持搜索的时候，可以通过该属性确认流的位置或者修改流的位置。
    3.读取流里的数据
        public abstract int Read(byte[] buffer, int offest, int count);
        public virtual int ReadByte();
        这是两种不同的读取方式，第一种是每次读取多个字节的数据，第二个是每次只读一个字节的数据。这里来细细讲解一下区别：
        public abstract int Read(byte[] buffer, int offest, int count);表示流每次最多读取 count 个字节的
        数据，然后将数据方法到 buffer 中，位置从下标为 offset 开始，并返回实际读取的字节数，如果流已经读完了，则返回
        0。这个过程中，Position 会往后移动实际读取长度，如果流支持搜索，程序中可以调用这个属性
        所以这里就有会这样的一个限制：offset + count <= buffer.Length，换句话说，偏移量 + 最大读取数目不能大于缓
        存数组的长度。
        因为这个方法返回一个实际读取长度，可能有人会这样判断是否读完：根据返回的结果与count比，如果返回的长度小于count
        则认为流已经读完；否则流还没读完。
        有一些流可能会达成这样的效果，但是很多流并不能以此为依据来判断流是否读完，也许某一次读取长度小于count，然后再读
        一次发现又有数据了。这是因为IO在系统中属于高耗时操作，大部分情况下IO的性能和程序的运算速度相差甚远。所以经常会出
        现这样的情景：流的长度是100，给了长度为100的缓存字节数组，然后第一次读取了10个字节，第二次读取了5个字节，这样一
        点一点的把这100个字节读取到。
        所以，必须以返回值为0作为流的读完判断依据。
        public virtual int ReadByte ();
        这个方法很简单，每次从流里读取一个字节的数据，如果读取完成返回-1。可能有人会疑惑了，这个方法明明是读取一个字节，
        也就是个byte，那为什么返回类型是int呢？很简单，因为byte没有负数，而int有。所以，当返回值不等于-1的时候，可以放
        心的类型转换为byte。
    4.把数据写入流
        public abstract void Write(byte[] buffer, int offset,int count);
        public virtual void WriteByte(byte value);
        流的写入与读取相比就简单多了，至少我们不用判断流的位置。现在简单分析一下：
        public abstract void Write (byte[] buffer, int offset, int count);
        表示从buffer的offset下标开始，取count个字节写入流里。所以，对offset、count的限制依旧，和不能大于数组的长度。
        写入成功，流的位置会移动,否则将保持现有位置。
        public virtual void WriteByte (byte value);
        这个方法就更简单了，直接写一个字节给流。
    5. 关闭或销毁流
    流在操作完成之后，需要将其关闭以释放流所持有的文件或IO设备等资源。很多人在使用电脑的时候，不能用QQ发送在本地已经打开的
    excel文件，它会提示文件被占用无法传输。这就是因为Excel打开了这个文件，就持有一个文件相关的流，所以QQ无法发送。解决办
    法很简单，关掉excel软件即可。回到当前，也就是我们在使用完成之后必须关闭流。
    那么我们该如何关闭流呢？调用以下方法：
        public virtual void Close ();
    C#虽然设置了Close方法，但是并不支持开发者在编写程序的时候手动调用Close方法，更推荐使用：
        public void Dispose ();
    这个方法会将释放流所持有使用的资源，并关闭流。
    当前需要注意的一个地方是，在把流关闭或释放之前把流里的数据推送到基础设备，即调用：
        public abstract void Flush ();
    有一些流设置了自动推送功能，如果遇到这种流则不需要手动调用该方法。
    对于流来说，一旦销毁或关闭，这个流就无法二次使用了，所以调用了Close、Dispose之后再次尝试读取/写入流都会报错
    */


    /*
    目录，不严谨的来讲可以用文件夹代替。不过严格来说，目录指的是文件所在的文件夹以及文件夹的位置这些信息的集合。
    路径是指文件或文件夹所在的位置的字符串表示，有相对路径和绝对路径，有物理路径和网络路径等一系列这些划分。
        ·相对路径指的是，相对程序所在目录目标文件所在的目录路径
        ·绝对路径指的是从系统或者网站的目录起点开始文件所在的位置，也就是说无论程序在哪都能通过绝对路径访问到对应文件
        ·物理路径是指文件在磁盘的路径，划分依据与之前的两种并不一致，所以不是并列关系
        ·网络路径是指网络或文件是在网络服务上部署的，通过URI访问的路径信息
    */

    /*
    File 和 FileInfo
        public static class File;// 静态类不可以实例化
        public sealed class FileInfo : System.IO.FileSystemInfo // sealed 类不可以被继承 
    我们忽略突然冒出来的FileSystemInfo，只需要明白它是FileInfo的基类即可。
    通过两个类的声明方式，可以看出File是一个工具类，而FileInfo则是文件对象。所以，File更多的用在快速操作文件并不需要
    长时间多次使用同一个文件的场景，而FileInfo则适合同一个文件的多次使用。
    我们先来看下File支持哪些操作：
    a.文件读取
        public static byte[] ReadAllBytes (string path);
        public static string[] ReadAllLines (string path);
        public static string[] ReadAllLines (string path, System.Text.Encoding encoding);
        public static string ReadAllText (string path);
        public static string ReadAllText (string path, System.Text.Encoding encoding);
        public static System.Collections.Generic.IEnumerable<string> ReadLines (string path);
    先从名称上分析方法应该是什么，应该具有哪些功能？
    ReadAllBytes以二进制的形式一次性把文件全部读出来
    ReadAllLines打开文本文件，将文件内容一行一行的全部读出来并返回
    ReadAllText打开文件，并将文件所有内容一次性读出来
    ReadLines 这是一个新的方法，根据返回值和方法名称，可以判断它应该与ReadAllLines有着类似的行为
        ReadLInes和ReadAllLines的区别：
        ReadAllLines返回的是字符串数组，所以该方法会一次性将文件内容全部读出
        ReadLines返回的是一个可枚举对象，根据之前在Linq系列和集合系列的知识，我们能判断出，这个方法不会立即返回数据
        所以我们很轻易的就能得出，ReadAllLines不会过久的持有文件对象，但是不适合操作大文件；ReadLines对于大文件的
        操作更擅长一些，但是可能会更久的持有文件
    */

    //public System.IO.StreamWriter AppendText();创建一个流适配器，在适配器中追加文本到文件中
    //public System.IO.FileInfo CopyTo(string destFileName);将现有文件复制到新文件中，并返回新文件的实例，不支持覆盖
    //public System.IO.FileInfo CopyTo(string destFileName,bool overwrite);根据 overwrite 确定是否覆盖
    //public System.IO.StreamWriter Create(); 创建当前对象代表的文件，并返回一个文件流
    //public System.IO.StreamWriter CreateText();与 AppendText 类似，但是会覆盖文件原有内容
    //public override void Delete();删除文件
    //public void MoveTo(string destFileName);将文件移动到新文件，不支持覆盖已存在的文件
    //public void MoveTo(string destFileName,bool overwrite);根据 overwrite 确定是否覆盖
    //public System.IO.FileStream Open(System.IO.FileMode mode);根据模式打开文件
    //public System.IO.FileStream Open(System.IO.FileMode mode, System.IO.FileAccess access);根据权限和模式，打开文件
    //public System.IO.FileStream OpenRead();打开一个只能读取的文件流
    //public System.IO.FileStream OpenText();打开一个读流适配器
    //public System.IO.FileStream OpenWrite();打开一个只能写的流
    class CSharpIO {
        static void Main0(string[] args) {
            string[] paths = { @"d:\archives","2010","media","images"};
            string fullPath = Path.Combine(paths);
            Console.WriteLine(fullPath);

            paths = new string[] { @"d:\archives\", @"2010\", "media", "images" };
            fullPath = Path.Combine(paths);
            Console.WriteLine(fullPath);

            paths = new string[] { @"d:/archives/", "2010/", "media", "images" };
            fullPath = Path.Combine(paths);
            Console.WriteLine(fullPath);

        }
    }

    class Program {

        static void Main0(string[] args) {
            var directory = Directory.GetCurrentDirectory();
            var program = File.Open("../../../Program.cs",FileMode.OpenOrCreate);
            //program = File.Open("Program.cs",FileMode.OpenOrCreate);
            var buffers = new byte[1024];
            var list = new List<byte>();
            while (true) {
                int length = program.Read(buffers,0,buffers.Length);
                if (length > 0) {
                    break;
                }
                list.AddRange(buffers.Take(length));
            }

            program.Close();
            Console.WriteLine(list.Count);
            // 到目前为止，打开了一个流读取当前程序源文件，每次读取到一个字节数组里，然后将数据放到list集合里，在读取完成后关闭这个流。虽然以上流并没有太多意义，但是基本演示了一下流的读取操作。

            // 注意到注释的那行代码和上一行代码的区别吗？在编译阶段，Directory.GetCurrentDirectory()表示源文件所在目录；在运行阶段，表示程序编译完成的DLL所在目录。
        }
    }
}
