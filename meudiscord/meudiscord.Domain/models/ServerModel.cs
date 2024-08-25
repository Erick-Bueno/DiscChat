using System.ComponentModel.DataAnnotations.Schema;

public class ServerModel
{
    public ServerModel(string serverName, int idUser)
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
    public UserModel user { get; set; }
    public List<ChannelModel> channels { get; set;}
}