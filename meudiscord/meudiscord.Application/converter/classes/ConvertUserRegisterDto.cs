public class ConvertUserRegisterDto : IConvertUserRegisterDto
{
    public UserEntity convertInUserEntity(UserRegisterDto user)
    {
        return new UserEntity(user.name, user.email, user.password);
    }
}