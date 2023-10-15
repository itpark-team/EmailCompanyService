using ClassLibraryCommunicationEntities.SocketsEntities;
using ConsoleAppClientSide.CommunicationEngine;

namespace ConsoleAppClientSide.Services;

public class ServicesManager
{
    private Dictionary<int, Func<ClientRequest>> _methods;

    public ServicesManager()
    {
        UsersService usersService = new UsersService();
        MailsService mailsService = new MailsService();

        _methods = new Dictionary<int, Func<ClientRequest>>();
        _methods[1] = usersService.processRegisterNewUserCommand;
        _methods[2] = usersService.processAuthExistUserCommand;
        _methods[3] = mailsService.processSendMailCommand;
        _methods[4] = mailsService.processGetIsNotOpennedMailsCommand;
        _methods[5] = mailsService.processGetAllMailsCommand;
    }

    public ClientRequest ProcessAction(int action)
    {
        Func<ClientRequest> method = _methods[action];
        return method.Invoke();
    }
}