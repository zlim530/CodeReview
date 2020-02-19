import java.util.ArrayList;
import java.util.List;

// n 皇后问题研究的是如何将 n 个皇后放置在 n×n 的棋盘上，并且使皇后彼此之间不能相互攻击。
// 给定一个整数 n，返回 n 皇后不同的解决方案的数量。

// 示例:

// 输入: 4
// 输出: 2
// 解释: 4 皇后问题存在如下两个不同的解法。
// [
//  [".Q..",  // 解法 1
//   "...Q",
//   "Q...",
//   "..Q."],

//  ["..Q.",  // 解法 2
//   "Q...",
//   "...Q",
//   ".Q.."]
// ]

// LeetCode：52.N皇后II
// https://leetcode-cn.com/problems/n-queens-ii/submissions/

public class _52_totalNQueens {

    boolean[] cols;

    boolean[] left;

    boolean[] right;

    int ways;

    public int totalNQueens(int n){
        if ( n < 1) {
            return 0;
        }
        cols = new boolean[n];
        left = new boolean[( n << 1) -1];
        right = new boolean[left.length];
        place(0);
        return ways;
    }

    private void place(int row) {
        if ( row == cols.length) {
            ways++;
            return;
        }

        for (int col = 0; col < cols.length; col++) {
            if ( cols[col]) {
                continue;
            }
            int leftIndex = row - col + cols.length - 1;
            if ( left[leftIndex]) {
                continue;
            }
            int rightIndex = row +col;
            if ( right[rightIndex]) {
                continue;
            }
            
            cols[col] = true;
            left[leftIndex] = true;
            right[rightIndex] = true;
            place(row + 1);
            cols[col] = false;
            left[leftIndex] = false;
            right[rightIndex] = false;
        }

    }

    // public int totalNQueens(int n) {
    //     List<Integer> ans = new ArrayList<>();
    //     backtrack(new ArrayList<Integer>(), ans, n);
    //     return ans.size();
    // }
    
    // private void backtrack(List<Integer> currentQueen, List<Integer> ans, int n) {
    //     if (currentQueen.size() == n) {
    //         ans.add(1);
    //         return;
    //     }
    //     for (int col = 0; col < n; col++) {
    //         if (!currentQueen.contains(col)) {
    //             if (isDiagonalAttack(currentQueen, col)) {
    //                 continue;
    //             }
    //             currentQueen.add(col);
    //             backtrack(currentQueen, ans, n);
    //             currentQueen.remove(currentQueen.size() - 1);
    //         }
    
    //     }
    
    // }
    
    // private boolean isDiagonalAttack(List<Integer> currentQueen, int i) {
    //     int current_row = currentQueen.size();
    //     int current_col = i;
    //     for (int row = 0; row < currentQueen.size(); row++) {
    //         if (Math.abs(current_row - row) == Math.abs(current_col - currentQueen.get(row))) {
    //             return true;
    //         }
    //     }
    //     return false;
    // }
}