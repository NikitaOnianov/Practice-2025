using System;
using System.Collections.Generic;

namespace practice2025.Models;

public partial class Client
{
    public long ClientId { get; set; }

    public string ClientName { get; set; } = null!;

    public string ClientSurname { get; set; } = null!;

    public string? ClientPatronymic { get; set; }

    public string? ClientSnils { get; set; }

    public string? ClientPassport { get; set; }

    public DateOnly ClientBirthday { get; set; }

    public string? ClientPolis { get; set; }

    public bool ClientIsMan { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();
}
