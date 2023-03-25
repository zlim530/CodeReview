namespace UserMgr.Domain.Entities
{
    public record UserAccessFail
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// 用户信息-表示对应哪个用户：因为 UserAccessFail 和 User 属于同一个聚合，因此可以直接引用 User 这个聚合根对象
        /// </summary>
        public User User { get; init; }

        /// <summary>
        /// 用户 Id - 外键
        /// </summary>
        public Guid UserId { get; init; }

        /// <summary>
        /// 是否锁定（成员变量）
        /// </summary>
        private bool lockOut;

        /// <summary>
        /// 锁定结束期
        /// </summary>
        public DateTime? LockoutEnd { get; private set; }
        
        /// <summary>
        /// 错误登录次数
        /// </summary>
        public int AccessFailedCount { get; private set; }

        /// <summary>
        /// 空构造函数：给 EFCore 构建数据使用
        /// </summary>
        private UserAccessFail()
        {
            
        }

        /// <summary>
        /// 给程序员使用
        /// </summary>
        /// <param name="user"></param>
        public UserAccessFail(User user)
        {
            Id = Guid.NewGuid();
            User = user;
        }

        /// <summary>
        /// 重置登录错误信息
        /// </summary>
        public void Reset()
        {
            lockOut = false;
            LockoutEnd = null;
            AccessFailedCount = 0;
        }

        /// <summary>
        /// 判断当前用户是否已经锁定
        /// </summary>
        /// <returns></returns>
        public bool IsLockOut()
        {
            if (lockOut)
            {
                if (LockoutEnd >= DateTime.Now)
                {
                    return true;
                }
                else// 锁定已经到期
                {
                    //AccessFailedCount = 0;
                    //LockoutEnd = null;
                    Reset();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    
        /// <summary>
        /// 处理一次“登录失败”
        /// </summary>
        public void Fail()
        {
            AccessFailedCount++;
            // 如果用户连续三次登录失败，则锁定此用户账号5分钟
            if (AccessFailedCount >= 3)
            {
                lockOut = true;
                LockoutEnd = DateTime.Now.AddMinutes(5);
            }
        }
    }
}
