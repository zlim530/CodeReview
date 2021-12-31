using YSR.MES.Common.Attributes;

namespace YSR.MES.Common.CommonModel.ExtendInterface
{
    public interface IDeptNo
    {
        /// <summary>
        /// 组织机构编码
        /// </summary>
        [FieldName("组织机构编码", nameof(NewCode))]
        string NewCode
        {
            get;
            set;
        }
    }
}
