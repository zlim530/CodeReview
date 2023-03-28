namespace UserMgr.WebAPI.UnitOfWorks
{
    /// <summary>
    /// EFCore 天然实现了工作单元由于在应用服务层中的 Controller API 接口是完整的用户故事，因此我们在应用服务层中
    /// 调用 SaveChangeAsync() 方法但是在每一个方法后面写 SaveChangeAsync()
    /// 很麻烦，因此我们使用 Filter 来实现当 Action 方法执行结束后自动调用 SaveChangeAsync 方法的功能
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)] // 只能用于方法特性使用
    public class UnitOfWorkAttribute : Attribute
    {
        /// <summary>
        /// 获取需要执行 SaveChangeAsync 方法的 DBContext 对象们
        /// </summary>
        public Type[] DbContextTypes { get; init; }

        public UnitOfWorkAttribute(params Type[] dbContextTypes)
        {
            DbContextTypes = dbContextTypes;
        }
    }
}
