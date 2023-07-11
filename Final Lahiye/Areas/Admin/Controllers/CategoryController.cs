using Final_Lahiye.Areas.Admin.Services.Interface;
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
    private readonly ICategoryService _categoryService;

    public CategoryController(AppDbContext context, ILogger<CategoryController> logger, ICategoryService categoryService)
    {
        _context = context;
        _logger = logger;
        _categoryService = categoryService;
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
        await _categoryService.CreateAsync(createCategoryVM);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
      var category = await _categoryService.GetByIdAsync(id);
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var delete = _categoryService.DeleteAsync(id);
        if (delete == null) return View();
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
        var result = await _categoryService.UpdateAsync(updateCategoryVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
