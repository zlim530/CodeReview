using System.Collections.Generic;

namespace YSR.MES.Common.CommonModel
{
    /// <summary>
    /// 分页返回的数据模版
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class OutputPageInfo<TEntity>
    {
        public OutputPageInfo(int count, IReadOnlyList<TEntity> items)
        {
            Msg = "";
            Data = items;
            Count = count;
        }

        /// <summary>
        /// 状态码 0 正常
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回的数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public IReadOnlyList<TEntity> Data { get; set; }
    }
}
