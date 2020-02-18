import java.util.ArrayList;
import java.util.List;

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

public class _51_solveNQueens {

    public int totalNQueens(int n) {
        List<Integer> ans = new ArrayList<>();
        backtrack(new ArrayList<Integer>(), ans, n);
        return ans.size();
    }
    
    private void backtrack(List<Integer> currentQueen, List<Integer> ans, int n) {
        if (currentQueen.size() == n) {
            ans.add(1);
            return;
        }
        for (int col = 0; col < n; col++) {
            if (!currentQueen.contains(col)) {
                if (isDiagonalAttack(currentQueen, col)) {
                    continue;
                }
                currentQueen.add(col);
                backtrack(currentQueen, ans, n);
                currentQueen.remove(currentQueen.size() - 1);
            }
    
        }
    
    }
    
    private boolean isDiagonalAttack(List<Integer> currentQueen, int i) {
        int current_row = currentQueen.size();
        int current_col = i;
        for (int row = 0; row < currentQueen.size(); row++) {
            if (Math.abs(current_row - row) == Math.abs(current_col - currentQueen.get(row))) {
                return true;
            }
        }
        return false;
    }

    

    static int[] cols;

    // static int ways;

    public static void solveNQueens(int n) {
        if ( n < 1) {
            return;
        }
        cols = new int[n];
        place(0);
    }

    private static void place(int row) {
        if ( row == cols.length) {
            // ways++;
            show();
            return;
        }

        for (int col = 0; col < cols.length; col++) {
            if ( isValid(row,col)) {
                cols[row] = col;
                place(col + 1);
            }
        }
    }

    private static void show() {
        for (int i = 0; i < cols.length; i++) {
            for (int j = 0; j < cols.length; j++) {
                if ( cols[i] == j) {
                    System.out.println("Q");
                }else{
                    System.out.println(".");
                }
            }
        }
    }

    private static boolean isValid(int row, int col) {
        for (int i = 0; i < row; i++) {
            if ( cols[i] == col) {
                return false;
            }
            if ( row - i == Math.abs(col - cols[i])) {
                return false;
            }
        }
        return false;
    }
}