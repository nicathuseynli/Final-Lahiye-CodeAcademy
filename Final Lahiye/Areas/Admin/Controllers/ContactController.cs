using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Contact;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class ContactController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<ContactController> _logger;
    private readonly IContactService _contactService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ContactController(AppDbContext context, ILogger<ContactController> logger, IWebHostEnvironment webHostEnvironment, IContactService contactService)
    {
        _context = context;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
        _contactService = contactService;
    }

    public async Task<IActionResult> Index()
    {
        var contact = await _context.Contacts.ToListAsync();
        return View(contact);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateContactVM createContactVM)
    {
        if (!ModelState.IsValid) return View();

        if (!createContactVM.Photo.ContentType.Contains("image/"))  return View();

        if (createContactVM.Photo.Length / 1024 > 500)  return View();

        await _contactService.CreateAsync(createContactVM);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var contact = await _contactService.GetByIdAsync(id);
        return View(contact);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var delete = _contactService.DeleteAsync(id);
        if (delete == null) return View();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        if (contact == null) return NotFound();

        var updateContactVM = new UpdateContactVM()
        {
            Id = contact.Id,
            Magazine = contact.Magazine,
            City = contact.City,
            Description = contact.Description,
            Image = contact.Image,
        };
        return View(updateContactVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateContactVM updateContactVM)
    {
        var result = await _contactService.UpdateAsync(updateContactVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
