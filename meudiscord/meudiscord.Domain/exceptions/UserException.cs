public class UserException:Exception
{
    public int status { get; private set; }
    public UserException(string? message, int status) : base(message)
    {
        this.status = status;
    }

    public static UserException UserRegisterError(string message){
        return new UserException(message, 400);
    }
    
}