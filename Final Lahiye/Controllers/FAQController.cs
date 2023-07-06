using Final_Lahiye.Data;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Controllers;
public class FAQController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<FAQController> _logger;

    public FAQController(ILogger<FAQController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var question = await _context.FaqPages.ToListAsync();
        FAQVM faqVM = new()
        {
           Faqs = question, 
        };
        return View(faqVM);
    }
}
