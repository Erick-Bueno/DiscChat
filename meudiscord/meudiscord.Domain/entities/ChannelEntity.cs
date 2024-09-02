using System.ComponentModel.DataAnnotations.Schema;

public class ChannelEntity
{
    public int id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid externalId { get; set; } = Guid.NewGuid();
    public string name { get; set; }
    public int serverId { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; private set; } = DateTime.UtcNow;
    public ServerEntity server { get; set; }
    public List<MessageEntity> messages {get; set;}
}