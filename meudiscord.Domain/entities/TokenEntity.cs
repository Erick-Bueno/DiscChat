using System.ComponentModel.DataAnnotations.Schema;

public class TokenEntity
{
    public TokenEntity(string email, string token)
    {
        this.email = email;
        this.token = token;
    }

    public int id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid externalId { get; set; } = Guid.NewGuid();
    public string email { get; set; }
    public string token { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; private set; } = DateTime.UtcNow;
}