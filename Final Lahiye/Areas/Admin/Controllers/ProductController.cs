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

    public ProductController(AppDbContext context, ILogger<ProductController> logger, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
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

        string filename = Guid.NewGuid().ToString() + "_" + createhomeproductVm.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createhomeproductVm.Photo.CopyToAsync(stream);

        HomeProduct homeproduct = new()
        {
            Name = createhomeproductVm.Name,
            LastPrice = createhomeproductVm.LastPrice,
            InStock = createhomeproductVm.InStock,
            CurrentPrice = createhomeproductVm.CurrentPrice,
            SalePercent = createhomeproductVm.SalePercent,
            Header = createhomeproductVm.Header,
            Rating = createhomeproductVm.Rating,
            SKUCode = createhomeproductVm.SKUCode,
            DeliveryInfo = createhomeproductVm.DeliveryInfo,
            ShippingInfo = createhomeproductVm.ShippingInfo,
            Description = createhomeproductVm.Description,
            Style = createhomeproductVm.Style,
            RoomType = createhomeproductVm.RoomType,
            PackCount = createhomeproductVm.PackCount,
            IdealFor = createhomeproductVm.IdealFor,    
            Capacity = createhomeproductVm.Capacity,
            Shape = createhomeproductVm.Shape,
            CategoryId = createhomeproductVm.CategoryId,
            ColourId = createhomeproductVm.ColourId,
            Image = filename,
        };
        await _context.Products.AddAsync(homeproduct);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var homeproduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (homeproduct == null)
            return NotFound();
        return View(homeproduct);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var homeproduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (homeproduct == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", homeproduct.Image);

        if (System.IO.File.Exists(path))  System.IO.File.Delete(path);
        

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
            InStock = homeproduct.InStock,
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

        var homeproduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == updateHomeProductVM.Id);
        if (homeproduct == null) return NotFound();

        if (updateHomeProductVM.Photo != null)
        {
            if (!updateHomeProductVM.Photo.ContentType.Contains("image/"))  return View();

            if (updateHomeProductVM.Photo.Length / 1024 > 500 ) return View();


            string filename = Guid.NewGuid().ToString() + " _ " + updateHomeProductVM.Photo.FileName;

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateHomeProductVM.Photo.CopyToAsync(stream);

            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", homeproduct.Image);
            if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
            
            homeproduct.Image = filename;
        }

        homeproduct.Name = updateHomeProductVM.Name;
        homeproduct.CurrentPrice = updateHomeProductVM.CurrentPrice;
        homeproduct.LastPrice = updateHomeProductVM.LastPrice;
        homeproduct.Header = updateHomeProductVM.Header;
        homeproduct.Rating = updateHomeProductVM.Rating;
        homeproduct.SKUCode = updateHomeProductVM.SKUCode;
        homeproduct.DeliveryInfo = updateHomeProductVM.DeliveryInfo;
        homeproduct.ShippingInfo = updateHomeProductVM.ShippingInfo;
        homeproduct.Description = updateHomeProductVM.Description;
        homeproduct.Style = updateHomeProductVM.Style;
        homeproduct.PackCount = updateHomeProductVM.PackCount;
        homeproduct.RoomType = updateHomeProductVM.RoomType;
        homeproduct.IdealFor = updateHomeProductVM.IdealFor;
        homeproduct.Capacity = updateHomeProductVM.Capacity;
        homeproduct.Shape = updateHomeProductVM.Shape;
        homeproduct.InStock = updateHomeProductVM.InStock;
        homeproduct.SalePercent = updateHomeProductVM.SalePercent;
        homeproduct.CategoryId = updateHomeProductVM.CategoryId;
        homeproduct.ColourId = updateHomeProductVM.ColourId;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
