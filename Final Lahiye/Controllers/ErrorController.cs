using Final_Lahiye.Data;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Controllers;
public class ErrorController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var errorPage = await _context.ErrorPages.FirstOrDefaultAsync();
        ErrorVM errorVM = new()
        {
            ErrorPages = errorPage,
        };
        return View(errorVM);
    }
}
