package com.zlim;

/**
 * @author zlim
 * @create 2020-02-29 20:47
 */
public class BruteForce01 {

    public  static  int indexOf(String text,String pattern){
        if (text == null || pattern == null){
            return -1;
        }
        char[] textChars = text.toCharArray();
        int tlen = textChars.length;
        if (tlen == 0){
            return  -1;
        }
        char[] patternChars = pattern.toCharArray();
        int plen = patternChars.length;
        if (plen == 0) {
            return -1;
        }
        if ( tlen < plen){
            return -1;
        }
        int pi = 0, ti = 0;
        // tmax 是文本串正在匹配子串开始索引的临界值
        int tmax = tlen - plen;
        // ti - pi ：是文本串中正在匹配子串的开始索引：是指每一轮比较中 text 首个比较字符的位置
        while ( pi < plen && ti - pi <= tmax){
            if (textChars[ti] == patternChars[pi]){
                ti++;
                pi++;
            }else {
                ti -= (pi - 1);
                pi = 0;
            }
        }
        return  (pi == plen) ? (ti - pi) : -1;

    }


    public  static  int indexOf2(String text,String pattern){
        if (text == null || pattern == null){
            return -1;
        }
        char[] textChars = text.toCharArray();
        int tlen = textChars.length;
        if (tlen == 0){
            return  -1;
        }
        char[] patternChars = pattern.toCharArray();
        int plen = patternChars.length;
        if (plen == 0) {
            return -1;
        }
        if ( tlen < plen){
            return -1;
        }
        int pi = 0, ti = 0;
        while ( pi < plen && ti < tlen){
            if (textChars[ti] == patternChars[pi]){
                ti++;
                pi++;
            }else {
                ti -= (pi - 1);
                pi = 0;
            }
        }
        return  (pi == plen) ? (ti - pi) : -1;

    }

}
