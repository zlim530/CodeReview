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

        

    }
}
