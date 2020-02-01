import java.util.Stack;

/* 给定一个只包括 '('，')'，'{'，'}'，'['，']' 的字符串，判断字符串是否有效。

有效字符串需满足：

左括号必须用相同类型的右括号闭合。
左括号必须以正确的顺序闭合。
注意空字符串可被认为是有效字符串。

示例 1:

输入: "()"
输出: true
示例 2:

输入: "()[]{}"
输出: true
示例 3:

输入: "(]"
输出: false
示例 4:

输入: "([)]"
输出: false
示例 5:

输入: "{[]}"
输出: true
 */

/**
 * IsValid
 */
// LeetCode：20.有效的括号
// https://leetcode-cn.com/problems/valid-parentheses/
public class IsValid {

    public boolean isValid(String s){
        Stack<Character> stack = new Stack<Character>();
        int size = stack.size();
        for (int i = 0; i < size; i++) {
            char c = s.charAt(i);
            if ( c == '(' || c == '{' || c == '[') {
                stack.push(c);
            } else {
                if ( stack.isEmpty()) {
                    return false;
                }
                char left = stack.pop();
                if (left == '(' && c != ')') {
                    return false;
                }
                if (left == '{' && c != '}') {
                    return false;
                }
                if ( left == '[' && c != ']') {
                    return false;
                }
            }
        }
        return stack.isEmpty();
    }
}