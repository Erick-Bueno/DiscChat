using System.ComponentModel.DataAnnotations.Schema;

public class MessageModel
{
    public int id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid externalId { get; set; } = Guid.NewGuid();
    public int idUser { get; set; }
    public int idChannel { get; set; }
    public string message { get; set;}
    public DateTime createdAt { get; set;} = DateTime.UtcNow;
    public UserModel user { get; set; }
    public ChannelModel channel { get; set; }
}