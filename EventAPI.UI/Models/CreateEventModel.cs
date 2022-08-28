namespace EventAPI.UI.Models
{
    public class CreateEventModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Plan { get; set; } = string.Empty;
        public string Organizer { get; set; } = string.Empty;
        public string Speaker { get; set; } = string.Empty;
        public DateTime EventTime { get; set; }
        public string EventPlace { get; set; } = string.Empty;
    }
}
