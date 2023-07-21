using Final_Lahiye.Data;
using Final_Lahiye.Helpers;
using Final_Lahiye.Models;
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
    public async Task<IActionResult> Index(int? categoryId, int? colourId, int id ,int pageNumber = 1, int pageSize = 5)
    {
        HomeProduct? product = await _context.Products
            .Include(x => x.Comments)
            .ThenInclude(c => c.Children)
            .Include(x => x.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(x => x.Id == id);

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
          .Include(x => x.Comments)
          .ThenInclude(c => c.Children)
          .Include(x => x.Comments)
          .ThenInclude(c => c.User)
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
    public async Task<IActionResult> Comments(int? commentId, int blogId, string comment)
    {
        if (string.IsNullOrWhiteSpace(comment))
        {
            return Json(new
            {
                error = true,
                message = "You should write something"
            });
        }
        if (blogId < 1)
        {
            return Json(new
            {
                error = true,
                message = "Product Review not found !"
            });
        }

        var blog = await _context.Products.FirstOrDefaultAsync(x => x.Id == blogId);

        if (blog == null)
        {
            return Json(new
            {
                error = true,
                message = "Product Review not found !"
            });
        }

        var commentModel = new Comment
        {
            ProductId = blogId,
            Description = comment,
        };

        if (commentId.HasValue && await _context.Comments.AnyAsync(c => c.Id == commentId))
        {
            commentModel.ParentId = commentId;
        }
        commentModel.UserId = ControllerContext.GetUserId().Value;

        await _context.Comments.AddAsync(commentModel);

        await _context.SaveChangesAsync();
     
        return PartialView("_Comment", commentModel);
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

}
