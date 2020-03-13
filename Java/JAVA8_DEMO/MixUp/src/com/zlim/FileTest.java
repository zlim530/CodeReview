package com.zlim;

import java.io.*;
import java.util.ArrayList;
import java.util.List;

/**
 * @author zlim
 * @create 2020-03-11 0:58
 */
public class FileTest {

    public static void main(String[] args) {
        Thread t = new Thread(){
            @Override
            public void run() {
                pong();
            }
        };
        t.run();
        System.out.println("ping");
        // pong
        // ping
    }

    static void pong() {
        System.out.println("pong");
    }





    public static void main1(String[] args) {
        File dir = new File("C:\\Users\\Lim\\Desktop\\Linux\\B-其他资料\\Java\\宋红康-Java-尚硅谷\\尚硅谷_宋红康_第13章_IO流");
        FilenameFilter filter = new FilenameFilter() {
            @Override
            public boolean accept(File dir, String name) {
                return name.endsWith(".pdf");
            }
        };
        List<File> list = new ArrayList<>();
        List<File> files = getFiles(dir, filter, list);
        // System.out.println("files = " + files);
        for (File file : list) {
            System.out.println("file = " + file);
        }
        System.out.println();
        System.out.println("********************************");
        System.out.println();
        FileTest.list(dir);
    }


    public static List<File> getFiles(File dir, FilenameFilter filter, List<File> list){
        File[] files = dir.listFiles();
        for (File file : files) {
            if( file.isDirectory()){
                getFiles(file,filter,list);
            }else{
                if( filter.accept(dir,file.getName())){
                    list.add(file);
                }
            }
        }
        return list;
    }





    //列出某文件夹及其子文件夹下面的文件，并可根据扩展名过滤
    public static void list(File path){
        if(!path.exists()){
            System.out.println("文件不存在");
        }else{
            if(path.isFile()){
                if(path.getName().toLowerCase().endsWith(".pdf")
                        /*||path.getName().toLowerCase().endsWith(".doc")
                        ||path.getName().toLowerCase().endsWith(".chm")
                        ||path.getName().toLowerCase().endsWith(".html")
                        ||path.getName().toLowerCase().endsWith(".htm")*/){
                    // System.out.println(path);
                    System.out.println(path.getName());
                }
            }else{
                File[] files=path.listFiles();
                for(int i=0;i<files.length;i++){
                    list(files[i]);
                }
            }
        }
    }




}
