
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Response> FindUserAuthenticated(Guid externalId)
    {
        var userFinded = _userRepository.FindUserByExternalId(externalId);
        if(userFinded == null){
            return new ResponseError(400, "Usuário não encontrado");
        }
        return new ResponseUserData(200, "Usuário encontrado", userFinded.name, userFinded.email);
    }
}
