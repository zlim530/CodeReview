namespace EFCoreConsoleDemo
{
    public class Comment
    {
        public long Id { get; set; }

        /// <summary>
        /// 一对多关系配置
        /// </summary>
        public Article Article { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// 设置额外的外键字段：对迁移没有影响
        /// </summary>
        public long ArticleId { get; set; }
    }
}