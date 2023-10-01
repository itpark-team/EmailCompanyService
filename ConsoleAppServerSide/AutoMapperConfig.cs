using AutoMapper;
using ClassLibraryCommunicationEntities.RequestResponseEntities;
using ConsoleAppServerSide.Models;

public class AutoMapperConfig
{
    private static AutoMapperConfig Instance = null;

    public static AutoMapperConfig GetInstance()
    {
        if (Instance == null)
        {
            Instance = new AutoMapperConfig();
        }

        return Instance;
    }

    private IMapper _mapper;

    public AutoMapperConfig()
    {
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<RequestRegisterUserDto, User>();
            cfg.CreateMap<User, ResponseRegisterUserDto>();
        });
        _mapper = config.CreateMapper();
    }

    public IMapper GetMapper()
    {
        return _mapper;
    }
}