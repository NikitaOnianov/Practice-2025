using System;
using System.Collections.Generic;

namespace practice2025.Models;

public partial class User
{
    public long UserId { get; set; }

    public string UserName { get; set; } = null!;

    public int UserType { get; set; }

    public string UserLogin { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public virtual UsersType UserTypeNavigation { get; set; } = null!;
}
