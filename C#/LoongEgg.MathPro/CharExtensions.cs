using System;
using System.Collections.Generic;
using System.Text;

/**
 * @author zlim
 * @create 2020/5/19 21:37:14
 */
/*
扩展方法类一般不能让它实例化，因此常设为 static 类，并且通常为什么数据类型添加扩展方法，这个类就叫 XxxExtensions
*/
namespace LoongEgg.MathPro {
    // TODO:30-1 字符的扩展方法-类型判断
    /// <summary>
    /// 字符的扩展方法
    /// </summary>
    public static class CharExtensions {

        /// <summary>
        /// 是操作数？0-9或.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsDigit(this char self) => ".0123456789".Contains(self);

        /// <summary>
        /// 是操作符？+-*/^
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsOperator(this char self) => "+-*/^".Contains(self);

        /// <summary>
        /// 是字母？
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsLetter(this char self) => 'a' <= self && self <= 'z' || 'A' <= self && self <= 'Z';

        /// <summary>
        /// 是左括号？
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsLeftBracket(this char self) => '(' == self;
        
        /// <summary>
        /// 是右括号
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsRightBracket(this char self) => ')' == self;
    }
}
