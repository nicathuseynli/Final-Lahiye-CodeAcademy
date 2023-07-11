using Final_Lahiye.Areas.Admin.Services.Interface;
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
    private readonly IBlogService _blogService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public BlogController(AppDbContext context, IWebHostEnvironment webHostEnvironment, ILogger<BlogController> logger, IBlogService blogService)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
        _blogService = blogService;
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
        if (!ModelState.IsValid) return View();

        if (!createBlogPageVM.Photo.ContentType.Contains("image/") &&
            !createBlogPageVM.ApplyInfoLeftPhoto.ContentType.Contains("image/") &&
            !createBlogPageVM.ApplyInfoRightPhoto.ContentType.Contains("image/"))
            return View();

        if (createBlogPageVM.Photo.Length / 1024 > 1000 && 
            createBlogPageVM.ApplyInfoLeftPhoto.Length / 1204>1000 && 
            createBlogPageVM.ApplyInfoRightPhoto.Length/ 1024>1000)
            return View();

        await _blogService.CreateAsync(createBlogPageVM);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var details = await _blogService.GetByIdAsync(id);
        return View(details);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var delete = _blogService.DeleteAsync(id);
        if (delete == null) return View();
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

            HeaderUp = blog.HeaderUp,

            InformationUp = blog.InformationUp,

            HeaderMiddle = blog.HeaderMiddle,

            InformationMiddle = blog.InformationMiddle,

            MiddleTextFirst = blog.MiddleTextFirst,

            MiddleTextSecond = blog.MiddleTextSecond,

            MiddleTextThird = blog.MiddleTextThird,

            MiddleTextFourth = blog.MiddleTextFourth,

            MiddleDescription = blog.MiddleDescription,

            HeaderEnd = blog.HeaderEnd,

            InformationEnd = blog.InformationEnd,

            ServiceInfo = blog.ServiceInfo,

            ServiceNameFirst = blog.ServiceNameFirst,

            ServicePriceFirst = blog.ServicePriceFirst,

            ServiceDescriptionFirst = blog.ServiceDescriptionFirst,

            ServiceNameSecond = blog.ServiceNameSecond,

            ServicePriceSecond = blog.ServicePriceSecond,

            ServiceDescriptionSecond = blog.ServiceDescriptionSecond,

            ServiceNameThird = blog.ServiceNameThird,

            ServicePriceThird = blog.ServicePriceThird,

            ServiceDescriptionThird = blog.ServiceDescriptionThird,

            ServiceNameFourth = blog.ServiceNameFourth,

            ServicePriceFourth = blog.ServicePriceFourth,

            ServiceDescriptionFourth = blog.ServiceDescriptionFourth,

            ApplyInfo = blog.ApplyInfo,
            Title = blog.Title,
        };
        return View(updateBlogPageVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateBlogPageVM updateBlogPageVM)
    {
        ViewBag.Author = new SelectList(await _context.Authors.ToListAsync(), "Id", "FullName", "Proffession");

        var result = await _blogService.UpdateAsync(updateBlogPageVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
