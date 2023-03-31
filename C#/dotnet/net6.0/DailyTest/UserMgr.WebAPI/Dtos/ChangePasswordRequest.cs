namespace UserMgr.WebAPI.Dtos
{
    public record ChangePasswordRequest(Guid Id, string Password);
}
