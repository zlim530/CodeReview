package com.zlim;

import java.util.Arrays;

/**
 * @author zlim
 * @create 2020-03-06 15:10
 */
public class TestProgram {

    public static void main(String[] args) {
        String  s = "i love you";
        String[] arr = s.split("\\s");
        int len = arr.length;
        int mid = (arr.length) >> 1;
        String[] arrleft = new String[mid];
        for (int i = 0; i < mid; i++) {
            arrleft[i] = arr[i];
        }
        for (int i = 0; i < len; i++) {
            if( mid % 2 == 0){
                arr[i] = arr[len - 1 - i];
            }else{
                if( i != mid){
                    arr[i] = arr[len - 1 - i];
                }
            }
        }
        System.arraycopy(arr,0,arrleft,0,mid);

    }


}
