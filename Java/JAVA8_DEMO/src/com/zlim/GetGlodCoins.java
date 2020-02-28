package com.zlim;

import java.math.BigDecimal;
import java.util.Scanner;

/**
 * @author zlim
 * @create 2020-02-28 18:41
 */
public class GetGlodCoins {
    public static void main(String[] args) {
        // System.out.println(getGlodCoins(567));
        Scanner sc = new Scanner(System.in);
        int n = sc.nextInt();
        int res = 0;
        if ( n < 5){
            System.out.println(1);
        } else if ( 5 <= n && n < 10){
            System.out.println(2);
        } else {
            res = (n - 10) + n / 5;
            System.out.println(res);
        }

    }


    public static BigDecimal getGlodCoins(int n){
        BigDecimal coins = new BigDecimal("1000");
        BigDecimal goldCoins = new BigDecimal("10");
        BigDecimal p1 = new BigDecimal("0.010000");
        BigDecimal p2 = new BigDecimal("1.000000");
        String s = Integer.toString(n);
        BigDecimal sn = new BigDecimal(s);
        BigDecimal res = null;
        if ( n <= 10) {
            res = p1.multiply(sn);
            return res;
        } else if ( n >= 991){
            return  p2;
        } else {
            return  p1.multiply(new BigDecimal("10"));
        }
    }

    public  static  int getRabbits(int n){
        if ( n < 5){
            return 1;
        } else if ( n == 5){
            return 2;
        }

        return 0;
    }

}
