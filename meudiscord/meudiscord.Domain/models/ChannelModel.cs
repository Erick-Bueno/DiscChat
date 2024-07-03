using System.ComponentModel.DataAnnotations.Schema;

public class ChannelModel
{
    public int id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid externalId { get; set; } = Guid.NewGuid();
    public string name { get; set; }
    public int serverId { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    public ServerModel server { get; set; }
    public List<MessageModel> messages {get; set;}
}