using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice2025.Models
{
    public partial class patient_turnoutDTO
    {
        public DateOnly HistoryDate { get; set; }

        public TimeOnly HistoryTime { get; set; }

        public long HistoryClient { get; set; }

        public string Client { get; set; }

        public long HistoryDiagnosis { get; set; }

        public string Diagnosis { get; set; }

        // Оригинальное свойство из базы данных
        public bool? DrivingTrainingPresence { get; set; }

        // Свойство для привязки к CheckBox
        public bool IsPresent
        {
            get => DrivingTrainingPresence ?? false;
            set => DrivingTrainingPresence = value;
        }
    }
}
