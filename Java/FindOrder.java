import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.Map;
import java.util.Queue;

/* 现在你总共有 n 门课需要选，记为 0 到 n-1。

在选修某些课程之前需要一些先修课程。 例如，想要学习课程 0 ，你需要先完成课程 1 ，我们用一个匹配来表示他们: [0,1]

给定课程总量以及它们的先决条件，返回你为了学完所有课程所安排的学习顺序。

可能会有多个正确的顺序，你只要返回一种就可以了。如果不可能完成所有课程，返回一个空数组。

示例 1:

输入: 2, [[1,0]] 
输出: [0,1]
解释: 总共有 2 门课程。要学习课程 1，你需要先完成课程 0。因此，正确的课程顺序为 [0,1] 。
示例 2:

输入: 4, [[1,0],[2,0],[3,1],[3,2]]
输出: [0,1,2,3] or [0,2,1,3]
解释: 总共有 4 门课程。要学习课程 3，你应该先完成课程 1 和课程 2。并且课程 1 和课程 2 都应该排在课程 0 之后。
     因此，一个正确的课程顺序是 [0,1,2,3] 。另一个正确的排序是 [0,2,1,3] 。
说明:

输入的先决条件是由边缘列表表示的图形，而不是邻接矩阵。详情请参见图的表示法。
你可以假定输入的先决条件中没有重复的边。
提示:

这个问题相当于查找一个循环是否存在于有向图中。如果存在循环，则不存在拓扑排序，因此不可能选取所有课程进行学习。
通过 DFS 进行拓扑排序 - 一个关于Coursera的精彩视频教程（21分钟），介绍拓扑排序的基本概念。
拓扑排序也可以通过 BFS 完成。
 */


// LeetCode：210.课程表II
// https://leetcode-cn.com/problems/course-schedule-ii/submissions/
// 解题思路：https://zhuanlan.zhihu.com/p/102051461
public class FindOrder {

    public static int[] findOrder(int numCourses,int[][] prerequisites) {
        if ( numCourses <= 0) {
            return new int[0];
        }
        Map<Integer,Integer> ins = new HashMap<>();
        Map<Integer,ArrayList<Integer>> outVertex = new HashMap<>();
        HashSet<Integer> set = new HashSet<>();
        int rows = prerequisites.length;
        
        for (int i = 0; i < rows; i++) {
            int key = prerequisites[i][0];
            int value = prerequisites[i][1];
            set.add(key);
            set.add(value);
            if ( !ins.containsKey(key)) {
                ins.put(key, 0);
            }
            if ( !ins.containsKey(value)) {
                ins.put(value, 0);
            }
            int inDegree = ins.get(key);
            ins.put(key, inDegree + 1);

            if ( !outVertex.containsKey(value)) {
                outVertex.put(value, new ArrayList<>());
            }
            ArrayList<Integer> list = outVertex.get(value);
            // Appends the specified element to the end of this list.
            list.add(key);
        }
        Queue<Integer> queue = new LinkedList<>();
        for (int vertex : set) {
            if ( ins.get(vertex) == 0) {
                queue.offer(vertex);
            }
        }
        int[] res = new int[numCourses];
        int count = 0;
        while ( !queue.isEmpty()) {
            int vertex = queue.poll();
            res[count] = vertex;
            count += 1;
            ArrayList<Integer> list = outVertex.getOrDefault(vertex, new ArrayList<>());
            for (int k : list) {
                int num = ins.get(k);
                if ( num == 1) {
                    queue.offer(num);
                }
                ins.put(k, num - 1);
            }
        }
        // for (int vertex : set) {
        //     if ( ins.get(vertex) != 0) {
        //         return new int[0];
        //     }
        // }
        int vertexsize = set.size();
        if ( vertexsize != res.length) {
            return new int[0];
        }
        HashSet<Integer> resSet = new HashSet<>();
	    for (int i = 0; i < count; i++) {
	        resSet.add(res[i]);
	    }
	    //有些课是独立存在的，这些课可以随时上，添加进来
	    for (int i = 0; i < numCourses; i++) {
	        if (!resSet.contains(i)) {
	            res[count++] = i;
	        }
	    }

        return res;
//     // 保存每个节点的先修课个数，也就是出度
//     HashMap<Integer, Integer> outNum = new HashMap<>();
//     // 保存以 key 为先修课的列表，也就是入度的节点
//     HashMap<Integer, ArrayList<Integer>> inNodes = new HashMap<>();
//     // 保存所有节点
//     HashSet<Integer> set = new HashSet<>();
//     int rows = prerequisites.length;
//     for (int i = 0; i < rows; i++) {
//         int key = prerequisites[i][0];
//         int value = prerequisites[i][1];
//         set.add(key);
//         set.add(value);
//         if (!outNum.containsKey(key)) {
//             outNum.put(key, 0);
//         }
//         if (!outNum.containsKey(value)) {
//             outNum.put(value, 0);
//         }
//         // 当前节点先修课个数加一
//         int num = outNum.get(key);
//         outNum.put(key, num + 1);

//         if (!inNodes.containsKey(value)) {
//             inNodes.put(value, new ArrayList<>());
//         }
//         // 更新以 value 为先修课的列表
//         ArrayList<Integer> list = inNodes.get(value);
//         list.add(key);
//     }

//     // 将当前先修课个数为 0 的课加入到队列中
//     Queue<Integer> queue = new LinkedList<>();
//     for (int k : set) {
//         if (outNum.get(k) == 0) {
//             queue.offer(k);
//         }
//     }
//     int[] res = new int[numCourses];
//     int count = 0;
//     while (!queue.isEmpty()) {
//         // 队列拿出来的课代表要删除的节点
//         // 要删除的节点的 list 中所有课的先修课个数减一
//         int v = queue.poll();
//         //**************主要修改的地方********************//
//         res[count++] = v;
//         //**********************************************//
//         ArrayList<Integer> list = inNodes.getOrDefault(v, new ArrayList<>());

//         for (int k : list) {
//             int num = outNum.get(k);
//             // 当前课的先修课要变成 0, 加入队列
//             if (num == 1) {
//                 queue.offer(k);
//             }
//             // 当前课的先修课个数减一
//             outNum.put(k, num - 1);
//         }
//     }
//     for (int k : set) {
//         if (outNum.get(k) != 0) {
//             //有课没有完成，返回空数组
//             return new int[0];
//         }
//     }
//     //**************主要修改的地方********************//
//     HashSet<Integer> resSet = new HashSet<>();
//     for (int i = 0; i < count; i++) {
//         resSet.add(res[i]);
//     }
//     //有些课是独立存在的，这些课可以随时上，添加进来
//     for (int i = 0; i < numCourses; i++) {
//         if (!resSet.contains(i)) {
//             res[count++] = i;
//         }
//     }
//     //**********************************************//
//     return res;
    }
}

// public class Solution {

//     public int[] findOrder(int numCourses, int[][] prerequisites) {
//         // 先处理极端情况
//         if (numCourses <= 0) {
//             return new int[0];
//         }
//         // 邻接表表示
//         HashSet<Integer>[] graph = new HashSet[numCourses];
//         for (int i = 0; i < numCourses; i++) {
//             graph[i] = new HashSet<>();
//         }
//         // 入度表
//         int[] inDegree = new int[numCourses];
//         // 遍历 prerequisites 的时候，把 邻接表 和 入度表 都填上
//         //遍历二维数组中每一个一维数组
//         for (int[] p : prerequisites) {
//             graph[p[1]].add(p[0]);
//             inDegree[p[0]]++;
//         }
//         LinkedList<Integer> queue = new LinkedList<>();
//         for (int i = 0; i < numCourses; i++) {
//             if (inDegree[i] == 0) {
//                 queue.addLast(i);
//             }
//         }
//         ArrayList<Integer> res = new ArrayList<>();
//         while (!queue.isEmpty()) {
//             // 当前入度为 0 的结点
//             Integer inDegreeNode = queue.removeFirst();
//             // 加入结果集中
//             res.add(inDegreeNode);
//             // 下面从图中删去
//             // 得到所有的后继课程，接下来把它们的入度全部减去 1
//             HashSet<Integer> nextCourses = graph[inDegreeNode];
//             for (Integer nextCourse : nextCourses) {
//                 inDegree[nextCourse]--;
//                 // 马上检测该结点的入度是否为 0，如果为 0，马上加入队列
//                 if (inDegree[nextCourse] == 0) {
//                     queue.addLast(nextCourse);
//                 }
//             }
//         }
//         // 如果结果集中的数量不等于结点的数量，就不能完成课程任务，这一点是拓扑排序的结论
//         int resLen = res.size();
//         if (resLen == numCourses) {
//             int[] ret = new int[numCourses];
//             for (int i = 0; i < numCourses; i++) {
//                 ret[i] = res.get(i);
//             }
//             return ret;
//         } else {
//             return new int[0];
//         }
//     }
// }