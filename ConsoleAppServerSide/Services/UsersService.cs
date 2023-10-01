using System.Text.Json;
using AutoMapper;
using ClassLibraryCommunicationEntities.RequestResponseEntities;
using ClassLibraryCommunicationEntities.SocketsEntities;
using ConsoleAppServerSide.DbConnector;
using ConsoleAppServerSide.Models;

namespace ConsoleAppServerSide.Services;

public class UsersService
{
    private EcsDbContext _dbContext;
    private IMapper _mapper;

    public UsersService(EcsDbContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = AutoMapperConfig.GetInstance().GetMapper();
    }

    public ServerResponse processRegisterNewUserCommand(string jsonData)
    {
        RequestRegisterUserDto requestRegisterUserDto =
            JsonSerializer.Deserialize<RequestRegisterUserDto>(jsonData);

        User user = _mapper.Map<User>(requestRegisterUserDto);

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        ResponseRegisterUserDto responseRegisterUserDto = _mapper.Map<ResponseRegisterUserDto>(user);

        string responseRegisterUserDtoJson = JsonSerializer.Serialize(responseRegisterUserDto);

        return new ServerResponse(Statuses.Ok, responseRegisterUserDtoJson);
    }
}