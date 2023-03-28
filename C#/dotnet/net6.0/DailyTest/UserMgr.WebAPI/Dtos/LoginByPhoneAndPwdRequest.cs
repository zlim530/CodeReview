using UserMgr.Domain.ValueObjects;

namespace UserMgr.WebAPI.Dtos;

public record LoginByPhoneAndPwdRequest(PhoneNumber PhoneNumber, string Password);
