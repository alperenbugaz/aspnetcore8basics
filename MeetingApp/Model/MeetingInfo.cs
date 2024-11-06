namespace MeetingApp.Model
{
    public class MeetingInfo
    {   
        public int Id { get; set; }

        public string? Location { get; set; }

        public DateTime? Date { get; set; }

        public int NumberOfParticipants { get; set; }
    }
}