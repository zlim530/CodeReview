using UserMgr.Domain.ValueObjects;

namespace UserMgr.Domain.Entities
{
    /// <summary>
    /// 用户登录记录聚合根：因为有可能存在需要脱离某一个 User 而直接查询某一段时间内人员登录记录的需要，所以将这个类型单独聚合
    /// 又因为这是另外一个独立的聚合根对象，所以不能在其内部直接引用其他聚合根对象，而是要引用其他聚合根对象的唯一标识符：这也是高扩展性的体现，如果以后这个聚合根需要拆分为单独微服务也很方便
    /// </summary>
    public record UserLoginHistory : IAggregateRoot
    {

        public long Id { get; init; }

        /// <summary>
        /// 用户 Id：一个指向 User 实体对象的外键，但是在物理上，我们并没有实际创建它们之间的外键关系
        /// 由于有可能传入一个不存在的 User，所以允许 UserId 为空
        /// </summary>
        public Guid? UserId { get; init; }

        public PhoneNumber PhoneNumber { get; init; }

        /// <summary>
        /// 创建时间（第一次创建时间）
        /// </summary>
        public DateTime CreatedDateTime { get; init; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; init; }

        private UserLoginHistory()
        {
            
        }

        public UserLoginHistory(Guid? userId, PhoneNumber phoneNumber, string message)
        {
            UserId = userId;
            PhoneNumber = phoneNumber;
            CreatedDateTime = DateTime.Now;
            Message = message;
        }
    }
}
