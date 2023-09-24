using System;
using System.Collections.Generic;

namespace ConsoleAppServerSide.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public virtual ICollection<Mail> MailIdFromNavigations { get; set; } = new List<Mail>();

    public virtual ICollection<Mail> MailIdToNavigations { get; set; } = new List<Mail>();
}
