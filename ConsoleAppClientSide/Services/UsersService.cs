using System.Text.Json;
using ClassLibraryCommunicationEntities.RequestResponseEntities;
using ClassLibraryCommunicationEntities.SocketsEntities;

namespace ConsoleAppClientSide.Services;

public class UsersService
{
    public ClientRequest processRegisterNewUserCommand()
    {
        string login = UiUtil.InputStringWithBound("Введите логин: ", 32);
        string password = UiUtil.InputStringWithBound("Введите пароль: ", 32);
        string name = UiUtil.InputStringWithBound("Введите имя: ", 32);
        string email = UiUtil.InputStringWithBound("Введите email: ", 32);

        RequestRegisterUserDto requestRegisterUserDto = new RequestRegisterUserDto()
        {
            Login = login,
            Password = password,
            Name = name,
            Email = email
        };

        return new ClientRequest(Commands.RegisterNewUser,
            JsonSerializer.Serialize(requestRegisterUserDto));
    }

    public ClientRequest processAuthExistUserCommand()
    {
        string login = UiUtil.InputStringWithBound("Введите логин: ", 32);
        string password = UiUtil.InputStringWithBound("Введите пароль: ", 32);
        
        RequestAuthUserDto requestAuthUserDto = new RequestAuthUserDto()
        {
            Login = login,
            Password = password
        };

        return new ClientRequest(Commands.AuthExistUser,
            JsonSerializer.Serialize(requestAuthUserDto));
    }
}