using UserMgr.Domain.ValueObjects;

namespace UserMgr.WebAPI.Dtos;

public record CheckLoginByPhoneAndCodeRequest(PhoneNumber PhoneNumber, string Code);