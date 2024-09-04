
public interface IPasswordService
{
    public string EncryptPassword(string password);
    public bool VerifyPassword(string password, string hashPassword);
}