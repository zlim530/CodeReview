package com.zlim;

import java.io.Serializable;

/**
 * @author zlim
 * @create 2020-03-09 0:01
 */
public class Creature<T> implements Serializable {

    private char gender;
    public double weight;

    public void  breath(){
        System.out.println("Creature breath.");
    }

    public void eat(){
        System.out.println("Creature eating.");
    }

}
