using Final_Lahiye.Areas.Admin.ViewModels.LoginPage;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class LoginPageController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<LoginPageController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public LoginPageController(AppDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<LoginPageController> logger)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
    }
    public async Task<IActionResult> Index()
    {
        var loginPage = await _context.LoginPages.ToListAsync();
        return View(loginPage);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateLoginPageVM createLoginVM)
    {
        if (!ModelState.IsValid) return View();

        if (!createLoginVM.Photo.ContentType.Contains("image/")) return View();

        if (createLoginVM.Photo.Length / 1024 > 500) return View();

        string filename = Guid.NewGuid().ToString() + "_" + createLoginVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createLoginVM.Photo.CopyToAsync(stream);

        LoginPage loginPage = new()
        {
            Description = createLoginVM.Description,
            Image = filename
        };
        await _context.LoginPages.AddAsync(loginPage);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var loginpage = await _context.LoginPages.FirstOrDefaultAsync(x => x.Id == id);
        if (loginpage == null)
            return NotFound();
        return View(loginpage);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var loginpage = await _context.LoginPages.FirstOrDefaultAsync(x => x.Id == id);
        if (loginpage == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", loginpage.Image);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.LoginPages.Remove(loginpage);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var loginpage = await _context.LoginPages.FirstOrDefaultAsync(x => x.Id == id);
        if (loginpage == null) return NotFound();

        var updateLoginVM = new UpdateLoginPageVM()
        {
            Id = loginpage.Id,
            Description = loginpage.Description,
            Image = loginpage.Image,
        };
        return View(updateLoginVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateLoginPageVM updateLoginVM)
    {

        var loginpage = await _context.LoginPages.FirstOrDefaultAsync(x => x.Id == updateLoginVM.Id);
        if (loginpage == null) return NotFound();

        if (updateLoginVM.Photo != null)
        {
            #region Create NewImage
            if (!updateLoginVM.Photo.ContentType.Contains("image/"))
                return View();

            if (updateLoginVM.Photo.Length / 1024 > 1000)
                return View();

            string filename = Guid.NewGuid().ToString() + " _ " + updateLoginVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateLoginVM.Photo.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", loginpage.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            loginpage.Image = filename;
            #endregion
        }
        loginpage.Description = updateLoginVM.Description;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
