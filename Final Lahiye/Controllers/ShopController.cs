using Final_Lahiye.Data;
using Final_Lahiye.Helpers;
using Final_Lahiye.Models;
using Final_Lahiye.Models.FormModel;
using Final_Lahiye.Utilities.Pagination;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Final_Lahiye.Controllers;
public class ShopController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ShopController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index(int id ,int? categoryId, int? colourId, int pageNumber = 1, int pageSize = 8)
    {
        var query = _context.Products
            .Include(x => x.Category)
            .Include(x=>x.Colour)
            .AsQueryable();

        if (categoryId.HasValue && categoryId.Value > 0 || colourId.HasValue && colourId.Value > 0)
        {
            query = query.Where(x => x.CategoryId == categoryId || x.ColourId == colourId);
        }

        var paginatedProducts = await query
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
             .ToListAsync();

        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling((double) totalCount / pageSize);

        var category = await _context.Categories.Include(x => x.Products).ToListAsync();
        var colour = await _context.Colours.Include(x => x.Products).ToListAsync();

        ShopVM shopVM = new()
        {
            Category = await _context.Categories.FirstOrDefaultAsync(),
            Categories = category,
            Colours = colour,
            Products = paginatedProducts,
            ProductPagination = new Pagination<HomeProduct>(paginatedProducts, pageNumber, totalPages)
        };

        return View(shopVM);
    }
    
    [AllowAnonymous]
    public async Task<IActionResult> Product(int id) 
    {
        HomeProduct? product = await _context.Products
          .Include(c => c.Category)
          .Include(c=>c.Colour)
          .FirstOrDefaultAsync(x => x.Id == id);

        var newproduct = await _context.Products.ToListAsync();

        ProductVM productVM = new()
        {
            Products = newproduct,
            Product = product
        };
        return View(productVM);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Checkout()
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
    public JsonResult GetProducts([FromBody] FilterPrice value)
    {
        var products = _context.Products
            .Where(product => product.CurrentPrice >= value.Start && product.CurrentPrice <= value.End)
            .ToList();

        return Json(products);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Order()
    {
      return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(CheckoutFormModel checkoutFormModel)
    {
       if (ModelState.IsValid)
        {
            try
            {
                await _context.CheckOutFormModels.AddAsync(checkoutFormModel);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Something went wrong" });
            }
        }
        else
        {
            return Json(new { success = false, message = "Ooops, Your Biling details are not done !" });
        }
    }

}



