/* 
子序列：是可以不连续的
但是子串、子数组、子区间必须是连续的

给定一个整数数组 nums ，找到一个具有最大和的连续子数组（子数组最少包含一个元素），返回其最大和。
这道题也属于最大切片问题（最大区段，Greatest Slice）

示例:

输入: [-2,1,-3,4,-1,2,1,-5,4],
输出: 6
解释: 连续子数组 [4,-1,2,1] 的和最大，为 6。

进阶:
如果你已经实现复杂度为 O(n) 的解法，尝试使用更为精妙的分治法求解。

注意：这里所说的连续子序列中的连续是指子序列的位置是连续的，是相连的，而不是指数值是连续的
*/

// LeetCode：53.最大子序列和
// https://leetcode-cn.com/problems/maximum-subarray/submissions/

public class _53_maxSubArray {

    public int maxSubArray5(int[] nums){
        if ( nums == null || nums.length == 0){
            return -1;
        }
        int dp = nums[0];
        int max = dp;
        for (int i = 1; i < nums.length; i++) {
            if ( dp < 0) {
                dp = nums[i];
            }else{
                dp = nums[i] + dp;
            }
            max = Math.max(max, dp);
        }

        return max;
    }

    // 动态规划：
    // 状态定义:dp[i]:以nums[i]作为结尾的最大连续子序列的和(nums是整个序列)
    // 状态转移方程:dp[i] = nums[i] + Math.max(dp[i - 1],0);
    // 状态初始状态：
    public int maxSubArray4(int[] nums){
        if ( nums == null || nums.length == 0){
            return -1;
        }
        int[] dp = new int[nums.length];
        int max = dp[0] = nums[0];
        for (int i = 1; i < nums.length; i++) {
            int prev = dp[i - 1];
            if ( prev < 0) {
                dp[i] = nums[i];
            } else{
                dp[i] = prev + nums[i];
            }
            max = Math.max(max, dp[i]);
        }

        return max;
    }

    public int maxSubArray3(int[] nums){
        if ( nums == null || nums.length == 0) {
            return 0;
        }
        return maxSubArray(nums,0,nums.length);
    }

    private int maxSubArray(int[] nums, int begin, int end) {
        if ( end - begin < 2 ) {
            return nums[begin];
        }
        // 将序列均匀地分割成 2 个子序列
        // 假设 [begin , end) 的最大连续子序列和是 S[i , j) ，那么它有 3 种可能
        // 1.[i , j) 存在于 [begin , mid) 中，同时 S[i , j) 也是 [begin , mid) 的最大连续子序列和
        // 2.[i , j) 存在于 [mid , end) 中，同时 S[i , j) 也是 [mid , end) 的最大连续子序列和
        // 3.[i , j) 一部分存在于 [begin , mid) 中，另一部分存在于 [mid , end) 中
        // ✓ [i , j) = [i , mid) + [mid , j)
        // ✓ S[i , mid) = max { S[k , mid) }，begin ≤ k ＜ mid
        // ✓ S[mid , j) = max { S[mid , k) }，mid ＜ k ≤ end
        int mid = (begin + end) >> 1;

        // int leftMax = Integer.MIN_VALUE;
        // int leftSum = 0;
        int leftMax = nums[mid - 1];
        int leftSum = leftMax;
        for (int i = mid - 2; i >=0; i--) {
            leftSum += nums[i];
            leftMax = Math.max(leftMax, leftSum);
        }

        // int rightMax = Integer.MIN_VALUE;
        // int rightSum = 0;
        int rightMax = nums[mid];
        int rightSum = rightMax;
        for (int i = mid + 1; i < end; i++) {
            rightSum += nums[i];
            rightMax = Math.max(rightMax, rightSum);
        }

        return Math.max(leftMax + rightMax,
                        Math.max(
                        maxSubArray(nums, begin, mid),
                        maxSubArray(nums, mid, end))
        );
    }

    public int maxSubArray2(int[] nums) {
        if ( nums == null || nums.length == 0) {
            return 0;
        }
        int max = Integer.MIN_VALUE;
        for (int i = 0; i < nums.length; i++) {
            int sum = 0;
            for (int j = i; j < nums.length; j++) {
                sum += nums[j];
                max = Math.max(max, sum);
            }
        }

        return max;
    }

    // 解法1：暴力出奇迹：
    // 穷举出所有可能的连续子序列，并计算出它们的和，最后取它们中的最大值
    public int maxSubArray1(int[] nums) {
        if ( nums == null || nums.length == 0) {
            return 0;
        }
        int max = Integer.MIN_VALUE;
        // 空间复杂度：O(1)，时间复杂度：O(n^3)
        for (int begin = 0; begin < nums.length; begin++) {
            for (int end = begin; end < nums.length; end++) {
                // sum是子序列[begin,end]的和
                int sum = 0;
                // [begin,end]:子序列范围
                for (int i = begin; i <= end; i++) {
                    sum += nums[i];
                }
                max = Math.max(sum, max);
            }
        }

        return max;
    }



}