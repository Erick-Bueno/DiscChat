using DevOne.Security.Cryptography.BCrypt;

public class PasswordService : IPasswordService
{
    public bool VerifyPassword(string password, string hashPassword)
    {
      return BCryptHelper.CheckPassword(password, hashPassword );
    }

    public string EncryptPassword(string password)
    {
        var salt = BCryptHelper.GenerateSalt(10);
        var hashPassword = BCryptHelper.HashPassword(password, salt);
        return hashPassword;
    }
}