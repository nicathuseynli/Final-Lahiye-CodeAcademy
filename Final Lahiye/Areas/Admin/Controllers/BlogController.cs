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

        string filename = Guid.NewGuid().ToString() + "_" + createBlogPageVM.Photo.FileName;
        string applyleft = Guid.NewGuid().ToString() + "_" + createBlogPageVM.ApplyInfoLeftPhoto.FileName;
        string applyright = Guid.NewGuid().ToString() + "_" + createBlogPageVM.ApplyInfoRightPhoto.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);
        string applyleftpath = Path.Combine(_webHostEnvironment.WebRootPath, "images", applyleft);
        string applyrightpath = Path.Combine(_webHostEnvironment.WebRootPath, "images", applyright);
       
        using FileStream stream = new FileStream(path, FileMode.Create);
        using FileStream applyleftstream = new FileStream(applyleftpath, FileMode.Create);
        using FileStream applyrightstream = new FileStream(applyrightpath, FileMode.Create);
      

        await createBlogPageVM.Photo.CopyToAsync(stream);
        await createBlogPageVM.ApplyInfoLeftPhoto.CopyToAsync(applyleftstream);
        await createBlogPageVM.ApplyInfoRightPhoto.CopyToAsync(applyrightstream);
        Blog blog = new()
        {
          Title = createBlogPageVM.Title,

          HeaderUp = createBlogPageVM.HeaderUp,

          InformationUp = createBlogPageVM.InformationUp,

          HeaderMiddle = createBlogPageVM.HeaderMiddle,   
            
          InformationMiddle = createBlogPageVM.InformationMiddle,

          MiddleTextFirst = createBlogPageVM.MiddleTextFirst,

          MiddleTextSecond = createBlogPageVM.MiddleTextSecond,

          MiddleTextThird = createBlogPageVM.MiddleTextThird,

          MiddleTextFourth = createBlogPageVM.MiddleTextFourth,

          MiddleDescription = createBlogPageVM.MiddleDescription,

          HeaderEnd = createBlogPageVM.HeaderEnd,

          InformationEnd = createBlogPageVM.InformationEnd,

          ServiceInfo = createBlogPageVM.ServiceInfo,

          ServiceNameFirst = createBlogPageVM.ServiceNameFirst,

          ServicePriceFirst = createBlogPageVM.ServicePriceFirst,

          ServiceDescriptionFirst = createBlogPageVM.ServiceDescriptionFirst,

         ServiceNameSecond = createBlogPageVM.ServiceNameSecond,

          ServicePriceSecond = createBlogPageVM.ServicePriceSecond,

          ServiceDescriptionSecond = createBlogPageVM.ServiceDescriptionSecond,

          ServiceNameThird = createBlogPageVM.ServiceNameThird,

          ServicePriceThird = createBlogPageVM.ServicePriceThird,

          ServiceDescriptionThird = createBlogPageVM.ServiceDescriptionThird,

          ServiceNameFourth = createBlogPageVM.ServiceNameFourth,

          ServicePriceFourth = createBlogPageVM.ServicePriceFourth,

          ServiceDescriptionFourth = createBlogPageVM.ServiceDescriptionFourth,

          ApplyInfo = createBlogPageVM.ApplyInfo,
         
          ApplyInfoLeftImage = applyleft,
          
          ApplyInfoRightImage = applyright,
         
          Image = filename,

          AuthorId = createBlogPageVM.AuthorId,
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
        string applyleftpath = Path.Combine(_webHostEnvironment.WebRootPath, "images", blog.ApplyInfoLeftImage);
        string applyrightpath = Path.Combine(_webHostEnvironment.WebRootPath, "images", blog.ApplyInfoRightImage);

        if (System.IO.File.Exists(path) && System.IO.File.Exists(applyleftpath) && System.IO.File.Exists(applyrightpath))
        {
            System.IO.File.Delete(path);
            System.IO.File.Delete(applyleftpath);
            System.IO.File.Delete(applyrightpath);
        }
            


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
        var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == updateBlogPageVM.Id);
        if (blog == null)
            return View();

        if (updateBlogPageVM.Photo != null && updateBlogPageVM.ApplyInfoLeftPhoto != null && updateBlogPageVM.ApplyInfoRightPhoto != null)
        {
            #region Create NewImage
            if (!updateBlogPageVM.Photo.ContentType.Contains("image/")&&
                !updateBlogPageVM.ApplyInfoLeftPhoto.ContentType.Contains("image/")&& 
                !updateBlogPageVM.ApplyInfoRightPhoto.ContentType.Contains("image/"))
                return View();

            if (updateBlogPageVM.Photo.Length / 1024 > 1000 && updateBlogPageVM.ApplyInfoRightPhoto.Length / 1024>1000 && updateBlogPageVM.ApplyInfoLeftPhoto.Length / 1024>1000)
                return View();

            string filename = Guid.NewGuid().ToString() + " _ " + updateBlogPageVM.Photo.FileName;
            string applyleft = Guid.NewGuid().ToString() + " _ " + updateBlogPageVM.ApplyInfoRightPhoto.FileName;
            string applyright = Guid.NewGuid().ToString() + " _ " + updateBlogPageVM.ApplyInfoLeftPhoto.FileName;

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);
            string applyleftpath = Path.Combine(_webHostEnvironment.WebRootPath, "images", applyleft);
            string applyrightpath = Path.Combine(_webHostEnvironment.WebRootPath, "images", applyright);

            using FileStream stream = new FileStream(path, FileMode.Create);
            using FileStream applyleftstream = new FileStream(applyleft, FileMode.Create);
            using FileStream applyrighttream = new FileStream(applyright, FileMode.Create);

            await updateBlogPageVM.Photo.CopyToAsync(stream);
            await updateBlogPageVM.Photo.CopyToAsync(applyleftstream);
            await updateBlogPageVM.Photo.CopyToAsync(applyrighttream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", blog.Image);
            string applyleftoldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", blog.ApplyInfoLeftImage);
            string applyrightoldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", blog.ApplyInfoRightImage);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            blog.Image = filename;
            blog.ApplyInfoLeftImage = applyleft;
            blog.ApplyInfoRightImage = applyright;
            #endregion
        }
        blog.Title = updateBlogPageVM.Title;

        blog.HeaderUp = updateBlogPageVM.HeaderUp;

        blog.InformationUp = updateBlogPageVM.InformationUp;

        blog.HeaderMiddle = updateBlogPageVM.HeaderMiddle;

        blog.InformationMiddle = updateBlogPageVM.InformationMiddle;

        blog.MiddleTextFirst = updateBlogPageVM.MiddleTextFourth;

        blog.MiddleTextSecond = updateBlogPageVM.MiddleTextSecond;

        blog.MiddleTextThird = updateBlogPageVM.MiddleTextThird;

        blog.MiddleTextFourth = updateBlogPageVM.MiddleTextFourth;

        blog.MiddleDescription = updateBlogPageVM.MiddleDescription;

        blog.HeaderEnd = updateBlogPageVM.HeaderEnd;

        blog.InformationEnd = updateBlogPageVM.InformationEnd;

        blog.ServiceInfo = updateBlogPageVM.ServiceInfo;

        blog.ServiceNameFirst = updateBlogPageVM.ServiceNameFirst;

        blog.ServicePriceFirst = updateBlogPageVM.ServicePriceFirst;

        blog.ServiceDescriptionFirst = updateBlogPageVM.ServiceDescriptionFirst;

        blog.ServiceNameSecond = updateBlogPageVM.ServiceNameSecond;
       
        blog.ServicePriceSecond = updateBlogPageVM.ServicePriceSecond;
       
        blog.ServiceDescriptionSecond = updateBlogPageVM.ServiceDescriptionSecond;
       
        blog.ServiceNameThird = updateBlogPageVM.ServiceNameThird;
       
        blog.ServicePriceThird = updateBlogPageVM.ServicePriceThird;
       
        blog.ServiceDescriptionThird = updateBlogPageVM.ServiceDescriptionThird;
       
        blog.ServiceNameFourth = updateBlogPageVM.ServiceNameFourth;
       
        blog.ServicePriceFourth = updateBlogPageVM.ServicePriceFourth;
       
        blog.ServiceDescriptionFourth = updateBlogPageVM.ServiceDescriptionFourth;
       
        blog.ApplyInfo = updateBlogPageVM.ApplyInfo;

        blog.AuthorId = updateBlogPageVM.AuthorId;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
