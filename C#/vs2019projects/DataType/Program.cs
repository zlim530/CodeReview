using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataType
{
    class Program
    {
        static void Main(string[] args)
        {
            ////使用var关键字可以让编译器自动推导类型
            //var x = 3;
            //Console.WriteLine(x.GetType().Name);//int32
            //var y = 3L;
            //Console.WriteLine(y.GetType().Name);//int64
            //var c = 3.0;
            //Console.WriteLine(c.GetType().Name);//double
            //var d = 3.0f;
            //Console.WriteLine(d.GetType().Name);//single 即float

            Console.WriteLine("Size of int : {0}",sizeof(int));//4

            //@（称作"逐字字符串"）即在@""双引号中的所有内容都会原样输出
            //@ 字符串中可以任意换行，换行符及缩进空格都计算在字符串长度之内
            //如果想在@""中包好双引号 只需在双引号前面再加上一个双引号即可 
            string str = @"<script type = ""text/javascript"">               
                <!--
                -->
                </script>";
            Console.WriteLine(str);
            /*
             <script type = "text/javascript">
                <!--
                -->
                </script>
             */


        }
    }
}
