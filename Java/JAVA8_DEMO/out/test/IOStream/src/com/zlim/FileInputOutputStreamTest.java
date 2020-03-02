package com.zlim;

import org.junit.Test;

import java.io.*;

/**
 *
 * 测试FileInputStream和FileOutputStream的使用：
 *  对于文本文件(.txt,.java,.c,.cpp..)，使用字符流处理：即FileReader/Writer类
 *  对于非文本文件(.jpg,.mp3,.mp4,.avi,.doc,.ppt,...)，使用字节流处理：即FileInputStream和FileOutputStream类
 *      对于文件的复制等操作，文本文件也可以用FileInputStream和FileOutputStream类进行复制，只不是如果想用
 *      FileInputStream和FileOutputStream在控制台对文本文件进行访问打印操作可能会导致乱码
 *      而对于非文本文件的操作，只能用字节流处理而不能字符流处理，不过是复制还是访问读取操作
 *
 * @author zlim
 * @create 2020-03-02 23:45
 */
public class FileInputOutputStreamTest {

    // 实现对图片的复制
    @Test
    public void testFileInputOutputStream()  {
        FileInputStream fis = null;
        FileOutputStream fos = null;
        try {
            File srcFile = new File("C:\\Users\\Lim\\Desktop\\code\\人脸库\\m.jpg");
            File destFile  = new File("qq.jpg");

            fis = new FileInputStream(srcFile);
            fos = new FileOutputStream(destFile);

            byte[] buffer = new byte[5];
            int len ;
            while ( ( len = fis.read(buffer)) != -1){
                fos.write(buffer,0,len);
            }
            System.out.println("复制成功。");
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            if (fis != null) {
                try {
                    fis.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }

            if (fos != null) {
                try {
                    fos.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }

    }





}




