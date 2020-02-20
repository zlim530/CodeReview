/* 给定不同面额的硬币 coins 和一个总金额 amount。编写一个函数来计算可以凑成总金额所需的最少的硬币个数。如果没有任何一种硬币组合能组成总金额，返回 -1。

示例 1:

输入: coins = [1, 2, 5], amount = 11
输出: 3 
解释: 11 = 5 + 5 + 1
示例 2:

输入: coins = [2], amount = 3
输出: -1
说明:
你可以认为每种硬币的数量是无限的。
 */

// LeetCode：322.零钱兑换
// https://leetcode-cn.com/problems/coin-change/
public class _322_coinChange {

    public int coinChange1(int[] coins, int amount) {
        if ( amount < 1 || coins == null || coins.length == 0) {
            return -1;
        }
        int[] dp = new int[amount + 1];
        for (int i = 1; i < dp.length; i++) {
            int min = Integer.MAX_VALUE;
            for (int coin : coins) {
                if ( i < coin) {
                    continue;
                }
                int v = dp[i - coin];
                if ( v < 0 || v >= min) {
                    continue;
                }
                // min = Math.min(dp[i - coin], min);
                min = v;
            }
            // 来到这里时，如果min == Integer.MAX_VALUE，则表示兑换的零钱比所有存在的零钱都要小
            // 也faces = [2,3,5] 而amount = 1：这种情况，则无法兑换零钱
            if( min == Integer.MAX_VALUE){
                dp[i] = -1;
            } else{
                dp[i] = min + 1;
            }
        }
        return dp[amount];
    }
}