using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

/**
 * @author zlim
 * @create 2020/5/20 18:40:07
 * 逆波兰表达式的分布类 > 核心数学
 */
namespace LoongEgg.MathPro {
    /// <summary>
    /// 逆波兰表达式
    /// </summary>
    public static partial class ReversePolishNotation {
        
        // TODO：35-2 List to Queue，将中缀表达式转为后缀表达式
        /// <summary>
        /// 转换为后缀表达式
        /// </summary>
        /// <param name="tokens">中缀表达式的集合</param>
        /// <returns>逆波兰表达式的转换结果</returns>
        public static Queue<Token> ConvertToPostFix(List<Token> tokens) {
            Debug.WriteLine("Begin to Convert to Post Fix ... ");

            // Stack of tokens with a type of
            // TokenType.Function, TokenType.Operator or Bracket
            // [LAST IN FIRST OUT]
            Stack<Token> stack = new Stack<Token>();

            // [FIRST IN FIRST OUT]
            Queue<Token> queue = new Queue<Token>();

            stack.Push(new Token('('));
            tokens.Add(new Token(')'));

            tokens.ForEach(token => {

                switch (token.Type) {
                    case TokenType.Operator:
                        if (stack.Count == 0) {
                            Debug.WriteLine($"stack.Push({token})");
                            stack.Push(token);
                        } else {
                            Token last = stack.Pop();
                            Debug.WriteLine($"stack.Pop() > {last}");
                            if (last.Type == TokenType.LeftBracket || 
                                last.Type == TokenType.RightBracket || 
                                token.Priority >= last.Priority) {
                                Debug.WriteLine($"stack.Push({token})");
                                stack.Push(last);

                                Debug.WriteLine($"stack.Push({token})");
                                stack.Push(token);

                            }
                        }
                        break;

                    case TokenType.Function:
                        Debug.WriteLine($"stack.Push({token})");
                        stack.Push(token);
                        break;

                    case TokenType.Operand:
                        Debug.WriteLine($"queue.Enqueue({token})");
                        queue.Enqueue(token);
                        break;

                    case TokenType.LeftBracket:
                        break;
                    case TokenType.RightBracket:
                        break;
                    default:
                        break;
                }

            });



            return queue;
        }

    }
}
