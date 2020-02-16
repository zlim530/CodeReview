import java.util.HashMap;
import java.util.Map;

// 给定一个未排序的整数数组，找出最长连续序列的长度。

// 要求算法的时间复杂度为 O(n)。

// 示例:

// 输入: [100, 4, 200, 1, 3, 2]
// 输出: 4
// 解释: 最长连续序列是 [1, 2, 3, 4]。它的长度为 4。

public class _128_longestConsecutive {

    public static int longestConsecutive(int[] nums) {
        if (nums == null) {
            return 0;
        }
        if (nums.length <= 1) {
            return nums.length;
        }
        int min = nums[0];
        for (int i = 0; i < nums.length; i++) {
            if ( nums[i] < min) {
                min = nums[i];
            }
        }
        Map<Integer, Integer> uf = new HashMap<>();
        for (int i : nums) {
            if (!uf.containsKey(i)) {
                uf.put(i, i);
                if (uf.containsKey(i - 1)) {
                    uf.put(i - 1, i);
                }
                if (uf.containsKey(i + 1)) {
                    uf.put(i, i + 1);
                }
            }
        }
        if ( uf.get(min) == min ) {
            return 1;
        }

        int count = findParent(uf, min);
        // for (int i : nums) {
        //     if ( uf.get(i) != i) {
        //         // int pNode = findParent(uf,i) - i + 1;
        //         // count = count > pNode ? count : pNode;
        //         findParent(uf, i);
        //     }
        // }

        return count;
    }

    private static int findParent(Map<Integer, Integer> uf, int i) {
        // if ( uf.get(i) == i) {
        //     return i;
        // }
        // return findParent(uf, uf.get(i));
        int count = 1;
        while ( uf.get(i) != i) {
            i = uf.get(i);
            // uf.get(i);
            count++;
        }
        return count;
    }

    
}