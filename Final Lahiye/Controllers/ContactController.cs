using Final_Lahiye.Data;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Controllers;
public class ContactController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public ContactController(AppDbContext context)
    {
        _context = context;
    }

    public ContactController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync();

        ContactVM contactVM = new()
        {
            Contact = contact,
        };
        return View(contactVM);
    }
}
