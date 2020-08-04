using System;
using System.Collections.Generic;
using System.Text;

/**
 * @author zlim
 * @create 2020/5/19 23:31:32
 */

namespace LoongEgg.MathPro {
    public partial class Token {

        // TODO：31-1 优先级
        /// <summary>
        /// token 的运算级别
        /// </summary>
        /// <param name="type"> token 的类型</param>
        /// <param name="token"> ToLower 后的 token 字符串</param>
        /// <returns>运算级数字越大的优先</returns>
        public static int GetTokenPriority(TokenType type, string token) {
            
            int priority = -1;
            switch (type) {
                case TokenType.Operator: {
                        if ( "+-".Contains(token[0])) {
                            priority = 1;
                        } else  if("*/".Contains(token[0])) {
                            priority = 2;
                        } else if ("^".Contains(token[0])) {
                            priority = 3;
                        } else {
                            throw new NotImplementedException($"{token}");
                        }
                    }
                    break;
                case TokenType.Function:
                    // 最高级别的
                    priority = 4;
                    break;
                case TokenType.Operand:
                case TokenType.LeftBracket:
                case TokenType.RightBracket:
                    priority = 1;
                    break;
                default:
                    throw new ArgumentException($" token tyoe = {type.ToString()} ???");
            }
            return priority;

        }

        // TODO:31-2 类型判断
        /// <summary>
        /// 获取 Token 的类型
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static TokenType GetTokenType(char token) {

            if (token.IsDigit()) {
                if (token == '.') {
                    throw new ArgumentException("<.>不能单独构成操作数");
                }
                return TokenType.Operand;
            } else if(token.IsOperator()) {
                return TokenType.Operator;
            } else if (token.IsLeftBracket()) {
                return TokenType.LeftBracket;
            } else if (token.IsRightBracket()) {
                return TokenType.RightBracket;
            } else {
                throw new ArgumentException($"不合适的token{token}");
            }

        }

        public static TokenType GetTokenType(string token) {

            if (token == null) {
                throw new ArgumentNullException($"token = {token} ???");
            }
            if ( token.Length == 1) {
                return GetTokenType(token[0]);
            } else {
                if (double.TryParse(token,out double d)) {
                    return TokenType.Operand;
                } else {
                    return TokenType.Function;
                }
            }

        }

        // TODO：32-1 转译字符串到 Token
        /// <summary>
        /// 将字符串转译成<see cref="Token"/>的集合
        /// </summary>
        /// <param name="inp">待转译的字符串</param>
        /// <returns></returns>
        public static List<Token> Tokenize(string inp) {

            var ret = new List<Token>();

            string str = inp.RemoveSpace();

            int i = 0;
            int cnt = str.Length;
            StringBuilder token = new StringBuilder();
            char c;

            while ( i < cnt) {
                c = str[i];
                token = new StringBuilder(c.ToString());

                if ( c.IsDigit()) { // 如果是数字
                    while ( i + 1 < cnt && str[i+1].IsDigit()) {
                        token.Append(str[i + 1]);
                        i += 1;
                    }
                } else if (c.IsLetter()) {// 如果是字母
                    while (i + 1 < cnt && str[i+1].IsLetter() ) {
                        token.Append(str[i + 1]);
                        i += 1;
                    }
                } else if (c.IsOperator()) {
                    if ( c == '-' && (i == 0 || (i > 0 && str[i - 1].IsLeftBracket()))) {
                        while (i + 1 < cnt && str[i + 1].IsDigit()) {
                            token.Append(str[i + 1]);
                            i += 1;
                        }
                    }
                } else if ( c.IsLeftBracket() || c.IsRightBracket()) {
                    // do nothing
                } else {
                    throw new ArgumentException($"Undefine char : {c}");
                }

                ret.Add(new Token(token.ToString()));
                i += 1;
            }
            return ret;
        }


        // TODO：32-1 转译字符串到 Token 
        /// <summary>
        /// 将字符串转译成 Token 的集合
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return NormalizeString;
        }

    }
}
