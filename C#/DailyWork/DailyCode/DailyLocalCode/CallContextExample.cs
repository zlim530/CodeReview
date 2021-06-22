using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
在.NET Framework中，Microsoft给我们设计了一个CallContext类。

命名空间：System.Runtime.Remoting.Messaging

类型完全限定名称：System.Runtime.Remoting.Messaging.CallContext

CallContext类似于方法调用的线程本地存储区的专用集合对象，并提供对每个逻辑执行线程都唯一的数据槽。数据槽不在其他逻辑线程上的调用上下文之间共享。当 CallContext 沿执行代码路径往返传播并且由该路径中的各个对象检查时，可将对象添加到其中。

简而言之，CallContext提供线程（多线程/单线程）代码执行路径中数据传递的能力。 

*/
namespace CallContextExample
{
    public class CallContextExample
    {
        public void TestGetSetData()
        {
            Console.WriteLine($"Current ThreadId = {Thread.CurrentThread.ManagedThreadId}");
            var users = new User()
            {
                Id = DateTime.Now.ToString(),
                Name = "Edison"
            };
        }
    }

    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }

}
