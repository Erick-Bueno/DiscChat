
using OneOf;

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

    public async Task<OneOf<ResponseAuth, AppError>> Login(UserLoginDto user)
    {

        try
        {
            var foundUser = _userRepository.FindUserByEmail(user.email);
            if (foundUser == null)
                return new UserNotRegisteredError("Email não cadastrado");
            var verifyPassword = _passwordService.VerifyPassword(user.password, foundUser.password);
            if (verifyPassword == false)
                return new IncorrectPasswordError("Senha incorreta");

            var accesstoken = _jwtService.GenerateAccessToken(foundUser);
            var refreshToken = _jwtService.GenerateRefreshToken();
            await _tokenRepository.UpdateToken(foundUser.email, refreshToken);
            return new ResponseAuth(200, "úsuario logado com sucesso", refreshToken, accesstoken);
        }
        catch (Exception ex)
        {
            return new InternalServerError(ex.Message);
        }

    }

    public async Task<OneOf<ResponseAuth, AppError>> Register(UserRegisterDto user)
    {
        try
        {
            var foundUser = _userRepository.FindUserByEmail(user.email);
            if (foundUser != null)
                return new EmailIsAlreadyRegisteredError("Email ja cadastrado");

            var hashPassword = _passwordService.EncryptPassword(user.password);
            user.password = hashPassword;
            var userEntity = _convertUserRegisterDto.convertInUserEntity(user);
            var accesstoken = _jwtService.GenerateAccessToken(userEntity);
            var refreshToken = _jwtService.GenerateRefreshToken();
            var tokenEntity= _convertToken.ConvertInTokenEntity(refreshToken, userEntity.email);
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    await _userRepository.Register(userEntity);
                    await _tokenRepository.RegisterToken(tokenEntity);
                    transaction.Commit();
                    return new ResponseAuth(201, "úsuario cadastrado com sucesso", refreshToken, accesstoken);
                }
                catch (System.Exception ex)
                {
                    transaction.Rollback();
                    return new InternalServerError(ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            return new InternalServerError(ex.Message);
        }

    }
}