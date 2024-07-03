using System.ComponentModel.DataAnnotations;

public class UserLoginDto
{
    [Required (ErrorMessage = "Informe um email")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Informe um email valido")]
    public string email { get; set; }
    [Required (ErrorMessage = "Informe uma senha")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$", ErrorMessage = "A senha deve conter no minimo 8 caracteres, letra minúscula, letra maiúscula, numero e caractere especial")]
    public string password { get; set; }
    
}