using System.Text.Json;
using AutoMapper;
using ClassLibraryCommunicationEntities.RequestResponseEntities;
using ClassLibraryCommunicationEntities.SocketsEntities;
using ConsoleAppServerSide.DbConnector;
using ConsoleAppServerSide.Models;

namespace ConsoleAppServerSide.Services;

public class MailsService
{
    private EcsDbContext _dbContext;
    private IMapper _mapper;

    public MailsService(EcsDbContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = AutoMapperConfig.GetInstance().GetMapper();
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
    
    public ServerResponse processGetIsNotOpenedMailsCommand(string jsonData)
    {
        int idUser = int.Parse(jsonData);
        
        List<Mail> emails = _dbContext.Mails.Where(mail => mail.IdTo == idUser && !mail.IsOpened).ToList();

        if (emails == null)
        {
            return new ServerResponse(Statuses.ServerError,
                $"mail with {idUser} id not found");
        }

        List<ResponseViewEmailDto> responseViewEmails = new List<ResponseViewEmailDto>();

        for (int i = 0; i < emails.Count; i++)
        {
            responseViewEmails.Add(_mapper.Map<ResponseViewEmailDto>(emails[i]));
        }

        List<Mail> mails = _dbContext.Mails.ToList();

        for (int i = 0; i < mails.Count; i++)
        {
            mails[i].IsOpened = true;
        }

        _dbContext.SaveChanges();

        return new ServerResponse(Statuses.Ok, JsonSerializer.Serialize(responseViewEmails));
    }
    
    public ServerResponse processAllMailsCommand(string jsonData)
    {
        int idUser = int.Parse(jsonData);
        
        List<Mail> emails = _dbContext.Mails.Where(mail => mail.IdTo == idUser).ToList();

        if (emails == null)
        {
            return new ServerResponse(Statuses.ServerError,
                $"mail with {idUser} id not found");
        }

        List<ResponseViewEmailDto> responseViewEmails = new List<ResponseViewEmailDto>();

        for (int i = 0; i < emails.Count; i++)
        {
            responseViewEmails.Add(_mapper.Map<ResponseViewEmailDto>(emails[i]));
        }

        List<Mail> mails = _dbContext.Mails.ToList();

        for (int i = 0; i < mails.Count; i++)
        {
            mails[i].IsOpened = true;
        }

        _dbContext.SaveChanges();

        return new ServerResponse(Statuses.Ok, JsonSerializer.Serialize(responseViewEmails));
    }
}