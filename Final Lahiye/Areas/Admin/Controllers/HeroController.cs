using Final_Lahiye.Areas.Admin.Services.Interface;
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
    private readonly ILogger<BioController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHeroService _heroService;
    public HeroController(AppDbContext context, ILogger<BioController> logger, IWebHostEnvironment webHostEnvironment, IHeroService heroService)
    {
        _context = context;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
        _heroService = heroService;
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

        await _heroService.CreateAsync(createHeroVM);

        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var delete = await _heroService.GetByIdAsync(id);
        return View(delete);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var hero = await _context.Heros.FirstOrDefaultAsync(x => x.Id == id);
        if (hero == null) return View();

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
        var result = await _heroService.UpdateAsync(updateHeroVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
