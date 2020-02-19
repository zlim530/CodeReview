import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Stack;

// n 皇后问题研究的是如何将 n 个皇后放置在 n×n 的棋盘上，并且使皇后彼此之间不能相互攻击。
// 给定一个整数 n，返回所有不同的 n 皇后问题的解决方案。

// 每一种解法包含一个明确的 n 皇后问题的棋子放置方案，该方案中 'Q' 和 '.' 分别代表了皇后和空位。

// 示例:

// 输入: 4
// 输出: [
//  [".Q..",  // 解法 1
//   "...Q",
//   "Q...",
//   "..Q."],

//  ["..Q.",  // 解法 2
//   "Q...",
//   "...Q",
//   ".Q.."]
// ]
// 解释: 4 皇后问题存在两个不同的解法。

// LeetCode：51.N皇后
// https://leetcode-cn.com/problems/n-queens/submissions/

public class _51_solveNQueens {

    public List<List<String>> solveNQueens(int n) {
        List<List<String>> ans = new ArrayList<>();
        backtrack(new ArrayList<Integer>(), ans, n);
        return ans;
    }
    
    private void backtrack(List<Integer> currentQueen, List<List<String>> ans, int n) {
        // 当前皇后的个数是否等于 n 了，等于的话就加到结果中
        if (currentQueen.size() == n) {
            List<String> temp = new ArrayList<>();
            for (int i = 0; i < n; i++) {
                char[] t = new char[n];
                Arrays.fill(t, '.');
                t[currentQueen.get(i)] = 'Q';
                temp.add(new String(t));
            }
            ans.add(temp);
            return;
        }
        //尝试每一列
        for (int col = 0; col < n; col++) {
            //当前列是否冲突
            if (!currentQueen.contains(col)) {
                //判断对角线是否冲突
                if (isDiagonalAttack(currentQueen, col)) {
                    continue;
                }
                //将当前列的皇后加入
                currentQueen.add(col);
                //去考虑下一行的情况
                backtrack(currentQueen, ans, n);
                //将当前列的皇后移除，去判断下一列
                //进入这一步就是两种情况，下边的行走不通了回到这里或者就是已经拿到了一个解回到这里
                currentQueen.remove(currentQueen.size() - 1);
            }
    
        }
    
    }
    
    private boolean isDiagonalAttack(List<Integer> currentQueen, int i) {
        // TODO Auto-generated method stub
        int current_row = currentQueen.size();
        int current_col = i;
        //判断每一行的皇后的情况
        for (int row = 0; row < currentQueen.size(); row++) {
            //左上角的对角线和右上角的对角线，差要么相等，要么互为相反数，直接写成了绝对值
            if (Math.abs(current_row - row) == Math.abs(current_col - currentQueen.get(row))) {
                return true;
            }
        }
        return false;
    }

    

    // private int[] cols;
    
    // private int n;

    // private List<List<String>> res;

    // public List<List<String>> solveNQueens(int n) {
    //     this.n = n;
    //     res = new ArrayList<>();
    //     if ( n < 1) {
    //         return res;
    //     }
    //     cols = new int[n];
    //     Stack<Integer> stack = new Stack<>();
    //     // place(0);
    //     backTrack(0,stack);
    //     return res;
    // }

    // private void backTrack(int row, Stack<Integer> stack) {
    //     if ( row == cols.length ) {
    //         List<String> board = convert2Board(stack,n);
    //         res.add(board);
    //         return;
    //     }

    //     for (int col = 0; col < cols.length; col++) {
    //         if ( isValid(row, col)) {
    //             cols[row]  = col;
    //             stack.push(col);
    //             backTrack(col + 1, stack);
    //             stack.pop();
    //         }
    //     }

    // }

    // private boolean isValid(int row, int col) {
    //     for (int i = 0; i < row; i++) {
    //         if ( cols[i] == col) {
    //             return false;
    //         }
    //         if ( row - i == Math.abs(col - cols[i])) {
    //             return false;
    //         }
    //     }
    //     return false;
    // }

    // private void place(int row) {
    //     if ( row == cols.length) {
    //         // ways++;
    //         show();
    //         return;
    //     }

    //     for (int col = 0; col < cols.length; col++) {
    //         if ( isValid(row,col)) {
    //             cols[row] = col;
    //             place(col + 1);
    //         }
    //     }
    // }

    // private List<String> convert2Board(Stack<Integer> stack, int n) {
    //     List<String> board = new ArrayList<>();
    //     for (Integer num : stack) {
    //         StringBuilder sb = new StringBuilder();
    //         for (int i = 0; i < n; i++) {
    //             sb.append(".");
    //         }
    //         sb.replace(num, num+1, "Q");
    //         board.add(sb.toString());
    //     }
    //     return board;
    // }

    // private void show() {
    //     for (int i = 0; i < cols.length; i++) {
    //         for (int j = 0; j < cols.length; j++) {
    //             if ( cols[i] == j) {
    //                 System.out.println("Q");
    //             }else{
    //                 System.out.println(".");
    //             }
    //         }
    //     }
    // }

    
}