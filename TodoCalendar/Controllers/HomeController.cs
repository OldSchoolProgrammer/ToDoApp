using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoCalendar.Data;
using TodoCalendar.Models;
using TodoCalendar.ViewModels;

namespace TodoCalendar.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var now = DateTime.Now;
        var tasks = await _context.Tasks.Include(t => t.Category).ToListAsync();

        var viewModel = new DashboardViewModel
        {
            TotalTasks = tasks.Count,
            CompletedTasks = tasks.Count(t => t.IsCompleted),
            PendingTasks = tasks.Count(t => !t.IsCompleted),
            OverdueTasks = tasks.Count(t => !t.IsCompleted && t.DueDate.HasValue && t.DueDate < now),
            
            UpcomingTasks = tasks
                .Where(t => !t.IsCompleted && t.DueDate.HasValue && t.DueDate >= now)
                .OrderBy(t => t.DueDate)
                .Take(5)
                .Select(t => new TaskViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    IsCompleted = t.IsCompleted,
                    CategoryId = t.CategoryId,
                    CategoryName = t.Category?.Name,
                    CategoryColor = t.Category?.Color,
                    Tags = t.Tags,
                    CreatedDate = t.CreatedDate,
                    CompletedDate = t.CompletedDate
                })
                .ToList(),

            HighPriorityTasks = tasks
                .Where(t => !t.IsCompleted && t.Priority == 3)
                .OrderBy(t => t.DueDate)
                .Take(5)
                .Select(t => new TaskViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    IsCompleted = t.IsCompleted,
                    CategoryId = t.CategoryId,
                    CategoryName = t.Category?.Name,
                    CategoryColor = t.Category?.Color,
                    Tags = t.Tags,
                    CreatedDate = t.CreatedDate,
                    CompletedDate = t.CompletedDate
                })
                .ToList(),

            RecentlyCompletedTasks = tasks
                .Where(t => t.IsCompleted)
                .OrderByDescending(t => t.CompletedDate)
                .Take(5)
                .Select(t => new TaskViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    IsCompleted = t.IsCompleted,
                    CategoryId = t.CategoryId,
                    CategoryName = t.Category?.Name,
                    CategoryColor = t.Category?.Color,
                    Tags = t.Tags,
                    CreatedDate = t.CreatedDate,
                    CompletedDate = t.CompletedDate
                })
                .ToList()
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
