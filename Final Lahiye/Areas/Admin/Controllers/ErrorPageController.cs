using Final_Lahiye.Areas.Admin.ViewModels.Error;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class ErrorPageController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<ErrorPageController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ErrorPageController(AppDbContext context, ILogger<ErrorPageController> logger, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var errorPage = await _context.ErrorPages.ToListAsync();
        return View(errorPage);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateErrorPageVM createErrorPageVM)
    {
        if (!ModelState.IsValid) return View();

        if (!createErrorPageVM.Photo.ContentType.Contains("image/")) return View();

        if (createErrorPageVM.Photo.Length / 1024 > 500) return View();

        string filename = Guid.NewGuid().ToString() + "_" + createErrorPageVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createErrorPageVM.Photo.CopyToAsync(stream);

        ErrorPage errorPage = new()
        {
            Message = createErrorPageVM.Message,
            Image = filename
        };
        await _context.ErrorPages.AddAsync(errorPage);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var errorPage = await _context.ErrorPages.FirstOrDefaultAsync(x => x.Id == id);
        if (errorPage == null)
            return NotFound();
        return View(errorPage);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var errorPage = await _context.ErrorPages.FirstOrDefaultAsync(x => x.Id == id);
        if (errorPage == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", errorPage.Image);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.ErrorPages.Remove(errorPage);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var errorPage = await _context.ErrorPages.FirstOrDefaultAsync(x => x.Id == id);
        if (errorPage == null) return NotFound();

        var updateErrorPageVM = new UpdateErrorPageVM()
        {
            Id = errorPage.Id,
            Message = errorPage.Message,
            Image = errorPage.Image,
        };
        return View(updateErrorPageVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateErrorPageVM updateErrorPageVM)
    {

        var errorPage = await _context.ErrorPages.FirstOrDefaultAsync(x => x.Id == updateErrorPageVM.Id);
        if (errorPage == null) return NotFound();

        if (updateErrorPageVM.Photo != null)
        {
            #region Create NewImage
            if (!updateErrorPageVM.Photo.ContentType.Contains("image/"))
                return View();

            if (updateErrorPageVM.Photo.Length / 1024 > 1000)
                return View();

            string filename = Guid.NewGuid().ToString() + " _ " + updateErrorPageVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateErrorPageVM.Photo.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", errorPage.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            errorPage.Image = filename;
            #endregion
        }
        errorPage.Message = updateErrorPageVM.Message;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
