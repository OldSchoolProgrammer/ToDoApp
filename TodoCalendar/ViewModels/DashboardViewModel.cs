namespace TodoCalendar.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public int OverdueTasks { get; set; }
        public List<TaskViewModel> UpcomingTasks { get; set; } = new();
        public List<TaskViewModel> HighPriorityTasks { get; set; } = new();
        public List<TaskViewModel> RecentlyCompletedTasks { get; set; } = new();
    }
}
