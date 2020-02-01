import java.util.Stack;

// 给定一个平衡括号字符串 S，按下述规则计算该字符串的分数：

// () 得 1 分。
// AB 得 A + B 分，其中 A 和 B 是平衡括号字符串。
// (A) 得 2 * A 分，其中 A 是平衡括号字符串。
//  

// 示例 1：

// 输入： "()"
// 输出： 1
// 示例 2：

// 输入： "(())"
// 输出： 2
// 示例 3：

// 输入： "()()"
// 输出： 2
// 示例 4：

// 输入： "(()(()))"
// 输出： 6
//  

// 提示：

// S 是平衡括号字符串，且只含有 ( 和 ) 。
// 2 <= S.length <= 50

/**
 * ScoreOfParentheses
 */
// LeetCode：856.括号的分数
// https://leetcode-cn.com/problems/score-of-parentheses/
public class ScoreOfParentheses {

    public int scoreOfParenthese(String S) {
        Stack<Integer> stack = new Stack<>();
        stack.push(0);
        for (char c : S.toCharArray()) {
            if (c == '(') {
                stack.push(0);
            } else {
                int v = stack.pop();
                int w = stack.pop();
                stack.push(w+Math.max(v * 2, 1));
            }
        }
        return stack.pop();
    }
}