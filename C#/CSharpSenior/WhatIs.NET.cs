using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace CSharpSenior {

    /*
       微软公司是全球最大的电脑软件提供商，为了占据开发者市场，进而在2002年推出了
       Visual Studio(简称VS，是微软提供给开发者的工具集).NET 1.0版本的开发者平台。
       为了吸引更多的开发者涌入平台，微软还在2002年宣布推出一个特性强大并且与.NET 平台
       无缝集成的编程语言，即 C# 1.0正式版本。

       跨语言：即只要是面向.NET 平台的编程语言，用其中一种语言编写的类型可以无缝地用在另一种
       语言编程的应用程序中的互操作性。

       跨平台：一次编译，不需要任何代码修改，应用程序就可以运行在任意有.NET 框架实现的平台上，即
       代码不依赖于操作系统，也不依赖硬件环境。
       （但是在早期微软推出的.NET FrameWork 只有 Windows 版本）

       但是，如果我想不仅仅局限于 C# 与 VB，我还想编写的代码在.NET 平台上通用的话，那么我就必须
       知道.NET 平台支持的每一种语言和我编写代码所使用的语言的差异，从而在编写代码的过程中避免这些。

       这几年编程语言层出不穷，在将来.NET 平台可能还会支持更多的语言，如果说对一个开发者而言掌握所有
       语言的差异性是不现实的，所以.NET 专门为此参考每种语言并找出来语言间的共性，然后定义了一组规则，
       开发者都遵守这个规则来编码，那么代码就可以被任意.NET 平台支持的语言所通用。

       而与其说是规则，不如说它是一组语言互操作的标准规范，它就是公共语言规范-Common Language Specification
       简称 CLS。

       值得一提的是，CLS 规则只是面向那些公开可以被其他程序集访问的成员，如 public、继承的 protected，对于
       程序集的内部成员如 privat、internal 则不会执行该检测规则。也就是说，所适应的 CLS 遵从性规则，仅是那些
       公开的成员，而非私有实现。

       什么是类库？
       在 CTS 中有一条就是要求基元数据类型的类库。我们先搞清楚什么是类库？类库就是类的逻辑集合，你开发工作中
       你用过或自己编写过很多工具类，比如搞 Web 的经常要用的 JsonHelper、XmlHelpe、HttpHelper 等等，这些
       类通常都会在命名为 Tool、Utility 等这样的项目中。像这些类的集合我们可以在逻辑上称之为“类库”，比如这个
       Helper 我们统称为工具类库。

       什么是基础类库BCL？
       当你通过VS创建一个项目后，你这个项目就已经引用好了通过.NET下的语言编写好的一些类库。比如控制台中你直接就
       可以用ConSole类来输出信息，或者using System.IO 即可通过File类对文件进行读取或写入操作，这些类都是微软
       帮你写好的，不用你自己去编写，它帮你编写了一个面向.NET的开发语言中使用的基本的功能，这部分类，我们称之为
       BCL（Base Class Library）， 基础类库，它们大多都包含在System命名空间下。

       基础类库BCL包含：基本数据类型，文件操作，集合，自定义属性，格式设置，安全属性，I/O流，字符串操作，事件日志等的类型

       什么是框架类库FCL？
       有关BCL的就不在此一一类举。.NET之大，发展至今，由微软帮助开发人员编写的类库越来越多，这让我们开发人员开发
       更加容易。由微软开发的类库统称为：FCL，Framework Class Library ，.NET框架类库，我上述所表达的BCL就是
       FCL中的一个基础部分，FCL中大部分类都是通过C#来编写的。

       在FCL中，除了最基础的那部分BCL之外，还包含我们常见的 如 ： 用于网站开发技术的 ASP.NET类库，该子类包含
       webform/webpage/mvc，用于桌面开发的 WPF类库、WinForm类库，用于通信交互的WCF、asp.net web api、
       Web Service类库等等

       什么是 CLR,.NET 虚拟机？
       实际上，.NET 不仅提供了自动内存管理的支持，它还提供了一些例如类型安全、应用程序域、异常机制等支持，
       这些都被统称为 CLR 公共语言运行库。

       CLR 是.NET 类型系统的基础，所有的.NET 技术都是建立在此之上，熟悉它可以帮助我们更好的理解框架组件的核心、原理。

       在我们执行托管代码之前，总会先运行这些运行库代码，通过运行库的代码调用，从而构成了一个用来支持托管程序的运行环境，
       进而完成诸如不需要开发人员手动管理内存，一套代码即可在各大平台跑的这样的操作。

       这套环境及体系值完善，以至于就像一个小型的系统一样，所以通常形象的称 CLR 为“.NET 虚拟机”。那么，如果以进程为最低端，
       进程上面的就是.NET 虚拟机（CLR），而虚拟机的上面才是我们的托管代码。换句话说，托管程序实际上是寄宿于.NET 虚拟机中。



       */

    public class BaseBusiness {
        public unsafe int* pointer;
    }

    [Flags]
    public enum BorderSides {
        Left = 1,
        Right = 2,
        Top = 4,
        Bottom = 8
    }

    class WhatIs {
        static void Main0() {

            #region Flags 特性的 enum 枚举类型

            //BorderSides bs = BorderSides.Left | BorderSides.Right;
            //Console.WriteLine(bs.ToString());

            #endregion
        }
    }


    class Program {
        static void Main0() {
            string rootDirectory = Environment.CurrentDirectory;
            Console.WriteLine("开始连接，端口号：8090");
            Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            socket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Loopback,8090));
            socket.Listen(30);
            byte[] statusBytes = new byte[4096];
            byte[] headerBytes = new byte[4096];
            byte[] bodyBytes = new byte[4096];
            /*byte[] statusBytes, headerBytes, bodyBytes;*/
            while (true) {
                Socket socketClient = socket.Accept();
                Console.WriteLine("新请求");
                byte[] buffer = new byte[4096];
                int length = socketClient.Receive(buffer,4096,SocketFlags.None);
                string requestStr = Encoding.UTF8.GetString(buffer,0,length);
                Console.WriteLine(requestStr);
                string[] strs = requestStr.Split(new string[] { "\r\n"},StringSplitOptions.None);
                string url = strs[0].Split(' ')[1];

                //byte[] statusBytes, headerBytes, bodyBytes;

                if (Path.GetExtension(url) == ".jpg") {
                    string status = "HTTP/1.1 200 OK\r\n";
                    statusBytes = Encoding.UTF8.GetBytes(status);
                    bodyBytes = File.ReadAllBytes(rootDirectory + url);
                    string header = string.Format("Content-Type:image/jpg;\r\ncharset=UTF-8\r\nContent-Length:{0}\r\n", bodyBytes.Length);
                    headerBytes = Encoding.UTF8.GetBytes(header);
                } else {
                    if (url == "/") {
                        url = "默认页";
                        string status = "HTTP/1.1 200 OK \r\n";
                        statusBytes = Encoding.UTF8.GetBytes(status);
                        string body = "<html>" +
                            "<head>" +
                                "<title>socket webServer -- Login</title>" +
                            "</head>" +
                            "<body>" +
                                "<div style=\"text-algin:center\">" +
                                    "当前访问" + url +
                                "</div>" +
                            "<body>" +
                        "<html>";
                        bodyBytes = Encoding.UTF8.GetBytes(body);
                        string header = string.Format("Content-Type:text/html;charset=UTF-8\r\nContent-Length:{0}\r\n",bodyBytes.Length);
                        headerBytes = Encoding.UTF8.GetBytes(header);

                    }
                }
                socketClient.Send(statusBytes);
                socketClient.Send(headerBytes);
                socketClient.Send(new byte[] { (byte)'\r', (byte)'\n' });
                socketClient.Send(bodyBytes);

                socketClient.Close();

            }
        }
    }

    
    
}
