namespace ClassLibraryCommunicationEntities.SocketsEntities;

public class Commands
{
    public static string GetNotOpenedMails { get; } = "GetNotOpenedMails";
    public static string SendMail { get; } = "SendMail";
    public static string RegisterNewUser { get; } = "RegisterNewUser";
    public static string AuthExistUser { get; } = "RegisterNewUser";
    public static string GetIsNotOpenedEmails { get; } = "GetIsNotOpenedEmails";
    public static string GetAllEmails { get; } = "GetAllEmails";
}