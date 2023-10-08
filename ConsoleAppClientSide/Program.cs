using System.Text.Json;
using ClassLibraryCommunicationEntities.RequestResponseEntities;
using ClassLibraryCommunicationEntities.SocketsEntities;
using ConsoleAppClientSide;
using ConsoleAppClientSide.CommunicationEngine;
using ConsoleAppClientSide.Services;

ClientEngine clientEngine = new ClientEngine("127.0.0.1", 34536);
clientEngine.ConnectToServer();

UiUtil.PrintlnString("Вы успешно подключились! Нажмите <Enter> для продолжения");
UiUtil.WaitPressAnyKey();

ServicesManager servicesManager = new ServicesManager();

while (true)
{
    UiUtil.ClearConsole();

    if (DataStorage.ContainsKey("user"))
    {
        ResponseAuthUserDto user = DataStorage.GetWithType<ResponseAuthUserDto>("user");
        
        UiUtil.PrintlnString($"{user.Name}, добро пожаловать. Ваш email: {user.Email}");
    }

    UiUtil.PrintlnString("Меню");
    UiUtil.PrintlnString("1.Регистрация");
    UiUtil.PrintlnString("2.Авторизация");
    UiUtil.PrintlnString("3.Отправить письмо");
    UiUtil.PrintlnString("0.Выход");

    int action = UiUtil.InputIntWithBounds("Введите действие: ", 0, 3);

    if (action == 0)
    {
        break;
    }

    ClientRequest clientRequest = servicesManager.ProcessAction(action);

    string messageToServer = JsonSerializer.Serialize(clientRequest);

    clientEngine.SendMessage(messageToServer);

    string messageFromServer = clientEngine.ReceiveMessage();

    ServerResponse serverResponse = JsonSerializer.Deserialize<ServerResponse>(messageFromServer);

    if (serverResponse.Status == Statuses.Ok)
    {
        UiUtil.PrintlnString("Запрос выполнен успешно");

        if (action == 2)
        {
            ResponseAuthUserDto user = JsonSerializer.Deserialize<ResponseAuthUserDto>(serverResponse.JsonData);

            DataStorage.AddOrUpdate("user", user);
        }
    }
    else if (serverResponse.Status == Statuses.ServerError)
    {
        UiUtil.PrintlnString("Запрос завершился с ошибкой на стороне сервера: " + serverResponse.JsonData);
    }

    UiUtil.PrintlnString("Нажмите <Enter> для продолжения");
    UiUtil.WaitPressAnyKey();
}

clientEngine.CloseClientSocket();
UiUtil.PrintlnString("Клиент успешно отсоединён");
UiUtil.PrintlnString("Программа будет завершена");