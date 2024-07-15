using System.ComponentModel.DataAnnotations;

public class GuildDto
{
    [Required(ErrorMessage = "Informe um nome ao servidor")]
    public string serverName { get; set; }
    public Guid externalIdUser { get; set; }
}