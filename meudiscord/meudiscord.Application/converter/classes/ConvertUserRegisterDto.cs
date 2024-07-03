public class ConvertUserRegisterDto : IConvertUserRegisterDto
{
    public UserModel convertInUserModel(UserRegisterDto user)
    {
        return new UserModel(user.name, user.email, user.password);
    }
}