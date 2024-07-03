

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public UserModel FindUserByEmail(string email)
    {
        return _appDbContext.users.Where(u => u.email == email).FirstOrDefault();
    }

    public UserLinq FindUserByExternalId(Guid externalId)
    {
        return _appDbContext.users.Where(u => u.externalId == externalId)
        .Select(
            u => new UserLinq
        {
            id = u.id,
            email = u.email,
            name = u.name
        })
        .FirstOrDefault();
    }

    public async Task<UserModel> Register(UserModel user)
    {
        await _appDbContext.users.AddAsync(user);
        await _appDbContext.SaveChangesAsync();
        return user;
    }
}