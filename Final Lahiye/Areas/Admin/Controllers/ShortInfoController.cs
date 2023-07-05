using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class ShortInfoController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<ShortInfoController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ShortInfoController(AppDbContext context, ILogger<ShortInfoController> logger, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var shortInfo = await _context.ShortInformations.ToListAsync();
        return View(shortInfo);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateShortInfoVM createshortInfoVM)
    {
        if (!ModelState.IsValid) return View();

        if (!createshortInfoVM.IconPhoto.ContentType.Contains("image/")) return View();

        if (createshortInfoVM.IconPhoto.Length / 1024 > 500) return View();

        string filename = Guid.NewGuid().ToString() + "_" + createshortInfoVM.IconPhoto.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createshortInfoVM.IconPhoto.CopyToAsync(stream);

        ShortInformation shortInfo = new()
        {
            Title = createshortInfoVM.Title,
            Information = createshortInfoVM.Information,
            Icon = filename
        };
        await _context.ShortInformations.AddAsync(shortInfo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var shortInfo = await _context.ShortInformations.FirstOrDefaultAsync(x => x.Id == id);
        if (shortInfo == null)
            return NotFound();
        return View(shortInfo);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var shortInfo = await _context.ShortInformations.FirstOrDefaultAsync(x => x.Id == id);
        if (shortInfo == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", shortInfo.Icon);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.ShortInformations.Remove(shortInfo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var shortInfo = await _context.ShortInformations.FirstOrDefaultAsync(x => x.Id == id);
        if (shortInfo == null) return NotFound();

        var updateshortInfoVM = new UpdateShortInfoVM()
        {
            Id = shortInfo.Id,
            Title = shortInfo.Title,
            Information = shortInfo.Information,
            Icon = shortInfo.Icon,
        };
        return View(updateshortInfoVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateShortInfoVM updateshortInfoVM)
    {

        var shortInfo = await _context.ShortInformations.FirstOrDefaultAsync(x => x.Id == updateshortInfoVM.Id);
        if (shortInfo == null) return NotFound();

        if (updateshortInfoVM.IconPhoto != null)
        {
            #region Create NewImage
            if (!updateshortInfoVM.IconPhoto.ContentType.Contains("image/"))
                return View();

            if (updateshortInfoVM.IconPhoto.Length / 1024 > 1000)
                return View();

            string filename = Guid.NewGuid().ToString() + " _ " + updateshortInfoVM.IconPhoto.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateshortInfoVM.IconPhoto.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", shortInfo.Icon);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            shortInfo.Icon = filename;
            #endregion
        }
        shortInfo.Information = updateshortInfoVM.Information;
        shortInfo.Title = updateshortInfoVM.Title;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
