namespace YSR.MES.Common.CommonModel.LinqCommonModel
{
    public interface IParentId<TKeyType>
    {
        TKeyType ParentId { get; set; }
    }
}
