
using OneOf;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OneOf<ResponseUserData, AppError>> FindUserAuthenticated(Guid externalId)
    {
        try
        {
            var userFinded = _userRepository.FindUserByExternalId(externalId);
            if (userFinded == null)
                return new UserNotFoundError("Usuário não encontrado");
            return new ResponseUserData(200, "Usuário encontrado", userFinded.name, userFinded.email);
        }
        catch (Exception ex)
        {
            return new InternalServerError(ex.Message); 
        }

    }
}
