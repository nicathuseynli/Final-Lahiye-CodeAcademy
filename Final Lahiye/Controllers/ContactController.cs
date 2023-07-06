using Final_Lahiye.Data;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Controllers;
public class ContactController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<ContactController> _logger;

    public ContactController(AppDbContext context, ILogger<ContactController> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task<IActionResult> Index()
    {
        var contacts = await _context.Contacts.ToListAsync();
        var details = await _context.ContactDetailss.FirstOrDefaultAsync();

        ContactVM contactVM = new()
        {
            Contacts = contacts,
            ContactDetails = details,
        };
        return View(contactVM);
    }
}
