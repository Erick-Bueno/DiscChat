public interface IGuildService {
    public Task<Response> GetAllGuilds();
    public Task<Response> CreateGuild(GuildDto guild);
    public Task<Response > DeleteGuild(DeleteGuildDto guild);
}