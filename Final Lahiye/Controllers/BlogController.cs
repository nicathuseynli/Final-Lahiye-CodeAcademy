using Final_Lahiye.Data;
using Final_Lahiye.ViewModels;
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

    public async Task<IActionResult> Index()
    {

        //var query = _context.Blogs.Include(x => x.AuthorName).AsQueryable();

        //if (categoryId.HasValue && categoryId.Value > 0 || shoppagecolourId.HasValue && authorId.Value > 0)
        //{
        //    query = query.Where(x => x.AuhtorId == authorId);
        //}
        //var blogPage = await query.ToListAsync();

        var blog = await _context.Blogs.Include(x=>x.Author).ToListAsync();
        var author = await _context.Authors.Include(x=>x.Blogs).ToListAsync();
        ViewBag.datetime = DateTime.Now.ToString("dd MMMM yyyy");

        BlogVM blogVM = new()
        {
            Blogs = blog,
            Authors = author,
        };
        return View(blogVM);
    }
}
