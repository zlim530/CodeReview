using System;
using System.Collections.Generic;
using System.Text;

/**
 * @author zlim
 * @create 2020/5/19 23:23:02
 */
namespace LoongEgg.MathPro {
    // TODO：31-1 词元的基本属性，[partial]分部类
    /// <summary>
    /// 数学表达式的词元
    /// </summary>
    public partial class Token {
        /// <summary>
        /// 词元的类型
        /// </summary>
        public TokenType Type { get; set; }

        /// <summary>
        /// 将原字符串全部.ToLower()
        /// </summary>
        public string NormalizeString { get; private set; }

        /// <summary>
        /// 运算的优先级
        /// </summary>
        public int Priority { get; private set; } = -1;

        /// <summary>
        /// 主构造器
        /// </summary>
        /// <param name="token"></param>
        public Token(string token) {
            this.NormalizeString = token.ToLower();
            this.Type = GetTokenType(this.NormalizeString);
            this.Priority = GetTokenPriority(this.Type, this.NormalizeString);
        }

        public Token(char token) : this(token.ToString()) { }
        
    }
}
