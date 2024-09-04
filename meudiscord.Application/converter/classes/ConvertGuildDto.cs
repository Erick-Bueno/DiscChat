public class ConvertGuildDto : IConvertGuildDto
{
    public ServerEntity ConvertInServerEntity(GuildDto guild, int idUser)
    {
        return new ServerEntity(guild.serverName, idUser);
    }
}