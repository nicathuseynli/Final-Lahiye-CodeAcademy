using Final_Lahiye.Areas.Admin.ViewModels.ShopPage;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class CategoryController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(AppDbContext context, ILogger<CategoryController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var category = await _context.Categories.ToListAsync();
        return View(category);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCategoryVM createCategoryVM)
    {
        Category category = new()
        {
            Name = createCategoryVM.Name,
        };
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
            return NotFound();
        return View(category);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null) return View();

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null) return NotFound();

        var updateCategoryVM = new UpdateCategoryVM()
        {
            Id = category.Id,
            Name = category.Name,
        };
        return View(updateCategoryVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateCategoryVM updateCategoryVM)
    {

        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == updateCategoryVM.Id);
        if (category == null) return NotFound();

        category.Name = updateCategoryVM.Name;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
