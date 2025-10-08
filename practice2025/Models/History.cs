using System;
using System.Collections.Generic;

namespace practice2025.Models;

public partial class History
{
    public DateOnly HistoryDate { get; set; }

    public TimeOnly HistoryTime { get; set; }

    public long HistoryClient { get; set; }

    public long HistoryDiagnosis { get; set; }

    public bool HistoryStatus { get; set; }

    public virtual Client HistoryClientNavigation { get; set; } = null!;

    public virtual Diagnosis HistoryDiagnosisNavigation { get; set; } = null!;
}
