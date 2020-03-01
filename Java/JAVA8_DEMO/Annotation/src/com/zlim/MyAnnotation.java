package com.zlim;

/**
 * @author zlim
 * @create 2020-03-01 21:34
 */
public @interface MyAnnotation {
    String value() default "hi";
}
