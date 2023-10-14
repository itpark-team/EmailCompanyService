using ClassLibraryCommunicationEntities.SocketsEntities;
using ConsoleAppServerSide.DbConnector;

namespace ConsoleAppServerSide.Services;

public class ServicesManager
{
    private Dictionary<string, Func<string, ServerResponse>>
        _methods;

    public ServicesManager(EcsDbContext dbContext)
    {
        UsersService usersService = new UsersService(dbContext);
        MailsService mailsService = new MailsService(dbContext);

        _methods = new Dictionary<string, Func<string, ServerResponse>>();
        _methods[Commands.RegisterNewUser] = usersService.ProcessRegisterNewUserCommand;
        _methods[Commands.AuthExistUser] = usersService.processAuthExistUserCommand;
        
        _methods[Commands.SendMail] = mailsService.processSendMailCommand;

        _methods[Commands.GetIsNotOpenedEmails] = mailsService.processGetIsNotOpenedMailsCommand;
        _methods[Commands.GetAllEmails] = mailsService.processAllMailsCommand;
    }

    public ServerResponse ProcessClientRequest(ClientRequest clientRequest)
    {
        Func<string, ServerResponse> method = _methods[clientRequest.Command];
        return method.Invoke(clientRequest.JsonData);
    }
}