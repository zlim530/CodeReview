using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMgr.Domain.Enums
{
    /// <summary>
    /// 用户查询结果：模型类，用于传递数据
    /// </summary>
    public enum UserAccessResult
    {
        OK, PhoneNumberNotFound, Lockout, NoPassword, PasswordError
    }
}
