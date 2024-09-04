using System.ComponentModel.DataAnnotations.Schema;

public class MessageEntity
{
    public int id { get;  set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid externalId { get;  set; } = Guid.NewGuid();
    public int idUser { get; set; }
    public int idChannel { get; set; }
    public string message { get; set;}
    public DateTime createdAt { get; private set;} = DateTime.UtcNow;
    public UserEntity user { get; set; }
    public ChannelEntity channel { get; set; }
}