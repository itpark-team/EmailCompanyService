namespace ClassLibraryCommunicationEntities.RequestResponseEntities;

public class RequestRegisterUserDto
{
    public string Login { get; set; }

    public string Password { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }
}