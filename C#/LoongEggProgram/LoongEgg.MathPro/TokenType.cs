using System;

namespace LoongEgg.MathPro {

    
    /// <summary>
    /// Token 的类型枚举
    /// </summary>
    public enum TokenType {
        
        /// <summary>
        /// 运算符：+ - * / ^
        /// </summary>
        Operator,

        /// <summary>
        /// 函数：cos sin etc ... 
        /// </summary>
        Function,

        /// <summary>
        /// 操作数：.0123456789
        /// </summary>
        Operand,

        /// <summary>
        /// 左括号：(
        /// </summary>
        LeftBracket,

        /// <summary>
        /// 右括号：)
        /// </summary>
        RightBracket,
    }
}
