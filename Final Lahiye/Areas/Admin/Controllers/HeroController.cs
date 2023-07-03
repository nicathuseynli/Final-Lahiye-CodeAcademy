using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class HeroController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<HeaderUpController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public HeroController(AppDbContext context, ILogger<HeaderUpController> logger, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }

    public async  Task<IActionResult> Index()
    {
        var homeHero = await _context.Heros.ToListAsync();
        return View(homeHero);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateHeroVM createHeroVM)
    {
        if (!ModelState.IsValid) return View();

        if (!createHeroVM.HeroPhoto.ContentType.Contains("image/")) return View();

        if (createHeroVM.HeroPhoto.Length / 1024 > 500) return View();

        string filename = Guid.NewGuid().ToString() + "_" + createHeroVM.HeroPhoto.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath,"images",filename);

        using FileStream stream = new FileStream (path, FileMode.Create);

        await createHeroVM.HeroPhoto.CopyToAsync(stream);

        Hero hero = new()
        {
            Title = createHeroVM.Title,
            Description = createHeroVM.Description,
            HeroImage = filename
        };
        await _context.Heros.AddAsync(hero);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var hero = await _context.Heros.FirstOrDefaultAsync(x => x.Id == id);
        if (hero == null)
            return NotFound();
        return View(hero);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var hero = await _context.Heros.FirstOrDefaultAsync(x => x.Id == id);
        if (hero == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", hero.HeroImage);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.Heros.Remove(hero);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var hero = await _context.Heros.FirstOrDefaultAsync(x => x.Id == id);
        if (hero == null) return NotFound();

        var updateBlog = new UpdateHeroVM()
        {
            Id = hero.Id,
            Description = hero.Description,
            Title = hero.Title,
            Image = hero.HeroImage,
        };
        return View(updateBlog);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateHeroVM updateHeroVM)
    {

        var hero = await _context.Heros.FirstOrDefaultAsync(x => x.Id == updateHeroVM.Id);
        if (hero == null) return NotFound();

        if (updateHeroVM.HeroPhoto != null)
        {
            #region Create NewImage
            if (!updateHeroVM.HeroPhoto.ContentType.Contains("image/"))
                return View();

            if (updateHeroVM.HeroPhoto.Length / 1024 > 1000)
                return View();

            string filename = Guid.NewGuid().ToString() + " _ " + updateHeroVM.HeroPhoto.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateHeroVM.HeroPhoto.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", hero.HeroImage);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            hero.HeroImage = filename;
            #endregion
        }
        hero.Description = updateHeroVM.Description;
        hero.Title = updateHeroVM.Title;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
