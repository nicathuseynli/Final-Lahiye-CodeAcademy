using Final_Lahiye.Areas.Admin.ViewModels.Blog;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class BlogController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<BlogController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public BlogController(AppDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<BlogController> logger)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
    }
    public async Task<IActionResult> Index()
    {
        var blog = await _context.Blogs.ToListAsync();
        return View(blog);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Author = new SelectList(await _context.Authors.ToListAsync(), "Id", "FullName","Proffession");
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBlogPageVM createBlogPageVM)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Author = new SelectList(await _context.Authors.ToListAsync(), "Id", "FullName", "Proffession");
            return View();
        }

        if (!createBlogPageVM.Photo.ContentType.Contains("image/"))
            return View();

        if (createBlogPageVM.Photo.Length / 1024 > 500)
            return View();
        string filename = Guid.NewGuid().ToString() + "_" + createBlogPageVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createBlogPageVM.Photo.CopyToAsync(stream);

        Blog blog = new()
        {
            Title = createBlogPageVM.Title,
            AuthorName = createBlogPageVM.AuthorName,
            Image = filename
        };
        await _context.Blogs.AddAsync(blog);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id);
        if (blog == null)
            return NotFound();
        return View(blog);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id);
        if (blog == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", blog.Image);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        ViewBag.Author = new SelectList(await _context.Authors.ToListAsync(),"Id" , "FullName" ,"Proffession");
        var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id);
        if (blog == null) return NotFound();

        var updateBlogPageVM = new UpdateBlogPageVM()
        {
            Id = blog.Id,
            AuthorId = blog.AuthorId,
            Title = blog.Title,
            AuthorName = blog.AuthorName,
        };
        return View(updateBlogPageVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateBlogPageVM updateBlogPageVM)
    {
        ViewBag.Author = new SelectList(await _context.Authors.ToListAsync(), "Id", "FullName", "Proffession");

        var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == updateBlogPageVM.Id);
        if (blog == null) return NotFound();

        if (updateBlogPageVM.Photo != null)
        {
            #region Create NewImage
            if (!updateBlogPageVM.Photo.ContentType.Contains("image/"))
                return View();

            if (updateBlogPageVM.Photo.Length / 1024 > 1000)
                return View();

            string filename = Guid.NewGuid().ToString() + " _ " + updateBlogPageVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateBlogPageVM.Photo.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", blog.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            blog.Image = filename;
            #endregion
        }
        blog.Title = updateBlogPageVM.Title;
        blog.AuthorName = updateBlogPageVM.AuthorName;
        blog.AuthorId = updateBlogPageVM.AuthorId;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
