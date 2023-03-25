using UserMgr.Domain.ValueObjects;
using Zack.Commons;

namespace UserMgr.Domain.Entities
{
    /// <summary>
    /// 用户聚合根
    /// </summary>
    public record User : IAggregateRoot
    {
        public Guid Id { get; init; }

        public PhoneNumber PhoneNumber { get; private set; }

        /// <summary>
        /// 密码的散列值
        /// </summary>
        private string? passwordHash;

        public UserAccessFail AccessFail { get; set; }

        private User()
        {
            
        }

        /// <summary>
        /// 在本程序规定手机号是必填的，但密码不是
        /// </summary>
        /// <param name="phoneNumber"></param>
        public User(PhoneNumber phoneNumber)
        {
            Id = Guid.NewGuid();
            PhoneNumber = phoneNumber;
            // AccessFail 不能为空，否则有 bug
            AccessFail = new UserAccessFail(this);
        }

        /// <summary>
        /// 是否设置了密码
        /// </summary>
        /// <returns></returns>
        public bool HasPassword()
        {
            return !string.IsNullOrEmpty(passwordHash);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public void ChangePassword(string value)
        {
            if (value.Length <= 3)
            {
                throw new ArgumentException("密码长度不能小于3");
            }
            passwordHash = HashHelper.ComputeMd5Hash(value);
        }

        /// <summary>
        /// 检查密码是否正确
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckPassword(string password)
        {
            return passwordHash == HashHelper.ComputeMd5Hash(password);
        }

        /// <summary>
        /// 修改手机号码
        /// </summary>
        /// <param name="phoneNumber"></param>
        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
