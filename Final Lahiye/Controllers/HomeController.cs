using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Final_Lahiye.Models.FormModel;
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
    private readonly IWebHostEnvironment _webHostEnvironment;
    public HomeController(ILogger<HomeController> logger, AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _context = context;
        _webHostEnvironment = webHostEnvironment;
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
        var contact = await _context.Contacts.FirstOrDefaultAsync();

        ContactVM contactVM = new()
        {
            Contacts = contacts,
            ContactDetails = details,
            Contact = contact
        };

        return View(contactVM);
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(ContactFormModel contactForm)
    {
        if (ModelState.IsValid)
        {
            try
            {
       /*         await _context.Contacts.AddAsync(*//*contactForm*//*);
                await _context.SaveChangesAsync();
*/
                // Вернуть успешный JSON-ответ
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Вернуть JSON-ответ с сообщением об ошибке
                return Json(new { success = false, message = "Ошибка при сохранении данных." });
            }
        }
        else
        {
            // Вернуть JSON-ответ с сообщением об ошибке валидации
            return Json(new { success = false, message = "Некоторые поля формы заполнены некорректно." });
        }
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
                Name = product.Name,
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
        return Json(new
        {
            error = false,
            message = "ok"
        });

    }

    [AllowAnonymous]
    public IActionResult Basket()
    {
        string existBasket = Request.Cookies["basket"];
        if (existBasket != null)
        {
            List<ProductBasketVM> products = JsonConvert.DeserializeObject<List<ProductBasketVM>>(existBasket);
            ViewBag.TotalPrice = CalculateTotalPrice(products);
            return View(products);
        }
        else
        {
            List<ProductBasketVM> products = new List<ProductBasketVM>();
            ViewBag.TotalPrice = 0;
            return View(products);
        }
    }

    [AllowAnonymous]
    public decimal CalculateTotalPrice(List<ProductBasketVM> products)
    {
        decimal totalPrice = 0;
        foreach (var product in products)
        {
            totalPrice += product.TotalPrice;
        }
        return totalPrice;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Remove(int id)
    {
        string existBasket = Request.Cookies["basket"];
        if (existBasket == null)
        {
            return NotFound();
        }
        var prod = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (prod == null) return NotFound();
        List<ProductBasketVM> products = JsonConvert.DeserializeObject<List<ProductBasketVM>>(existBasket);
        ProductBasketVM productToRemove = products.FirstOrDefault(p => p.Id == id);
        if (productToRemove == null)
        {
            return NotFound();
        }
        products.Remove(productToRemove);
        string basket = JsonConvert.SerializeObject(products);
        Response.Cookies.Append("basket", basket, new CookieOptions { MaxAge = TimeSpan.FromDays(14) });
        return RedirectToAction("Basket");
    }

}

