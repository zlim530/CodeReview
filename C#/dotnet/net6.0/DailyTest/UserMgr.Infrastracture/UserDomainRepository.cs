using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using UserMgr.Domain.Entities;
using UserMgr.Domain.Events;
using UserMgr.Domain.Interfaces;
using UserMgr.Domain.ValueObjects;
using UserMgr.Infrastracture.DbContexts;
using Zack.Infrastructure.EFCore;

namespace UserMgr.Infrastracture
{
    /// <summary>
    /// 领域模型、领域服务中只是定义了抽象的实体、防腐层接口和仓储接口，我们需要在基础设施中对它们进行落地和实现
    /// 实体类、值对象（UserAccessFail）的定义是和持久化机制无关的，而它们需要通过 EFCore 的配置、上下文类等建立和数据库的关系
    /// 实体类的配置、上下文类等也是和持久层相关的，因此放到基础设施层，还有防腐层接口、仓储接口的实现
    /// Repository 不一定放在数据库中，而是可以放在任何实现数据存储的服务中（如分布式缓存等）
    /// </summary>
    public class UserDomainRepository : IUserDomainRepository
    {
        private readonly UserDbContext userDbContext;
        private readonly IDistributedCache distributedCache;
        private readonly IMediator mediator;

        public UserDomainRepository(UserDbContext userDbContext,
            IDistributedCache distributedCache,
            IMediator mediator)
        {
            this.userDbContext = userDbContext;
            this.distributedCache = distributedCache;
            this.mediator = mediator;
        }

        public async Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string message)
        {
            var user = await FindOneAsync(phoneNumber);
            UserLoginHistory history = new UserLoginHistory(user?.Id, phoneNumber, message);
            userDbContext.UserLoginHistories.Add(history); 
            // 没有 SaveChangesAsync() 也即这里不保存，只是对实体状态进行操作
        }

        public Task<User?> FindOneAsync(PhoneNumber phoneNumber)
        {
            // AccessFail 是延迟加载的，需要联表查询关联出来
            return userDbContext.Users.Include(u => u.AccessFail).SingleOrDefaultAsync(ExpressionHelper.MakeEqual((User u) => u.PhoneNumber, phoneNumber));
        }

        public async Task<User?> FindOneAsync(Guid userId)
        {
            return await userDbContext.Users.Include(u => u.AccessFail).SingleOrDefaultAsync(u => u.Id == userId);
        }

        public Task PublishEventAsync(UserAccessResultEvent _event)
        {
            mediator.Publish(_event);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取短信验证码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<string?> RetrievePhoneCodeAsync(PhoneNumber phoneNumber)
        {
            string key = phoneNumber.RegionCode + phoneNumber.Number;
            string cacheKey = $"LoginByPhoneAndCode_Code_{key}";
            string? code = await distributedCache.GetStringAsync(cacheKey);
            distributedCache.Remove(cacheKey);// 设置验证码获取一次就清除（无效）
            return code;
        }

        /// <summary>
        /// 保存手机号与对应的验证码：为了展示 Repository 不一定总是放在数据库中，这个方法实现使用分布式缓存
        /// 为什么不用内存缓存，是因为 Web 服务器可能分布在多台不同的服务器中，如果直接保存到内存服务器可能保存与获取的并不是同一个服务器
        /// 而分布式缓存又天然有缓存过期时间的属性，因此解决了验证有效时间的设定
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public Task SavePhoneNumberCodeAsync(PhoneNumber phoneNumber, string code)
        {
            string key = phoneNumber.RegionCode + phoneNumber.Number;
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
            // 当一个方法的最后一行是 Async 方法，我们可以直接返回不使用 await，这样在方法定义申明中也不需要 async
            // 这样性能更优
            return distributedCache.SetStringAsync($"LoginByPhoneAndCode_Code_{key}", code, options);
            
        }
    }
}