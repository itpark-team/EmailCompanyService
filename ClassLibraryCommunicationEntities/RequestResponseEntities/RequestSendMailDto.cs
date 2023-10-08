namespace ClassLibraryCommunicationEntities.RequestResponseEntities;

public class RequestSendMailDto
{
    public int IdFrom { get; set; }
    public string EmailTo { get; set; }
    
    public string Message { get; set; }

    public string Title { get; set; }
}