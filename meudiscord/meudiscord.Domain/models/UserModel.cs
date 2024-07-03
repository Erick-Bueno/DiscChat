using System.ComponentModel.DataAnnotations.Schema;

public class UserModel
{
    public UserModel(string name, string email, string password)
    {
        this.name = name;
        this.email = email;
        this.password = password;
    }

    public int id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid externalId { get; set; } = Guid.NewGuid();
    public string name { get; set; }
    public string email { get; set; }
    public string password {get; set;}
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    public List<ServerModel> servers { get; set; }
    public List<MessageModel> messages { get; set;}

}