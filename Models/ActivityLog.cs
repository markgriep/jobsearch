using System;

namespace jobsearch.Models
{
    public class ActivityLog
    {
        public int Id { get; set; }

        public DateTime ActivityDate { get; set; } = DateTime.Today;

        public string BusinessOrOrganization { get; set; } = string.Empty;

        public string ActivityPerformed { get; set; } = string.Empty;

        public string TypeOfWork { get; set; } = string.Empty;

        public string Results { get; set; } = string.Empty;

        public string? ContactPerson { get; set; }

        public string? HowPerformed { get; set; }

        public string? Notes { get; set; }

        public bool ShouldBeOnDes { get; set; }

        public bool IsOnDes { get; set; }
    }
}
