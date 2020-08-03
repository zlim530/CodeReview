using System;

namespace ReflectAndAttribute{
    public class Program{
        public static void Main(string[] args){
            // 运行时
            Console.WriteLine("".GetType().Module);
            // System.Private.CoreLib.dll
            // 编译时
            Console.WriteLine(typeof(Int32).Assembly);
            // System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e
        }
    }
}
