using System;
using System.Collections.Generic;

namespace practice2025.Models;

public partial class UsersType
{
    public int UserTypeId { get; set; }

    public string UserTypeName { get; set; } = null!;

    public int? UsersTypeMedicalDepartments { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual MedicalDepartment? UsersTypeMedicalDepartmentsNavigation { get; set; }
}
