using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

/**
 * @author zlim
 * @create 2020/6/12 20:46:19
 */
namespace CSharpSenior {
    public class Mixed {

        /// <summary>
        /// 获取类型的成员
        /// </summary>
        /// <param name="args"></param>
        static void Main0(string[] args) {
            // GetMembers 方法也可以不传 BindingFlags，默认返回的是所有公开的成员。
            var members = typeof(object).GetMembers(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            foreach (var member in members) {
                Console.WriteLine($"{member.Name} is a {member.MemberType}");
            }
        }

        /// <summary>
        /// 获取并调用对象的方法
        /// </summary>
        /// <param name="args"></param>
        static void Main1(string[] args) {
            var str = "hello";
            var method = str.GetType().GetMethod("Substring",new[] { typeof(int),typeof(int)});
            var result = method.Invoke(str,new object[] { 0,4});// 相当于 str.Substring(0,4);
            Console.WriteLine(result);// hell

            var method2 = typeof(Math).GetMethod("Exp");
            // 对于静态方法，则对象参数传空：
            var result2 = method2.Invoke(null,new object[] { 2});// 相当于 Math.Exp(2);
            Console.WriteLine(result2);// 输出(e^2):7.38905609893065

        }

        /// <summary>
        /// 如果是泛型方法，则还需要通过泛型参数来创建泛型方法
        /// </summary>
        /// <param name="args"></param>
        static void Main2(string[] args) {
            // 反射调用泛型方法
            MethodInfo method1 = typeof(Sample).GetMethod("GenericMethod");
            MethodInfo generic1 = method1.MakeGenericMethod(typeof(string));
            string sample = "sample";
            generic1.Invoke(sample,null);

            // 反射调用静态泛型方法
            MethodInfo method2 = typeof(Sample).GetMethod("StaticMethod");
            MethodInfo generic2 = method2.MakeGenericMethod(typeof(string));
            generic2.Invoke(null,null);
        }

    }

    public class Sample {
        public void GenericMethod<T>() { 
        
        }

        public static void StaticMethod<T>() { 
        
        }
    }
}
