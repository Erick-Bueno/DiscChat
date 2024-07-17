using OneOf;

public interface IAuthService
{
    public Task<OneOf<ResponseAuth, AppError>> Register(UserRegisterDto user);
    public Task<OneOf<ResponseAuth, AppError>> Login(UserLoginDto user);
}