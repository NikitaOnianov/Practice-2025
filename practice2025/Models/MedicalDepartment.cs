using System;
using System.Collections.Generic;

namespace practice2025.Models;

public partial class MedicalDepartment
{
    public int MedicalDepartmentId { get; set; }

    public string MedicalDepartmentName { get; set; } = null!;

    public int? MedicalDepartmentNumberOfSeats { get; set; }

    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

    public virtual ICollection<UsersType> UsersTypes { get; set; } = new List<UsersType>();
}
