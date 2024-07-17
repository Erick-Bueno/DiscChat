using OneOf;

public interface IGuildService {
    public Task<OneOf<ResponseAllGuilds,AppError>> GetAllGuilds();
    public Task<OneOf<ResponseCreateGuild,AppError>> CreateGuild(GuildDto guild);
    public Task<OneOf<ResponseSuccessDefault,AppError>> DeleteGuild(DeleteGuildDto guild);
}