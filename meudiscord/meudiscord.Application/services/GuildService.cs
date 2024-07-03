
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

    public async Task<Response> CreateGuild(GuildDto guild)
    {
        var user = _userRepository.FindUserByExternalId(guild.externalIdUser);
        if(user == null){
            return new ResponseError(400, "Não foi possivel criar o servidor");
        }
        var serverModel = _convertGuildDto.ConvertInServerModel(guild, user.id);
        await _guildRepository.CreateGuild(serverModel);
        return new ResponseCreateGuild(201, "Servidor criado com sucesso", serverModel.externalId);
    }

    public async Task<Response> DeleteGuild(DeleteGuildDto guild)
    {
        var user = _userRepository.FindUserByExternalId(guild.externalIdUser);
        if(user == null){
            return new ResponseError(400, "Não foi possivel deletar o servidor");
        }
        var server = _guildRepository.FindServerByExternalIdServerAndIdUser(guild.externalIdServer, user.id);
        if(server == null){
            return new ResponseError(400, "O servidor não pertence ao usuário que esta tentando deleta-lo");
        }
        await _guildRepository.DeleteGuild(server);
        return new ResponseSuccessDefault(200, "Servidor deletado com sucesso");
    }

    public async Task<Response> GetAllGuilds()
    {
        var guilds = await _guildRepository.GetAllGuilds();
        if(guilds.Count == 0){
            return new ResponseError(400, "Nenhum servidor foi encontrado");
        }
        return new ResponseAllGuilds(200, "Guildas encontradas", guilds);
    }
}