using Final_Lahiye.Data;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Controllers;
public class SingleBlogController : Controller
{
    private readonly AppDbContext _context;

    public SingleBlogController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int id)
    {
        var blog = await _context.Blogs.Include(x => x.Author).FirstOrDefaultAsync(x=>x.Id==id);
        ViewBag.datetime = DateTime.Now.ToString("dd MMMM yyyy");

        SingleBlogVM singleBlogVM = new()
        {
            Blog = blog,
        };
        return View(singleBlogVM);
    }
}
