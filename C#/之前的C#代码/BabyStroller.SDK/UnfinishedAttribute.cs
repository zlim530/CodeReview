using System;
using System.Collections.Generic;
using System.Text;

namespace BabyStroller.SDK
{
    // 在主体程序的SDK中再添加一个类给那些还没有完全实现SDK中接口功能的类
    // C#中要求所有的Attribute都要继承至Attribute
    public class UnfinishedAttribute:Attribute
    {

    }
}
