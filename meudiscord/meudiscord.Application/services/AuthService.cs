
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IConvertUserRegisterDto _convertUserRegisterDto;
    private readonly IConvertToken _convertToken;
    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenRepository _tokenRepository;

    public AuthService(IUserRepository userRepository, IPasswordService passwordService, IConvertUserRegisterDto convertUserRegisterDto, IJwtService jwtService, IUnitOfWork unitOfWork, IConvertToken convertToken, ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _convertUserRegisterDto = convertUserRegisterDto;
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
        _convertToken = convertToken;
        _tokenRepository = tokenRepository;
    }

    public async Task<Response> Login(UserLoginDto user)
    {
        var foundUser = _userRepository.FindUserByEmail(user.email);
        if(foundUser == null)
        {
            return new ResponseError(400, "Úsuario não cadastrado");
        }
        var verifyPassword = _passwordService.VerifyPassword(user.password, foundUser.password);
        if(verifyPassword == false)
        {
            return new ResponseError(400, "Senha incorreta");
        }
        var accesstoken = _jwtService.GenerateAccessToken(foundUser);
        var refreshToken = _jwtService.GenerateRefreshToken();
        await _tokenRepository.UpdateToken(foundUser.email, refreshToken);
        return new ResponseAuth(200, "úsuario logado com sucesso",refreshToken,accesstoken);
    }

    public async Task<Response> Register(UserRegisterDto user)
    {
        var foundUser = _userRepository.FindUserByEmail(user.email);
        if(foundUser != null)
        {
            return new ResponseError(400, "Email ja cadastrado");
        }
        var hashPassword = _passwordService.EncryptPassword(user.password);
        user.password = hashPassword;
        var userModel = _convertUserRegisterDto.convertInUserModel(user);
        var accesstoken = _jwtService.GenerateAccessToken(userModel);
        var refreshToken = _jwtService.GenerateRefreshToken();
        var tokenModel = _convertToken.ConvertInTokenModel(refreshToken, userModel.email);
        using(var transaction = _unitOfWork.BeginTransaction()){
            try
            {
                await _userRepository.Register(userModel);
                await _tokenRepository.RegisterToken(tokenModel);
                transaction.Commit();
                return new ResponseAuth(201, "úsuario cadastrado com sucesso",refreshToken,accesstoken);
            }
            catch (System.Exception ex)
            {   
                transaction.Rollback();
                throw UserException.UserRegisterError("Erro ao cadastrar");
            }
        }
    }
}