using System.Text.Json;
using ClassLibraryCommunicationEntities.RequestResponseEntities;
using ClassLibraryCommunicationEntities.SocketsEntities;

namespace ConsoleAppClientSide.Services;

public class MailsService
{
    public ClientRequest processSendMailCommand()
    {
        string emailTo = UiUtil.InputStringWithBound("Введите email получателя: ", 32);
        string title = UiUtil.InputStringWithBound("Введите заголовок письма: ", 128);
        string message = UiUtil.InputStringWithBound("Введите текст письма: ", 4096);

        RequestSendMailDto requestSendMailDto = new RequestSendMailDto()
        {
            IdFrom = DataStorage.GetWithType<ResponseAuthUserDto>("user").Id,
            EmailTo = emailTo,
            Message = message,
            Title = title
        };

        return new ClientRequest(Commands.SendMail,
            JsonSerializer.Serialize(requestSendMailDto));
    }
    
    public ClientRequest processGetIsNotOpennedMailsCommand()
    {
        int idUser = DataStorage.GetWithType<ResponseAuthUserDto>("user").Id;

        return new ClientRequest(Commands.GetIsNotOpenedEmails,
            JsonSerializer.Serialize(idUser));
    }
    
    public ClientRequest processGetAllMailsCommand()
    {
        int idUser = DataStorage.GetWithType<ResponseAuthUserDto>("user").Id;

        return new ClientRequest(Commands.GetAllEmails,
            JsonSerializer.Serialize(idUser));
    }
}