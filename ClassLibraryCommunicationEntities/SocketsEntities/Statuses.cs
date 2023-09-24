namespace ClassLibraryCommunicationEntities.SocketsEntities;

public class Statuses
{
    public static int Ok { get; } = 200;
    public static int UnknownCommand { get; } = 400;
    public static int ServerError { get; } = 500;
}