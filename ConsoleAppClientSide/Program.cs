using System.Text.Json;
using ClassLibraryCommunicationEntities.RequestResponseEntities;
using ClassLibraryCommunicationEntities.SocketsEntities;
using ConsoleAppClientSide.CommunicationEngine;

ClientEngine clientEngine = new ClientEngine("127.0.0.1", 34536);
clientEngine.ConnectToServer();

Console.WriteLine("Вы успешно подключились! Нажмите <Enter> для продолжения");
Console.ReadKey();


bool isRun = true;
while (isRun)
{
    Console.Clear();
    Console.WriteLine("Меню");
    Console.WriteLine("1.Регистрация");
    Console.WriteLine("0.Выход");

    Console.Write("Введите действие: ");
    int action = int.Parse(Console.ReadLine());

    switch (action)
    {
        case 1:
        {
            Console.Write("Введите логин: ");
            string login = Console.ReadLine();

            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            Console.Write("Введите имя: ");
            string name = Console.ReadLine();

            Console.Write("Введите email: ");
            string email = Console.ReadLine();

            RequestRegisterUserDto requestRegisterUserDto = new RequestRegisterUserDto()
            {
                Login = login,
                Password = password,
                Name = name,
                Email = email
            };

            ClientRequest clientRequest = new ClientRequest()
            {
                Command = Commands.RegisterNewUser,
                JsonData = JsonSerializer.Serialize(requestRegisterUserDto)
            };

            string messageToServer = JsonSerializer.Serialize(clientRequest);
            clientEngine.SendMessage(messageToServer);
        }

            break;
        case 0:
        {
            isRun = false;
        }
            break;
    }
}

clientEngine.CloseClientSocket();