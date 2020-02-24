import java.util.List;
import java.util.ArrayList;

// 给定一个没有重复数字的序列，返回其所有可能的全排列。

// 示例:

// 输入: [1,2,3]
// 输出:
// [
//   [1,2,3],
//   [1,3,2],
//   [2,1,3],
//   [2,3,1],
//   [3,1,2],
//   [3,2,1]
// ]

// 46.全排列I
// https://leetcode-cn.com/problems/permutations/solution/hui-su-suan-fa-python-dai-ma-java-dai-ma-by-liweiw/

public class _46_permute {

    public List<List<Integer>> permute(int[] nums) {
        int len =  nums.length;
        List<List<Integer>> res = new ArrayList<>();
        if ( len == 0) {
            return res;
        }
        
        // 记录数组中每次选择的数字状态，true表示已经选过则不可以选择，false表示没有选过
        boolean[] used = new boolean[len];
        // path记录已经选过的数字，也即已经遍历的数字情况
        List<Integer> path = new ArrayList<>();

        // 每次都是从第一个数开始选起
        dfs(nums,len,0,path,used,res);
        return res;
    }

    private void dfs(int[] nums, int len, int depth, List<Integer> path, boolean[] used, List<List<Integer>> res) {
        if ( depth == len) {
            res.add(new ArrayList<>(path));
            return;
        }

        for (int i = 0; i < len; i++) {
            if ( !used[i]) {
                path.add(nums[i]);
                used[i] = true;
                dfs(nums, len, depth+1, path, used, res);
                used[i] = false;
                path.remove(depth);
            }
        }
    }
}