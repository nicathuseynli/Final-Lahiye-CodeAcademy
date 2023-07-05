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
        var hero = await _context.Heros.FirstOrDefaultAsync();
        var banner = await _context.Banners.FirstOrDefaultAsync();
        var elementor = await _context.Elementors.FirstOrDefaultAsync();
        var shortInfo = await _context.ShortInformations.ToListAsync();
        var testimonial = await _context.Testimonials.ToListAsync();
        HomeVM homeVM = new HomeVM()
        {
            Hero = hero,
            Banner = banner,
            Elementor = elementor,
            ShortInformations = shortInfo,
            Testimonials = testimonial,
        };
        return View(homeVM);
    }
}