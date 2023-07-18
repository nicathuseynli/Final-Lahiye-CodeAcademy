using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Final_Lahiye.Controllers;
public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<HomeController> _logger;
    //private readonly UserManager<AppUser> _userManager;

    public HomeController(ILogger<HomeController> logger, AppDbContext context) //UserManager<AppUser> userManager)
    {
        _logger = logger;
        _context = context;
        //_userManager = userManager;
    }
    [AllowAnonymous]
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
        string basket = Request.Cookies["basket"];
        ViewBag.BasketCount = 0;
        ViewBag.BasketPrise = 0;
        if (basket != null)
        {
            List<ProductBasketVM> products = JsonConvert.DeserializeObject<List<ProductBasketVM>>(basket);
            ViewBag.BasketCount = products.Sum(p => p.Count);
        }
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
    [AllowAnonymous]
    public async Task<IActionResult> Error()
    {
        var errorPage = await _context.ErrorPages.FirstOrDefaultAsync();
        ErrorVM errorVM = new()
        {
            ErrorPages = errorPage,
        };
        return View(errorVM);
    }
    [AllowAnonymous]
    public async Task<IActionResult> FAQ()
    {
        var question = await _context.FaqPages.ToListAsync();
        FAQVM faqVM = new()
        {
            Faqs = question,
        };
        return View(faqVM);
    }
    [AllowAnonymous]
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

    [AllowAnonymous]
    public async Task<IActionResult> AddBasket(int? id)
    {
        if (id == null) return NotFound();
        HomeProduct product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        List<ProductBasketVM> products;
        string existBasket = Request.Cookies["basket"];
        if (existBasket == null)
        {
            products = new List<ProductBasketVM>();
        }
        else
        {
            products = JsonConvert.DeserializeObject<List<ProductBasketVM>>(existBasket);
        }

        ProductBasketVM checkProduct = products.FirstOrDefault(x => x.Id == id);
        if (checkProduct == null)
        {
            ProductBasketVM newProduct = new ProductBasketVM
            {
                Id = product.Id,
                Image = product.Image,
                Price = product.CurrentPrice,
                Count = 1,
            };
            products.Add(newProduct);
        }
        else
        {
            checkProduct.Count++;

        }
        string basket = JsonConvert.SerializeObject(products);
        Response.Cookies.Append("basket", basket, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });
        return RedirectToAction(nameof(Index));
    }
    [AllowAnonymous]
    public IActionResult Basket()
    {
        return Content(Request.Cookies["basket"]);
    }
/*    [AllowAnonymous]
    public IActionResult Search(string search)
    {
        var model = _context.Products.Where(p => p.Name.Contains(search)).OrderByDescending(p => p.Id).Take(10).ToList();
        return PartialView("_SearchPartial", model);
    }*/
    
}