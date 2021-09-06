namespace WebApiOfBoolManage.Models
{
    /// <summary>
    /// App信息
    /// </summary>
    public class App
    {
        /// <summary>
        /// App的ID号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// App的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// App的说明
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 返回处理结果
    /// </summary>
    public class ResultJson
    {
        /// <summary>
        /// 返回代码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
    }


}