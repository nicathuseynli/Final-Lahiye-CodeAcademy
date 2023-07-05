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

        string filename = Guid.NewGuid().ToString() + "_" + createAuthorVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createAuthorVM.Photo.CopyToAsync(stream);
        Author author = new()
        {
            FullName = createAuthorVM.FullName,
            Proffesion = createAuthorVM.Proffession,
            BlogId = createAuthorVM.Blogid,
            Image = filename,
        };
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
        if (author == null)
            return View();
        return View(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
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
        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == updateAuthorVM.Id);
        if (author == null)
            return View();

        if (updateAuthorVM.Photo != null)
        {
            #region Create NewImage
            if (!updateAuthorVM.Photo.ContentType.Contains("image/"))
                return View();

            if (updateAuthorVM.Photo.Length / 1024 > 1000)
                return View();

            string filename = Guid.NewGuid().ToString() + " _ " + updateAuthorVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateAuthorVM.Photo.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", author.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            author.Image = filename;
            #endregion
        }
        author.FullName = updateAuthorVM.FullName;
        author.Proffesion = updateAuthorVM.Proffession;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
