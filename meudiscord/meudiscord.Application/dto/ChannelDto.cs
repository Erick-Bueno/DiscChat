using System.ComponentModel.DataAnnotations;
using System.Data.Common;

public class ChannelDto
{
    [Required(ErrorMessage = "Informe um nome para o canal")]
    public string channelName { get; set; }
    public Guid externalIdServer { get; set;}
    
}