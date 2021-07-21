package com.zlim;

public class Main {

    public static void main(String[] args) {
        System.out.println("Hello World!");
        System.out.println(lcs("ABCDEFG","ABCDEEG"));
    }

    private static int lcs(String s1, String s2) {
        int[] pre = new int[s1.length()];
        int[] cur = new int[s1.length()];

        for (int i = 0; i < s2.length(); i++) {
            for (int j = 0; j < s1.length(); j++) {
                if (s2.charAt(i) == s1.charAt(j)){
                    cur[j] = j == 0 ? 1 : pre[j - 1] +1;
                } else {
                    cur[j] = Math.max(pre[j],j == 0 ? 0 : cur[j - 1]);
                }
            }
            pre = cur;
            cur = new int[s1.length()];
        }
        return pre[pre.length - 1];
    }

}
