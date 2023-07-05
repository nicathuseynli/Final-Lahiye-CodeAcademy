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
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ContactController(AppDbContext context, ILogger<ContactController> logger, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
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

        string filename = Guid.NewGuid().ToString() + "_" + createContactVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createContactVM.Photo.CopyToAsync(stream);

        Contact contact = new()
        {
            ByAddress = createContactVM.ByAddress,
            ByEmail = createContactVM.ByEmail,
            ByPhone = createContactVM.ByPhone,
            Magazine = createContactVM.Magazine,
            City = createContactVM.City,
            Description = createContactVM.Description,
            Image = filename
        };
        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        if (contact == null)
            return NotFound();
        return View(contact);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        if (contact == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", contact.Image);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
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
            ByAddress = contact.ByAddress,
            ByEmail = contact.ByEmail,
            ByPhone = contact.ByPhone,
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

        var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == updateContactVM.Id);
        if (contact == null) return NotFound();

        if (updateContactVM.Photo != null)
        {
            #region Create NewImage
            if (!updateContactVM.Photo.ContentType.Contains("image/"))
                return View();

            if (updateContactVM.Photo.Length / 1024 > 1000)
                return View();

            string filename = Guid.NewGuid().ToString() + " _ " + updateContactVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateContactVM.Photo.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", contact.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            contact.Image = filename;
            #endregion
        }
        contact.ByAddress = updateContactVM.ByAddress;
        contact.ByEmail = updateContactVM.ByEmail;
        contact.ByPhone = updateContactVM.ByPhone;
        contact.Magazine = updateContactVM.Magazine;
        contact.City = updateContactVM.City;
        contact.Description = updateContactVM.Description;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
