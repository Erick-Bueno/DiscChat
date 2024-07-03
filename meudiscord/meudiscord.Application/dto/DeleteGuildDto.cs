public class DeleteGuildDto {
    public Guid externalIdServer;
    public Guid externalIdUser;

    public DeleteGuildDto(Guid externalIdServer, Guid externalIdUser)
    {
        this.externalIdServer = externalIdServer;
        this.externalIdUser = externalIdUser;
    }
}