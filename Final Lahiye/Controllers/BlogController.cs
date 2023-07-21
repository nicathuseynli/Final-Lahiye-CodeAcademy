using Final_Lahiye.Data;
using Final_Lahiye.Helpers;
using Final_Lahiye.Models;
using Final_Lahiye.Services.Interfaces;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Controllers;
public class BlogController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<BlogController> _logger;
    public BlogController(AppDbContext context, ILogger<BlogController> logger)
    {
        _context = context;
        _logger = logger;
    }
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        ViewBag.datetime = DateTime.Now.ToString("dd MMMM yyyy");
        var blog = await _context.Blogs.Include(x => x.Author).ToListAsync();
        var author = await _context.Authors.Include(x => x.Blogs).ToListAsync();

        BlogVM blogVM = new()
        {
            Blogs = blog,
            Authors = author,
        };
        return View(blogVM);
    }
    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        //var blog = await _context.Blogs
        //    .Include(x => x.Author)
            //.Include(x => x.Comments)
            //.ThenInclude(c => c.Children)
            //.Include(x => x.Comments)
            //.ThenInclude(c => c.User).FirstOrDefaultAsync(x => x.Id == id);
        ViewBag.datetime = DateTime.Now.ToString("dd MMMM yyyy");

        //SingleBlogVM singleBlogVM = new()
        //{
        //    Blog = blog,
        //};
        return View();
    }
    //[HttpPost]
    //public async Task<IActionResult> Comments(int? commentId, int blogId, string comment)
    //{
    //    if (string.IsNullOrWhiteSpace(comment))
    //    {
    //        return Json(new
    //        {
    //            error = true,
    //            message = "Serh bos buraxila bilmez"
    //        });
    //    }
    //    if (blogId < 1)
    //    {
    //        return Json(new
    //        {
    //            error = true,
    //            message = "blog movcud deyil"
    //        });
    //    }

    //    var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == blogId);

    //    if (blog == null)
    //    {
    //        return Json(new
    //        {
    //            error = true,
    //            message = "blog movcud deyil"
    //        });
    //    }

    //    var commentModel = new Comment
    //    {
    //        BlogId = blogId,
    //        Description = comment,
    //    };

    //    if (commentId.HasValue && await _context.Comments.AnyAsync(c => c.Id == commentId))
    //    {
    //        commentModel.ParentId = commentId;
    //    }


    //    commentModel.UserId = ControllerContext.GetUserId().Value;

    //    await _context.Comments.AddAsync(commentModel);

    //    /*         try
    //             {*/
    //    await _context.SaveChangesAsync();
    //    /*  }*/
    //    /*           catch (Exception ex)
    //               {

    //                   throw;
    //               }*/
    //    return PartialView("_Comment", commentModel);
    //}
}
