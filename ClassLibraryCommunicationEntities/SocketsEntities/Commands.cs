namespace ClassLibraryCommunicationEntities.SocketsEntities;

public class Commands
{
    public static string GetNotOpenedMails { get; } = "GetNotOpenedMails";
    public static string SendMail { get; } = "SendMail";
    public static string RegisterNewUser { get; } = "RegisterNewUser";
}