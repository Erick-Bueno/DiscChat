public interface IUserService{
    public Task<Response> FindUserAuthenticated(Guid externalId);
}