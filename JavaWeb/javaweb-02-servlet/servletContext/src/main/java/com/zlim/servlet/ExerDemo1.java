package com.zlim.servlet;

import java.util.Scanner;

/**
 * @author zlim
 * @create 2020-03-26 22:11
 */
public class ExerDemo1 {
    public static void main1(String[] args) {
        int n = 1;
        int num = 1;
        int maxr = 1;
        Scanner in = new Scanner(System.in);
        n = in.nextInt();
        int[] arr = new  int[n];
        int[] arr2 = new int[n];
        for (int i = 0; i < n; i++) {
            arr[i] = in.nextInt();
        }

        for (int i = 0; i < n; i++) {
            arr2[i] = arr[i];
            for (int j = 0; j < n; j++) {
                arr2[j] = arr[j];
                if(  arr2[i] + 1 == arr2[j]){
                    num = num + 1;
                    arr2[i] = arr2[j];
                }
            }
            if( num > maxr){
                maxr = num;
            }
            num = 1;
        }
        System.out.println(n - maxr );
    }


    public static void main(String[] args) {
        int[] arr = new int[201];
        int[] arr2 = new int[201];
        int n = 0;
        int cnt = 0;
        int k = 0;
        Scanner in = new Scanner(System.in);
        n = in.nextInt();

        for (int i = 0; i < n; i++) {
            arr[i] = in.nextInt();
        }

        for (int i = 0; i < n - 1; i++) {
            if( arr[i] >= arr[i + 1]){
                arr2[cnt++] = i + 1;
                for (;i < n - 1;i ++){
                    if( arr[i] <= arr[i + 1]){
                        break;
                    }
                }
            }
        }
        if( arr[n - 2] <= arr[n - 1]){
            arr2[cnt++] = n;
        }
        for (int i = 0; i < cnt; i++) {
            System.out.println(arr2[i]);
        }

    }



    public class TreeNdoe {

        int val;

        TreeNdoe left;

        TreeNdoe right;

        public TreeNdoe(int x) {
            val = x;
        }
    }


    public TreeNdoe buildTree106(int[] inorder, int[] postorder) {
        return buildTree106Helper(postorder, 0, postorder.length, inorder, 0, inorder.length);
    }

    private TreeNdoe buildTree106Helper(int[] postorder, int p_start, int p_size, int[] inorder, int i_start,
                                        int i_size) {
        if (p_start == p_size || i_start == i_size) {
            return null;
        }
        int root_val = postorder[p_size - 1];
        TreeNdoe root = new TreeNdoe(root_val);
        int i_root_index = 0;
        for (int i = i_start; i < i_size; i++) {
            if (inorder[i] == root_val) {
                i_root_index = i;
                break;
            }
        }
        int leftSize = i_root_index - i_start;
        int postStLeft = p_start + leftSize - i_start;
        root.left = buildTree106Helper(postorder, p_start, postStLeft, inorder, i_start, i_root_index);
        root.right = buildTree106Helper(postorder, postStLeft, p_size - 1, inorder, i_root_index + 1, i_size);
        return root;
    }

}
