using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<ProductController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IProductService _productService;

    public ProductController(AppDbContext context, ILogger<ProductController> logger, IWebHostEnvironment webHostEnvironment, IProductService productService)
    {
        _context = context;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var homeproduct = await _context.Products.ToListAsync();
        return View(homeproduct);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Category = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        ViewBag.Colour = new SelectList(await _context.Colours.ToListAsync(), "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateHomeProductVM createhomeproductVm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Category = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            ViewBag.Colour = new SelectList(await _context.Colours.ToListAsync(), "Id", "Name");
            return View();
        }

        if (!createhomeproductVm.Photo.ContentType.Contains("image/") )
            return View();

        if (createhomeproductVm.Photo.Length / 1024 > 500 )
            return View();

        await _productService.CreateAsync(createhomeproductVm);
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var homeproduct = await _productService.GetByIdAsync(id);
        return View(homeproduct);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var homeproduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (homeproduct == null) return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", homeproduct.Image);

        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

        _context.Products.Remove(homeproduct);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        ViewBag.Category = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        ViewBag.Colour = new SelectList(await _context.Colours.ToListAsync(), "Id", "Name");

        var homeproduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (homeproduct == null) return NotFound();

        var updateHomeProductVM = new UpdateHomeProductVM()
        {
            Id = id,
            CategoryId = homeproduct.CategoryId,
            ColourId = homeproduct.ColourId,
            Name = homeproduct.Name,
            Header = homeproduct.Header,
            Rating = homeproduct.Rating,
            SKUCode = homeproduct.SKUCode,
            DeliveryInfo = homeproduct.DeliveryInfo,
            ShippingInfo = homeproduct.ShippingInfo,
            Description = homeproduct.Description,
            Style = homeproduct.Style,
            PackCount = homeproduct.PackCount,
            RoomType = homeproduct.RoomType,
            IdealFor = homeproduct.IdealFor,
            Capacity = homeproduct.Capacity,
            Shape = homeproduct.Shape,
            LastPrice = homeproduct.LastPrice,
            CurrentPrice = homeproduct.CurrentPrice,
            SalePercent = homeproduct.SalePercent,
            Image = homeproduct.Image,
        };

        return View(updateHomeProductVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateHomeProductVM updateHomeProductVM)
    {

        ViewBag.Category = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        ViewBag.Colour = new SelectList(await _context.Colours.ToListAsync(), "Id", "Name");
        var result = await _productService.UpdateAsync(updateHomeProductVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
