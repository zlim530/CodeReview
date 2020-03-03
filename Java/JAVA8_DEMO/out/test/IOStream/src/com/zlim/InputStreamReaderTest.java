package com.zlim;

import org.junit.Test;

import java.io.*;

/**
 * 处理流之二：转换流的作用
 * 1.转换流：属于字符流（看后缀Reader）:故操作的基本单位是char[]字符型数组
 *      InputStreamReader:将一个字节的输入流转化为字符的输入流
 *      OutputStreamWriter：将一个字符的输出流转换为字节的输出流
 * 2.作用：提供字节流与字符流之间的转换
 *
 * 3. 解码： 字节、字节数组 --- > 字符数组、字符串
 *    编码： 字符数组、字符串  ---> 字节、字节数组
 *
 * 4.字符集
 * 常见的编码表
 *  ASCII：美国标准信息交换码。
 *  用一个字节的7位可以表示。
 *  ISO8859-1：拉丁码表。欧洲码表
 *  用一个字节的8位表示。
 *  GB2312：中国的中文编码表。最多两个字节编码所有字符
 *  GBK：中国的中文编码表升级，融合了更多的中文文字符号。最多两个字节编码
 *  Unicode：国际标准码，融合了目前人类使用的所有字符。
 *      为每个字符分配唯一的字符码。所有的文字都用两个字节来表示。
 *      在内存层面表示没问题，但是存入文件时有问题：因为向下兼容ASCII码，而所有ASCII码只需要一个字节即可
 *      在Unicode中两个字节到底是表示两个ASCII码还是作为整体只表示一个字符有歧义：对于上面的其他三个编码其实也有这样的问题
 *      但是上面三个编码规定最高位为0则表示ASCII码，即只需要一个字节；而其他的则按照对应字符集的编码规则进行编码
 *  UTF-8：变长的编码方式，可用1-4个字节来表示一个字符。向下兼容ASCII码
 *       Unicode只是定义了一个庞大的、全球通用的字符集，并为每个字符规定了唯
 *       一确定的编号，具体存储成什么样的字节流，取决于字符编码方案。推荐的Unicode编码是UTF-8和UTF-16。
 *       Unicode字符集只是定义了字符的集合和唯一编号，Unicode编码，则是对UTF-8、
 *      UCS-2/UTF-16等具体编码方案的统称而已，并不是具体的编码方案。
 *
 *
 * @author zlim
 * @create 2020-03-03 20:03
 */
public class InputStreamReaderTest {

    

    @Test
    public  void  test1() throws Exception {

        // 用字节流去读取文本文件：当不设计到文件的控制台显示操作时可以使用字节流对象作为中间容器将文本
        // 进行复制操作，但是如果想要在控制台输出文件文件的内容使用字节流对象会导致乱码
        // 现在使用转换流将字节输入流对象转化为字符输入流对象并在控制台上显示：保证不乱码
        FileInputStream fis = new FileInputStream("hi1.txt");
        // InputStreamReader isr = new InputStreamReader(fis); // 使用系统默认的字符集
        // 参数2指明了字符集，取决于原文件保存时使用的字符集
        InputStreamReader isr = new InputStreamReader(fis,"UTF-8");

        char[] cbuff = new char[200];
        int len;
        while ( (len = isr.read(cbuff)) != -1){
            String  str = new String(cbuff,0,len);
            System.out.println(str);
        }

        isr.close();

    }

    // 综合测试InputStreamReader 和 OutputStreamWriter的使用
    // 总结： 字节输入流对象（文件） --通过InputStreamReader--> 字符流输入对象（文件）
    //            故InputStreamReader需要接收字节输入对象作为参数生成对应的InputStreamReader对象
    //            表示将输入字节流对象转换为字符输入流对象:也即解码过程
    //        字符输入对象（文件）  --通过OutputStreamWriter--> 字节流输出对象（文件）
    //            故OutputStreamWriter需要接受字节输出流作为参数生成对应的OutputStreamWriter对象
    //            表示通过读取字符输出流对象转换为指定的字节输出流对象中:也即编码过程
    @Test
    public void test2() throws Exception {
        // 字节输入流与字节输出流
        FileInputStream  fis = new FileInputStream("hi.txt");
        FileOutputStream fos = new FileOutputStream("hi_gbk.txt");

        InputStreamReader isr = new InputStreamReader(fis,"utf-8");
        OutputStreamWriter osw = new OutputStreamWriter(fos,"gbk");

        char[] cbuff = new char[20];
        int len;
        while ( (len = isr.read(cbuff)) != -1){
            osw.write(cbuff,0,len);
        }

        // 关闭资源
        isr.close();
        osw.close();

    }




}



























