public interface IAuthService
{
    public Task<Response> Register(UserRegisterDto user);
    public Task<Response> Login(UserLoginDto user);
}