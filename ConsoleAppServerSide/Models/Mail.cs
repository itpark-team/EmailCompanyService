using System;
using System.Collections.Generic;

namespace ConsoleAppServerSide.Models;

public partial class Mail
{
    public int Id { get; set; }

    public int IdFrom { get; set; }

    public int IdTo { get; set; }

    public string Message { get; set; }

    public string Title { get; set; }

    public bool IsOpened { get; set; }

    public DateTime CreatedDatetime { get; set; }

    public virtual User IdFromNavigation { get; set; }

    public virtual User IdToNavigation { get; set; }
}
