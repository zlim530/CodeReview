namespace YSR.MES.Common.CommonModel
{
    /// <summary>
    /// 分页数据基类
    /// </summary>
    public abstract class InputPageBase
    {
        /// <summary>
        /// 当前页数
        /// </summary>
        public virtual int Page { get; set; }

        /// <summary>
        /// 取多少条数据
        /// </summary>
        public virtual int Limit { get; set; }

        /// <summary>
        /// 跳转页
        /// </summary>
        public virtual int? JumpPage { get; set; }
    }
}
