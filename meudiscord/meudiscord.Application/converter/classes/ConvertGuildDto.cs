public class ConvertGuildDto : IConvertGuildDto
{
    public ServerModel ConvertInServerModel(GuildDto guild, int idUser)
    {
        return new ServerModel(guild.serverName, idUser);
    }
}