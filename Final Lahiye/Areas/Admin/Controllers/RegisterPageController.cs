using Final_Lahiye.Areas.Admin.ViewModels.RegisterPage;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class RegisterPageController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<RegisterPageController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public RegisterPageController(AppDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<RegisterPageController> logger)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
    }
    public async Task<IActionResult> Index()
    {
        var registerPage = await _context.RegisterPages.ToListAsync();
        return View(registerPage);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateRegisterPageVM createRegisterPageVM)
    {
        if (!ModelState.IsValid) return View();

        if (!createRegisterPageVM.Photo.ContentType.Contains("image/")) return View();

        if (createRegisterPageVM.Photo.Length / 1024 > 500) return View();

        string filename = Guid.NewGuid().ToString() + "_" + createRegisterPageVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createRegisterPageVM.Photo.CopyToAsync(stream);

        RegisterPage registerPage = new()
        {
            Description = createRegisterPageVM.Description,
            Image = filename
        };
        await _context.RegisterPages.AddAsync(registerPage);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var registerPage = await _context.RegisterPages.FirstOrDefaultAsync(x => x.Id == id);
        if (registerPage == null)
            return NotFound();
        return View(registerPage);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var registerPage = await _context.RegisterPages.FirstOrDefaultAsync(x => x.Id == id);
        if (registerPage == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", registerPage.Image);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.RegisterPages.Remove(registerPage);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var registerPage = await _context.RegisterPages.FirstOrDefaultAsync(x => x.Id == id);
        if (registerPage == null) return NotFound();

        var updateRegisterVM = new UpdateRegisterPageVM()
        {
            Id = registerPage.Id,
            Description = registerPage.Description,
            Image = registerPage.Image,
        };
        return View(updateRegisterVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateRegisterPageVM updateRegisterVM)
    {

        var registerPage = await _context.RegisterPages.FirstOrDefaultAsync(x => x.Id == updateRegisterVM.Id);
        if (registerPage == null) return NotFound();

        if (updateRegisterVM.Photo != null)
        {
            #region Create NewImage
            if (!updateRegisterVM.Photo.ContentType.Contains("image/"))
                return View();

            if (updateRegisterVM.Photo.Length / 1024 > 1000)
                return View();

            string filename = Guid.NewGuid().ToString() + " _ " + updateRegisterVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateRegisterVM.Photo.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", registerPage.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            registerPage.Image = filename;
            #endregion
        }
        registerPage.Description = updateRegisterVM.Description;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
