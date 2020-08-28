using LearningAbpDemo.Common.EnumCommon;
using System;

namespace LearningAbpDemo.Movie
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// 映射的数据表名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 过滤的接口名称
        /// </summary>
        public string FilterName { get; private set; } = string.Empty;

        /// <summary>
        /// 数据源枚举
        /// </summary>
        public ContextTypeEnum ContextType { get; private set; }


        public TableNameAttribute(string name,string tableName,string filtername,ContextTypeEnum contextType)
        {
            DisplayName = name;
            Name = tableName;
            FilterName = filtername;
            ContextType = contextType;
        }

        public TableNameAttribute(string name, string tableName,  ContextTypeEnum contextType)
        {
            DisplayName = name;
            Name = tableName;
            ContextType = contextType;
        }

        public TableNameAttribute(string name,string tableName)
        {
            DisplayName = name;
            Name = tableName;
        }
    }
}