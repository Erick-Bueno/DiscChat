public interface IUserRepository
{
    public Task<UserModel> Register(UserModel user);
    public UserModel FindUserByEmail (string email);
    public UserLinq FindUserByExternalId(Guid externalId);
}