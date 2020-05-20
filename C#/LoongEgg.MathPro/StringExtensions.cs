using System;
using System.Collections.Generic;
using System.Text;

/**
 * @author zlim
 * @create 2020/5/20 15:33:24
 */
namespace LoongEgg.MathPro {
    public static class StringExtensions {

        /// <summary>
        /// 剔除字符串中的空格
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string RemoveSpace(this string self) => self.Replace(" ",string.Empty);
    
    }
}
