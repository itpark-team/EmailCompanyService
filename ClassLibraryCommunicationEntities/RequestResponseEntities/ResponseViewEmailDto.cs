namespace ClassLibraryCommunicationEntities.RequestResponseEntities;

public class ResponseViewEmailDto
{
    public string Message { get; set; }

    public string Title { get; set; }

    public bool IsOpened { get; set; }

    public DateTime CreatedDatetime { get; set; }

    public override string ToString()
    {
        return $"{CreatedDatetime.ToString()}: {Title} \n {Message}";
    }
}