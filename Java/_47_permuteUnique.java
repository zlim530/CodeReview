import java.lang.reflect.Array;
import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Deque;
import java.util.List;

// 给定一个可包含重复数字的序列，返回所有不重复的全排列。

// 示例:

// 输入: [1,1,2]
// 输出:
// [
//   [1,1,2],
//   [1,2,1],
//   [2,1,1]
// ]

// LeetCode：47.全排列2
// https://leetcode-cn.com/problems/permutations-ii/solution/hui-su-suan-fa-python-dai-ma-java-dai-ma-by-liwe-2/

public class _47_permuteUnique {

    public List<List<Integer>> permuteUnique(int[] nums) {
        int len = nums.length;
        List<List<Integer>> res = new ArrayList<>();
        if ( len == 0) {
            return res;
        }

        Arrays.sort(nums);

        boolean[] used = new boolean[len];
        List<Integer> path = new ArrayList<>();

        dfs(nums,len,0,path,used,res);
        return res;
    }

    private void dfs(int[] nums, int len, int depth, List<Integer> path, boolean[] used, List<List<Integer>> res) {
        if ( depth == len) {
            res.add(new ArrayList<>(path));
            return ;
        }
        for (int i = 0; i < len; i++) {
            if ( used[i]) {
                continue;
            }
            // 剪枝条件
            if ( i > 0 && nums[i] == nums[i - 1] && used[i - 1] == false) {
                continue;
            }
            path.add(nums[i]);
            used[i] = true;

            dfs(nums, len, depth+1, path, used, res);

            path.remove(depth);
            used[i] = false;
        }
    }
}