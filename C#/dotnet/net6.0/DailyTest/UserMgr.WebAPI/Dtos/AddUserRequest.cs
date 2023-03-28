using UserMgr.Domain.ValueObjects;

namespace UserMgr.WebAPI.Dtos;

public record AdduserRequest(PhoneNumber PhoneNumber, string Password);