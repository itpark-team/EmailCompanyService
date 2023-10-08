using System.Text.Json;
using ClassLibraryCommunicationEntities.RequestResponseEntities;
using ClassLibraryCommunicationEntities.SocketsEntities;
using ConsoleAppServerSide.DbConnector;
using ConsoleAppServerSide.Models;

namespace ConsoleAppServerSide.Services;

public class MailsService
{
    private EcsDbContext _dbContext;

    public MailsService(EcsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ServerResponse processSendMailCommand(string jsonData)
    {
        RequestSendMailDto requestSendMailDto =
            JsonSerializer.Deserialize<RequestSendMailDto>(jsonData);

        User userTo = _dbContext.Users.Where(user => user.Email == requestSendMailDto.EmailTo).FirstOrDefault();

        if (userTo == null)
        {
            return new ServerResponse(Statuses.ServerError,
                $"user with {requestSendMailDto.EmailTo} not found");
        }

        Mail mail = new Mail()
        {
            IdTo = userTo.Id,
            IdFrom = requestSendMailDto.IdFrom,
            Title = requestSendMailDto.Title,
            Message = requestSendMailDto.Message,
            IsOpened = false,
            CreatedDatetime = DateTime.Now
        };

        _dbContext.Mails.Add(mail);
        _dbContext.SaveChanges();

        return new ServerResponse(Statuses.Ok);
    }
}