using UserMgr.Domain.Entities;
using UserMgr.Domain.Enums;
using UserMgr.Domain.Events;
using UserMgr.Domain.Interfaces;
using UserMgr.Domain.ValueObjects;

namespace UserMgr.Domain
{
    /// <summary>
    /// Domain：包括实体类（聚合根）、事件（UserAccessResultEvent）、防腐层接口（ISmsCodeSender）、仓储接口（IUserDomainRepository）、领域服务（UserDomainService）
    /// 领域服务
    /// </summary>
    public class UserDomainService
    {
        private readonly IUserDomainRepository _userDomainRepository;
        private readonly ISmsCodeSender _smsCodeSender;

        public UserDomainService(IUserDomainRepository userDomainRepository,
            ISmsCodeSender smsCodeSender)
        {
            _userDomainRepository = userDomainRepository;
            _smsCodeSender = smsCodeSender;
        }

        public async Task<UserAccessResult> CheckLoginAsync(PhoneNumber phoneNumber, string password)
        {
            User? user = await _userDomainRepository.FindOneAsync(phoneNumber);
            UserAccessResult result;
            if (user == null)
            {
                result = UserAccessResult.PhoneNumberNotFound;
            }
            else if (IsLockUser(user))
            {
                result = UserAccessResult.Lockout;
            }
            else if (user.HasPassword() == false)
            {
                result = UserAccessResult.NoPassword;
            }
            else if (user.CheckPassword(password))
            {
                result = UserAccessResult.OK;
            }
            else
            {
                result = UserAccessResult.PasswordError;
            }
            if (user != null)
            {
                if (result == UserAccessResult.OK)
                {
                    ResetAccessFail(user); // 重置成功
                }
                else
                {
                    AccessFail(user); // 处理登录失败
                }
            }
            UserAccessResultEvent resultEvent = new (phoneNumber, result);
            await _userDomainRepository.PublishEventAsync(resultEvent);
            return result;
        }

        public async Task<UserAccessResult> SendCodeAsync(PhoneNumber phoneNumber)
        {
            var user = await _userDomainRepository.FindOneAsync(phoneNumber);
            if (user == null)
            {
                return UserAccessResult.PhoneNumberNotFound;
            }
            if (IsLockUser(user))
            {
                return UserAccessResult.Lockout;
            }
            string code = Random.Shared.Next(1000, 9999).ToString();
            await _userDomainRepository.SavePhoneNumberCodeAsync(phoneNumber, code);
            await _smsCodeSender.SendCodeAsync(phoneNumber, code);
            return UserAccessResult.OK;
        }

        public async Task<CheckCodeResult> CheckCodeAsync(PhoneNumber phoneNumber, string code)
        {
            var user = await _userDomainRepository.FindOneAsync(phoneNumber);
            if (user == null)
            {
                return CheckCodeResult.PhoneNumberNotFound;
            }
            if (IsLockUser(user)) 
            {
                return CheckCodeResult.Lockout;
            }
            string? codeInServer = await _userDomainRepository.RetrievePhoneCodeAsync(phoneNumber);
            if (string.IsNullOrEmpty(codeInServer))
            {
                return CheckCodeResult.CodeError;
            }
            if (code == codeInServer)
            {
                return CheckCodeResult.OK;
            }
            else
            {
                AccessFail(user);
                return CheckCodeResult.CodeError;
            }
        }

        public void ResetAccessFail(User user)
        {
            user.AccessFail.Reset();
        }

        public bool IsLockUser(User user) 
        {
            return user.AccessFail.IsLockOut();
        }

        public void AccessFail(User user)
        {
            user.AccessFail.Fail();
        }
    }
}
