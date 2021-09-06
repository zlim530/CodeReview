using System;
using System.Linq;
using System.Xml.Linq;

/**
 * @author zlim
 * @create 2020/8/23 21:00:01
 */
namespace XML {
    /*
    什么是 XML（Extensible Markup Language）：是一种文本文件格式，被设计用来传输和存储数据（现在一般用来存储数据，传输数据一般都用 JSON 了）
    由标签和属性组成元素（即节点），由元素组成“树状结构”
    必须有且只有一个根节点
    其他：
        ·大小写敏感
        ·注释
        ·实体引用（保留字符替代）
    */
    public class XML的生成与查询 {
        /// <summary>
        /// XML 文件的生成和加载
        /// </summary>
        /// <param name="args"></param>
        public static void Main0(string[] args) {
            //XElement xElement = new XElement("root",new XElement("name",32));
            //Console.WriteLine(xElement);
            /*< root >
              < name > 32 </ name >
            </ root >*/

            // 一个构造函数写完 XML 文件
            XElement xElement1 = new XElement("luckystack", // 根结点：有且只有一个
                new XComment("源栈欢迎您！"),         // 注释 <!-- -->
                new XElement("location","CQ",       // XML 元素（节点）
                    new XAttribute("head",true)),   // 元素属性
                new XElement("teachers",
                    new XElement("name","大飞哥",
                        new XAttribute("postion","head"),
                        new XAttribute("age",1)),
                    new XElement("name","小鱼",new XAttribute("postion","UI")),
                    new XElement("name","阿杰")
                )
            );

            //Console.WriteLine(xElement1);
            // string XNode.ToString():返回此节点的缩进 XML：重写了 object 中的 ToString 方法
            //Console.WriteLine(xElement1.ToString());
            /*
            <luckystack>
              <!--源栈欢迎您！-->
              <location head="true">CQ</location>
              <teachers>
                <name postion="head" age="1">大飞哥</name>
                <name postion="UI">小鱼</name>
                <name>阿杰</name>
              </teachers>
            </luckystack>
            */

            // Save(string fileName)：将此元素序列化为文件：使用此方法会自动在生成的序列化文件的首行添加 XMl 声明，如下所示：
            // <?xml version="1.0" encoding="utf-8"?>
            //xElement1.Save(@"C:\Users\Lim\Desktop\code\CodeReview\C#\dotnet\LINQAndXML\luckystack.xml");

            // 手动添加一个 XML 声明
            XDocument document = new XDocument(new XDeclaration("1.1","utf-16","yes"),xElement1);
            //document.Save(@"C:\Users\Lim\Desktop\code\CodeReview\C#\dotnet\LINQAndXML\luckystack.xml");

            //Console.WriteLine(document);
            /*
            XML 声明只能在文件中才能显示，为什么？
                ·Console.WriteLine(xElement1) => Console.WriteLine(xElement1.ToString())
                ·XDocument.ToString() => XNode.ToString()
            因为在 XElement 或者 XDocument 的重写的 ToString() 方法中声明了
            ws.OmitDeclaration = true; (其中 omit 是省略的的意思)
            */
            /*
            <luckystack>
              <!--源栈欢迎您！-->
              <location head="true">CQ</location>
              <teachers>
                <name postion="head" age="1">大飞哥</name>
                <name postion="UI">小鱼</name>
                <name>阿杰</name>
              </teachers>
            </luckystack>
            */

            #region XML 的继承关系
            /*
            ·XElement：XContainer：XNode
            ·XDocument：XContainer
            ·XAttribute：XObject：IXmlLineInfo
            ·XComment：XNode
            类/接口的声明：
            public class XElement : XContainer, IXmlSerializable
            public abstract class XContainer : XNode
            public abstract class XNode : XObject
            public abstract class XObject : IXmlLineInfo
            public interface IXmlLineInfo
            */
            #endregion

            #region 加载 XML 文件

            // 从 XML 文件中加载 XElement 对象到内存
            XElement xElement = XElement.Load(@"C:\Users\Lim\Desktop\code\CodeReview\C#\dotnet\LINQAndXML\luckystack.xml");
            Console.WriteLine(xElement);
            Console.WriteLine();
            // 获取此节点的第一个子节点
            Console.WriteLine(xElement.FirstNode);
            // <!--源栈欢迎您！-->
            Console.WriteLine();
            // 获取具有指定的 XName 的第一个（按文档顺序）子元素
            Console.WriteLine(xElement.Element("teachers").FirstNode);
            // <name postion="head" age="1">大飞哥</name>

            Console.WriteLine();
            //Console.WriteLine((new Student("zlim") { Age = 23}).ToString());

            #endregion

            #region 显式转换与隐式转换
            // public static implicit operator XName(string expandedName);
            //XName xName = "zlim";

            /*var student = new Student("zlim") {Age = 23 };
            Console.WriteLine((int)student);
            Console.WriteLine(student);*/
            #endregion

        }


        /// <summary>
        /// LINQ to XML 查询
        /// </summary>
        /// <param name="args"></param>
        public static void Main1(string[] args) {
            
            XElement xElement = XElement.Load(@"C:\Users\Lim\Desktop\code\CodeReview\C#\dotnet\LINQAndXML\luckystack.xml");

            //查：其实是最核心的内容，因为实现要找到进行操作的对象或位置
            //简单查找可以直接使用XNode及其子类的自有属性：
            var teachers = from x in xElement.Descendants()// 按文档顺序返回此文档或元素的子代元素【集合】
                           where x.Name == "name"
                           where !x.Value.Contains("飞哥")
                           where !x.HasAttributes
                           select x;
            foreach (var item in teachers) {
                Console.WriteLine(item);
            }

        }
    }

    public class Student {
        private string _name;
        public Student(string name) {
            _name = name;
        }

        public int Age { get; set; }
        public string Name {
            get { return _name; }
        }

        /// <summary>
        /// 显式转换：格式是固定的，前面一定是 public static explicit/implicit operator 
        /// 输入参数是可以转换的对象，int 表示可以将 Student 对象显式转换为 int
        /// 是如何转换的呢？-> 就是将 Student 对象的 Age 转换为 int 
        /// </summary>
        /// <param name="student"></param>
        public static explicit operator int(Student student) {
            return student.Age;
        }

        /// <summary>
        /// 隐式转换：表示可以将 Student 对象隐式转换为 string 
        /// </summary>
        /// <param name="student"></param>
        public static implicit operator string(Student student) {
            return student.Name;
        }

        public override string ToString() {
            return this.Name + " is " + this.Age + " years old.";
        }
    }
}
