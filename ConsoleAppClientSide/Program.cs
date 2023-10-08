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
    UiUtil.PrintlnString("Меню");
    UiUtil.PrintlnString("1.Регистрация");
    UiUtil.PrintlnString("0.Выход");

    int action = UiUtil.InputIntWithBounds("Введите действие: ", 0, 1);

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
    }
    else if (serverResponse.Status == Statuses.ServerError)
    {
        UiUtil.PrintlnString("Запрос завершился с ошибкой на стороне сервера");
    }

    UiUtil.PrintlnString("Нажмите <Enter> для продолжения");
    UiUtil.WaitPressAnyKey();
}

clientEngine.CloseClientSocket();
UiUtil.PrintlnString("Клиент успешно отсоединён");