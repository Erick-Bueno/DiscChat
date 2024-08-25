
using OneOf;

public class GuildService : IGuildService
{
    private readonly IGuildRepository _guildRepository;
    private readonly IUserRepository _userRepository;
    private readonly IConvertGuildDto _convertGuildDto;

    public GuildService(IGuildRepository guildRepository, IUserRepository userRepository, IConvertGuildDto convertGuildDto)
    {
        _guildRepository = guildRepository;
        _userRepository = userRepository;
        _convertGuildDto = convertGuildDto;
    }

    public async Task<OneOf<ResponseCreateGuild,AppError>> CreateGuild(GuildDto guild)
    {
        Console.WriteLine(guild.externalIdUser);
        var user = _userRepository.FindUserByExternalId(guild.externalIdUser);
        if(user == null)
            return new UnableToCreateServerError();
        var serverModel = _convertGuildDto.ConvertInServerModel(guild, user.id);
        await _guildRepository.CreateGuild(serverModel);
        return new ResponseCreateGuild(201, "Servidor criado com sucesso", serverModel.externalId, serverModel.serverName);
    }

    public async Task<OneOf<ResponseSuccessDefault,AppError>> DeleteGuild(DeleteGuildDto guild)
    {
        var user = _userRepository.FindUserByExternalId(guild.externalIdUser);
        if(user == null)
            return new UnableToDeleteServerError();
        var server = _guildRepository.FindServerByExternalIdServerAndIdUser(guild.externalIdServer, user.id);
        if(server == null)
            return new TheServerDoesNotBelongToTheUserTryingToDeleItError();
        
        await _guildRepository.DeleteGuild(server);
        return new ResponseSuccessDefault(200, "Servidor deletado com sucesso");
    }

    public async Task<OneOf<ResponseAllGuilds,AppError>> GetAllGuilds()
    {
        var guilds = _guildRepository.GetAllGuilds();
        if(guilds.Count == 0)
            return new NoServersWereFoundError();
        
        return new ResponseAllGuilds(200, "Guildas encontradas", guilds);
    }

    public async Task<OneOf<ResponseGetGuildByExternalId, AppError>> GetGuildByExternalId(Guid externalIdGuild)
    {
        var guild = _guildRepository.FindGuildByExternalId(externalIdGuild);
        if(guild == null){
            return new ServerNotFoundError();
        }
        return new ResponseGetGuildByExternalId(200, "Servidor encontrado", guild.serverName);
    }
}