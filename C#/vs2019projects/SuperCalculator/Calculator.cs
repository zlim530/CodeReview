using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//创建类库项目 此项目是不可以生成可执行文件的 只会生成.dll文件 --> 自定义类库
namespace Tools
{
    public class Calculator
    {
        public static double Add(double a ,double b)
        {
            return a + b;
        }

        public static double Sub(double a, double b)
        {
            return a - b;
        }

        public static double Mul(double a, double b)
        {
            return a * b;
        }

        public static double Div(double a, double b)
        {
            if (b == 0)
            {
                return double.PositiveInfinity;
                //PositiveInfinity 表示无穷大
            }
            else
            {
                return a / b;
            }
        }
    }
}
