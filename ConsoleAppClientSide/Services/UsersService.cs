using System.Text.Json;
using ClassLibraryCommunicationEntities.RequestResponseEntities;
using ClassLibraryCommunicationEntities.SocketsEntities;

namespace ConsoleAppClientSide.Services;

public class UsersService
{
    public ClientRequest processRegisterNewUserCommand()
    {
        string login = UiUtil.InputString("Введите логин: ");
        string password = UiUtil.InputString("Введите пароль: ");
        string name = UiUtil.InputString("Введите имя: ");
        string email = UiUtil.InputString("Введите email: ");

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
}