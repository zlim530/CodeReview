using UserMgr.Domain.Entities;
using UserMgr.Domain.Enums;
using UserMgr.Domain.Events;
using UserMgr.Domain.Interfaces;
using UserMgr.Domain.ValueObjects;

namespace UserMgr.Domain
{
    public class UserDomainService
    {
        private readonly IUserDomainRepository userDomainRepository;
        private readonly ISmsCodeSender smsCodeSender;

        public UserDomainService(IUserDomainRepository userDomainRepository,
            ISmsCodeSender smsCodeSender)
        {
            userDomainRepository = userDomainRepository;
            smsCodeSender = smsCodeSender;
        }

        public async Task<UserAccessResult> CheckLoginAsync(PhoneNumber phoneNumber, string password)
        {
            User? user = await userDomainRepository.FindOneAsync(phoneNumber);
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
                    ResetAccessFail(user);
                }
                else
                {
                    AccessFail(user);
                }
            }
            UserAccessResultEvent resultEvent = new (phoneNumber, result);
            await userDomainRepository.PublishEventAsync(resultEvent);
            return result;
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
