using System;
using System.Collections.Generic;

namespace practice2025.Models;

public partial class Diagnosis
{
    public long DiagnosisId { get; set; }

    public string DiagnosisName { get; set; } = null!;

    public int DiagnosisMedicalDepartment { get; set; }

    public virtual MedicalDepartment DiagnosisMedicalDepartmentNavigation { get; set; } = null!;

    public virtual ICollection<History> Histories { get; set; } = new List<History>();
}
