using ConsoleAppServerSide.DbConnector;

namespace ConsoleAppServerSide.Services;

public class MailsService
{
    private EcsDbContext _dbContext;

    public MailsService(EcsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}