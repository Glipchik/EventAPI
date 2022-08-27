namespace EventAPI.UI.Models
{
    public class UpdateEventModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Organizer { get; set; }
        public string Speaker { get; set; }
        public DateTime EventTime { get; set; }
        public string EventPlace { get; set; }
    }
}
