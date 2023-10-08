namespace ClassLibraryCommunicationEntities.SocketsEntities;

public class ServerResponse
{
    public int Status { get; set; }
    public string JsonData { get; set; }

    public ServerResponse()
    {
        
    }
    
    public ServerResponse(int status)
    
    {
        Status = status;
        JsonData = String.Empty;
    }

    public ServerResponse(int status, string jsonData)
    {
        Status = status;
        JsonData = jsonData;
    }
}