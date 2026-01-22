namespace TodoCalendar.ViewModels
{
    public class CalendarEventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime? Start { get; set; }
        public string? Color { get; set; }
        public bool AllDay { get; set; } = true;
        public string? Url { get; set; }
    }
}
