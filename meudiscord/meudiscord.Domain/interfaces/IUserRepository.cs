public interface IUserRepository
{
    public Task<UserEntity> Register(UserEntity user);
    public UserEntity FindUserByEmail (string email);
    public UserLinq FindUserByExternalId(Guid externalId);
}