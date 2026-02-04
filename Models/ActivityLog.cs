namespace jobsearch.Models
{
    public class ActivityLog
    {
        public int Id { get; set; }


        // Stored as TEXT: "YYYY-MM-DD"
        public string ActivityDate { get; set; } = string.Empty;

        public string BusinessOrOrganization { get; set; } = string.Empty;

        public string ActivityPerformed { get; set; } = string.Empty;

        public string TypeOfWork { get; set; } = string.Empty;

        public string Results { get; set; } = string.Empty;

        public string? ContactPerson { get; set; }

        public string? HowPerformed { get; set; }

        public string? Notes { get; set; }

        public int ShouldBeOnDes { get; set; }

        public int IsOnDes { get; set; }
    }
}
