using UserMgr.Domain.Entities;
using UserMgr.Domain.Events;
using UserMgr.Domain.ValueObjects;

namespace UserMgr.Domain.Interfaces
{
    public interface IUserDomainRepository
    {
        /// <summary>
        /// FindXXX:有可以返回 null:因为不是所有的手机号都对应存在用户
        /// GetXXX:一定不会返回 null
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Task<User?> FindOneAsync(PhoneNumber phoneNumber);

        public Task<User?> FindOneAsync(Guid userId);

        /// <summary>
        /// 短信验证码登录
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string message);

        public Task SavePhoneNumberCodeAsync(PhoneNumber phoneNumber, string code);

        public Task<string?> RetrievePhoneCodeAsync(PhoneNumber phoneNumber);

        /// <summary>
        /// 发布用户登录结果事件
        /// </summary>
        /// <param name="_event"></param>
        /// <returns></returns>
        public Task PublishEventAsync(UserAccessResultEvent _event);
    }
}
