
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

    public async Task<OneOf<ResponseCreateGuild, AppError>> CreateGuild(GuildDto guild)
    {
        try
        {
            var user = _userRepository.FindUserByExternalId(guild.externalIdUser);
            if (user == null)
                return new UnableToCreateServerError("Não foi possivel criar o servidor");
            var serverEntity = _convertGuildDto.ConvertInServerEntity(guild, user.id);
            await _guildRepository.CreateGuild(serverEntity);
            return new ResponseCreateGuild(201, "Servidor criado com sucesso", serverEntity.externalId, serverEntity.serverName);
        }
        catch (Exception ex)
        {
            return new InternalServerError(ex.Message);
        }
    }

    public async Task<OneOf<ResponseSuccessDefault, AppError>> DeleteGuild(DeleteGuildDto guild)
    {
        try
        {
            var user = _userRepository.FindUserByExternalId(guild.externalIdUser);
            if (user == null)
                return new UnableToDeleteServerError("Não foi possivel deletar o servidor");
            var server = _guildRepository.FindServerByExternalIdServerAndIdUser(guild.externalIdServer, user.id);
            if (server == null)
                return new TheServerDoesNotBelongToTheUserTryingToDeleItError("O server não pertence ao usuário que esta tentando deleta-lo");

            await _guildRepository.DeleteGuild(server);
            return new ResponseSuccessDefault(200, "Servidor deletado com sucesso");
        }
        catch (Exception ex)
        {
            return new InternalServerError(ex.Message);
        }

    }

    public async Task<OneOf<ResponseAllGuilds, AppError>> GetAllGuilds()
    {
        try
        {
            var guilds = _guildRepository.GetAllGuilds();
            if (guilds.Count == 0)
                return new NoServersWereFoundError("Nenhum servidor encontrado");

            return new ResponseAllGuilds(200, "Guildas encontradas", guilds);
        }
        catch (Exception ex)
        {
            return new InternalServerError(ex.Message);
        }

    }

    public async Task<OneOf<ResponseGetGuildByExternalId, AppError>> GetGuildByExternalId(Guid externalIdGuild)
    {
        try
        {
            var guild = _guildRepository.FindGuildByExternalId(externalIdGuild);
            if (guild == null)
            {
                return new ServerNotFoundError("Servidor não encontrado");
            }
            return new ResponseGetGuildByExternalId(200, "Servidor encontrado", guild.serverName);
        }
        catch (Exception ex)
        {
            return new InternalServerError(ex.Message);
        }

    }
}