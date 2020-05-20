using System;
using System.Collections.Generic;
using System.Text;

/**
 * @author zlim
 * @create 2020/5/20 16:05:43
 */
namespace LoongEgg.MathPro {
    public static partial class ReversePolishNotation {

        /// <summary>
        /// 普通计算
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        /// <param name="opr">操作符</param>
        /// <returns>计算结果</returns>
        private static double CalculateOpr(string left, string right, string opr) => CalculateOpr(Convert.ToDouble(left),Convert.ToDouble(right),opr);

        /// <summary>
        /// 普通计算
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        /// <param name="opr">操作符</param>
        /// <returns>计算结果</returns>
        private static double CalculateOpr(double left,double right,string opr) {

            switch (opr) {
                case "+":return left + right;
                case "-":return left - right;
                case "*":return left * right;
                case "/":return left / right;
                case "^":return Math.Pow(left,right);

                default:
                    throw new NotImplementedException(opr);
            }

        }


        /// <summary>
        /// 函数运算
        /// </summary>
        /// <param name="fun">函数名称</param>
        /// <param name="d">操作数</param>
        /// <returns></returns>
        private static double CalculateFun(string fun, string d) => CalculateFun(fun,Convert.ToDouble(d));


        const double Deg2Rad = Math.PI / 180.0;
        /// <summary>
        /// 函数运算：[注意]角的大小以角度记，不是弧度，而 Math.Cos/Sin() 方法都是计算弧度，因此需要做一下转换
        /// </summary>
        /// <param name="fun"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double CalculateFun(string fun,double d) {

            switch (fun) {
                case "cos":return Math.Cos(d * Deg2Rad);
                case "sin":return Math.Sin(d * Deg2Rad);
                case "sqr":return d * d;// 平方
                case "sqrt":return Math.Sqrt(d);

                default:
                    throw new NotImplementedException($"{fun} ??? ");
            }

        }
    
    }
}
