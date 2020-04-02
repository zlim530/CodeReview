package com.zlim.test;

/**
 * @author zlim
 * @create 2020-04-02 17:17
 */
public class Queen {

    boolean[] cols;

    boolean[] leftTop;

    boolean[] rightTop;

    public void place(int row){
        if( row == cols.length){
            return;
        }
        for (int col = 0; col < cols.length; col++) {
            if( cols[col]){
                continue;
            }
            int leftTopIndex = row - col + cols.length - 1;
            if( leftTop[leftTopIndex]){
                continue;
            }
            int rightTopIndex = row + col;
            if( rightTop[rightTopIndex]){
                continue;
            }
            cols[col] = leftTop[leftTopIndex] = rightTop[rightTopIndex] = true;
            place(row + 1);
            cols[col] = leftTop[leftTopIndex] = rightTop[rightTopIndex] = false;
        }
    }
}
