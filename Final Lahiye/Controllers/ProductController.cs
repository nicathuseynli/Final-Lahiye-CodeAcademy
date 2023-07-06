using Final_Lahiye.Data;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Controllers;
public class ProductController : Controller
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? id)
    {
        var homeproduct = await _context.Products.Include(c => c.Category).FirstOrDefaultAsync(x => x.Id == id);
        var newproduct = await _context.Products.ToListAsync();
        //var realetedProduct = await _context.RealetedProducts.FirstOrDefaultAsync();
        //var singleProduct = await _context.SingleProducts.FirstOrDefaultAsync();
        //var singleProducts = await _context.SingleProducts.ToListAsync();
        //ViewBag.datetime = DateTime.Now.ToString("dd MMMM yyyy");

        AllProductVM productVM = new ()
        {
            Product = homeproduct,
            Products = newproduct,
            //RealetedProducts = realetedProduct,
            //SingleProduct = singleProduct,
            //SingleProducts = singleProducts
        };
        return View(productVM);
    }
}
