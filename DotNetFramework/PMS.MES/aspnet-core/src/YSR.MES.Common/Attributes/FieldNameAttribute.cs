using System;

namespace YSR.MES.Common.Attributes
{
    /// <summary>
    /// 字段特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldNameAttribute : Attribute
    {
        public FieldNameAttribute(string displayName, string name)
        {
            DisplayName = displayName;
            Name = name;
        }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }
    }
}
