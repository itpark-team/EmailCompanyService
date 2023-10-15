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

    public ServerResponse ProcessRegisterNewUserCommand(string jsonData)
    {
        RequestRegisterUserDto? requestRegisterUserDto =
            JsonSerializer.Deserialize<RequestRegisterUserDto>(jsonData);

        User user = _mapper.Map<User>(requestRegisterUserDto);

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        ResponseRegisterUserDto responseRegisterUserDto = _mapper.Map<ResponseRegisterUserDto>(user);

        string responseRegisterUserDtoJson = JsonSerializer.Serialize(responseRegisterUserDto);

        return new ServerResponse(Statuses.Ok, responseRegisterUserDtoJson);
    }

    public ServerResponse processAuthExistUserCommand(string jsonData)
    {
        RequestAuthUserDto requestAuthUserDto =
            JsonSerializer.Deserialize<RequestAuthUserDto>(jsonData);

        User user = _dbContext.Users
            .Where(user => user.Login == requestAuthUserDto.Login && user.Password == requestAuthUserDto.Password)
            .FirstOrDefault();

        if (user != null)
        {
            ResponseAuthUserDto responseAuthUserDto = _mapper.Map<ResponseAuthUserDto>(user);

            string responseAuthUserDtoJson = JsonSerializer.Serialize(responseAuthUserDto);

            return new ServerResponse(Statuses.Ok, responseAuthUserDtoJson);
        }

        return new ServerResponse(Statuses.ServerError,
            $"user with {requestAuthUserDto.Login} and {requestAuthUserDto.Password} not found");
    }
}