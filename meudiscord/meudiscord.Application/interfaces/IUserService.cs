using OneOf;

public interface IUserService{
    public Task<OneOf<ResponseUserData, AppError>> FindUserAuthenticated(Guid externalId);
}