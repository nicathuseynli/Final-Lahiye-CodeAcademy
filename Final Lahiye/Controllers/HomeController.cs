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
        var product = await _context.Products.FirstOrDefaultAsync();
        var testimonial = await _context.Testimonials.FirstOrDefaultAsync();
        var shortInfo = await _context.ShortInformations.ToListAsync();
        var hometestimonial = await _context.Testimonials.ToListAsync();
        var homeproducts = await _context.Products.ToListAsync();
        HomeVM homeVM = new HomeVM()
        {
            Hero = hero,
            Banner = banner,
            Elementor = elementor,
            ShortInformations = shortInfo,
            Testimonials = hometestimonial,
            Testimonial = testimonial,
            Products = homeproducts,
            Product = product,
        };
        return View(homeVM);

    }
    public async Task<IActionResult> Error()
    {
        var errorPage = await _context.ErrorPages.FirstOrDefaultAsync();
        ErrorVM errorVM = new()
        {
            ErrorPages = errorPage,
        };
        return View(errorVM);
    }
    public async Task<IActionResult> FAQ()
    {
        var question = await _context.FaqPages.ToListAsync();
        FAQVM faqVM = new()
        {
            Faqs = question,
        };
        return View(faqVM);
    }
    public async Task<IActionResult> Contact()
    {
        var contacts = await _context.Contacts.ToListAsync();
        var details = await _context.ContactDetailss.FirstOrDefaultAsync();

        ContactVM contactVM = new()
        {
            Contacts = contacts,
            ContactDetails = details,
        };
        return View(contactVM);
    }


}