using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Final_Lahiye.Controllers;
public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var heros = await _context.Heros.FirstOrDefaultAsync();
        var banners = await _context.Banners.FirstOrDefaultAsync();
        HomeVM homeVM = new HomeVM()
        {
            Hero = heros,
            Banner = banners,
        };
        return View(homeVM);
    }
}