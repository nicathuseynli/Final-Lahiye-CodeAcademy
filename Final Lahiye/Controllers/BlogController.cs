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
        var blog = await _context.Blogs.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == id);
        ViewBag.datetime = DateTime.Now.ToString("dd MMMM yyyy");

        SingleBlogVM singleBlogVM = new()
        {
            Blog = blog,
        };
        return View(singleBlogVM);
    }
  
}
