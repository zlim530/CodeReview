// 给定一个无序的整数数组，找到其中最长上升子序列的长度。

// 示例:

// 输入: [10,9,2,5,3,7,101,18]
// 输出: 4 
// 解释: 最长的上升子序列是 [2,3,7,101]，它的长度是 4。

// LeetCode：300.最长的上升子序列
// https://leetcode-cn.com/problems/longest-increasing-subsequence/submissions/

public class _300_lengthOfLIS {

    public int lengthOfLIS(int[] nums) {
        if ( nums == null || nums.length == 0) {
            return 0;
        }
        int len = 0;
        int[] top = new int[nums.length];
        for (int num : nums) {
            int begin = 0;
            int end = len;
            while ( begin < end) {
                int mid = (begin + end) >> 1;
                if (num <= top[mid]) {
					end = mid;
				} else {
					begin = mid + 1;
				}
            }
            top[begin] = num;
            if ( begin == len) {
                len++;
            }
        }
        return len;
    }

    // 动态规划
    // 时间复杂度：O(n^2)
    // 空间复杂度：O(n)
    public int lengthOfLIS1(int[] nums) {
        if ( nums == null || nums.length == 0) {
            return 0;
        }
        int[] dp = new int[nums.length];
        int max = dp[0] = 1;
        for (int i = 1; i < dp.length; i++) {
            dp[i] = 1;
            for (int j = 0; j < i; j++) {
                if ( nums[j] >= nums[i]) {
                    continue;
                }
                dp[i] = Math.max(dp[i], dp[j] + 1);
            }
            max = Math.max(dp[i], max);
        }
        return max;
    }
}