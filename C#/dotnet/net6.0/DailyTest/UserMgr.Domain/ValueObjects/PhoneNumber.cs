namespace UserMgr.Domain.ValueObjects
{
    /// <summary>
    /// 手机号值对象（与聚合根的概念相比）
    /// </summary>
    /// <param name="RegionNumber">区域（国家）区号</param>
    /// <param name="Number">手机号</param>
    public record PhoneNumber(int RegionNumber, string Number);
    
}
