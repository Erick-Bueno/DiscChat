using System.ComponentModel.DataAnnotations.Schema;

public class ServerEntity
{
    public ServerEntity(string serverName, int idUser)
    {
        this.serverName = serverName;
        this.idUser = idUser;
    }

    public int id { get;  set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid externalId { get; set; } = Guid.NewGuid(); 
    public string serverName { get; set; }
    public int idUser { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; private set; } = DateTime.UtcNow;
    public UserEntity user { get; set; }
    public List<ChannelEntity> channels { get; set;}
}