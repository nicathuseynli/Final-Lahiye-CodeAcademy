using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Contact;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class ContactDetailsController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<ContactDetailsController> _logger;
    private readonly IContactDetailsService _contactDetailsService;

    public ContactDetailsController(AppDbContext context, ILogger<ContactDetailsController> logger, IWebHostEnvironment webHostEnvironment, IContactDetailsService contactDetailsService)
    {
        _context = context;
        _logger = logger;
        _contactDetailsService = contactDetailsService;
    }

    public async Task<IActionResult> Index()
    {
        var details = await _context.ContactDetailss.ToListAsync();
        return View(details);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateContactDetailsVM createContactDetailsVM)
    {
        if (!ModelState.IsValid) return View();
        await _contactDetailsService.CreateAsync(createContactDetailsVM);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var details = await _contactDetailsService.GetByIdAsync(id);
        return View(details);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var details = await _context.ContactDetailss.FirstOrDefaultAsync(x => x.Id == id);
        if (details == null) return View();

        _context.ContactDetailss.Remove(details);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var details = await _context.ContactDetailss.FirstOrDefaultAsync(x => x.Id == id);
        if (details == null) return NotFound();

        var updateContactDetailsVM = new UpdateContactDetailsVM()
        {
            Id = details.Id,
            ByAddress = details.ByAddress,
            ByEmail = details.ByEmail,
            ByPhone = details.ByPhone,
        };
        return View(updateContactDetailsVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateContactDetailsVM updateContactDetailsVM)
    {
        var result = await _contactDetailsService.UpdateAsync(updateContactDetailsVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
