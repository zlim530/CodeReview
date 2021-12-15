using System.Collections.Generic;

namespace YSR.MES.Common.CommonModel.LinqCommonModel
{
    public interface IChildThis<TEntity>
    {
        IEnumerable<TEntity> Children { get; set; }
    }
}
