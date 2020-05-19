using SpeakVoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;

namespace OrderSystem.Common
{
   public class SpeakDinner
    {

       public static bool isInitializeSpeechToText = false;
      public  static string[] set = { "订餐", "下菜", "提交", "确认", "结账", "取消", "搜索", "玫瑰花茶", "客家酿豆腐", "川菜回锅肉", "红烧牛肉", "宫保鸡丁", "凉拌广东菜心", "客家酿豆腐", "鲜虾肠粉", "玫瑰花茶", "川菜回锅肉", "水煮鱼片" };
       
       // 调用语音订餐的初始化位置
       public static void TakeToSepak(string [] sets)
       {
        // string[]  set = { "订餐", "下菜", "提交", "确认", "结账", "取消", "搜索", "玫瑰花茶", "客家酿豆腐", "川菜回锅肉", "红烧牛肉", "宫保鸡丁", "凉拌广东菜心", "客家酿豆腐", "鲜虾肠粉", "玫瑰花茶", "川菜回锅肉", "水煮鱼片" };
           if (!isInitializeSpeechToText)
           {
               SpRecognition.Initialize(
                   sets,
                   RecognizeMode.Multiple);

               SpRecognition.GetTextFromSpeech += new EventHandler(GetTextFromSpeech);
               isInitializeSpeechToText = true;

           }
           SpRecognition.Start();

       }

       private static void GetTextFromSpeech
       (object sender, EventArgs e)
       {

           Order order = new Order();
           var txt = sender as string;
           switch (txt)
           {

               case "订餐":
                   order.Show();
                   break;
               case "下菜":

                   break;
               case "玫瑰花茶":

                   break;
               case "客家酿豆腐":

                   break;
               case "川菜回锅肉":

                   break;
               case "红烧牛肉":

                   break;
               case "宫保鸡丁":

                   break;
               case "凉拌广东菜心":

                   break;
               case "鲜虾肠粉":

                   break;
               case "水煮鱼片":

                   break;
            
               case "提交":
                     Check  pass1 = new Check();
                     pass1.Show();
                 //  order.button3_Click(null, null);
                   break;
           }

       }

    }
}
