using Final_Lahiye.Areas.Admin.ViewModels.Blog;
using Final_Lahiye.Areas.Services.Interface;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]

public class AuthorController : Controller
{
    private readonly AppDbContext _context;
    private readonly IAuthorService _authorService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AuthorController(AppDbContext context, IWebHostEnvironment webHostEnvironment, IAuthorService authorService)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _authorService = authorService;
    }

    public async Task<IActionResult> Index()
    {
        var category = await _context.Authors.ToListAsync();
        return View(category);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAuthorVM createAuthorVM)
    {
        if (!ModelState.IsValid) return View();

        if (!createAuthorVM.Photo.ContentType.Contains("image/")) return View();

        if (createAuthorVM.Photo.Length / 1024 > 500) return View();

        await _authorService.CreateAsync(createAuthorVM);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var details = await _authorService.GetByIdAsync(id);
        return View(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
        if (author == null) return 
                View();
        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", author.Image);
        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
        if (author == null) return NotFound();

        var updateAuthorVM = new UpdateAuthorVM()
        {
            Id = author.Id,
            FullName = author.FullName,
            Proffession = author.Proffesion,
            Blogid = author.BlogId,
            Image = author.Image,
        };
        return View(updateAuthorVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateAuthorVM updateAuthorVM)
    {
        var result = await _authorService.UpdateAsync(updateAuthorVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
