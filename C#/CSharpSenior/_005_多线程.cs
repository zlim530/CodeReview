using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace CSharpSenior {
    class _005_多线程 {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            Thread thread = new Thread(new ThreadStart(DoWork));
            thread.Start();

            Thread.Sleep(10);
            thread.Abort();

            Thread parameterizedThread = new Thread(new ParameterizedThreadStart(DoWorkWithParam));
            parameterizedThread.Start();

        }

        private static void DoWork() {
            try {
                for (int i = 0; i < 10; i++) {
                    Console.WriteLine("Work Thread:",i.ToString());
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Thread.ResetAbort();
            }

            Console.WriteLine("Work Thread:still alive and working.");
            Thread.Sleep(500);
            Console.WriteLine("Work Thread:finished working.");
        
        }

        public static void DoWorkWithParam(object obj) {
            string msg = (string)obj;
            Console.WriteLine("Parameterized Work Thread:"+msg);
        }

        /// <summary>
        /// 获取程序集中的所有公共类型
        /// </summary>
        /// <param name="assembly"></param>
        static void GetExportedTypes(Assembly assembly) {
            var types = assembly.GetExportedTypes();

            foreach (var item in types) {
                Console.WriteLine(item.Name);
            }
        }

        static void Main1(string[] args) {
            var assembly = Assembly.Load("ReflectionDemo.A,Version=1.0.0.0,Culture=neutral,PublicKeyToken=null");

            var assembly2 = Assembly.LoadFrom(@"http://www.a.com/ReflectionDemo.A.dll");

            var path = string.Format(@"{0}\{1}",AppDomain.CurrentDomain.BaseDirectory,@"plugins\ReflectionDemo.A.dll");
            var assembly3 = Assembly.LoadFile(path);

        }
    }
}
