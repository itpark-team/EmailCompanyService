namespace ClassLibraryCommunicationEntities.SocketsEntities;

public class ClientRequest
{
    public string Command { get; set; }
    public string JsonData { get; set; }

    public ClientRequest(string command, string jsonData)
    {
        Command = command;
        JsonData = jsonData;
    }
}