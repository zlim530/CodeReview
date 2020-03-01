package com.zlim;

import org.junit.Test;

/**
 * @author zlim
 * @create 2020-03-01 17:05
 */
public class IDEADebug {

    @Test
    public void testStringBuffer(){
        String str = null;
        StringBuffer sb = new StringBuffer();
        sb.append(str);

        System.out.println(sb.length());    // 4
        System.out.println(sb);         // null

        StringBuffer sb1 = new StringBuffer(str);  // java.lang.NullPointerException
        System.out.println(sb1);
    }

}
