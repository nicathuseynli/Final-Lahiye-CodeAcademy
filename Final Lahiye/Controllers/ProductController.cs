/*using Final_Lahiye.Data;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]

    public async Task<IActionResult> Index (int id)
    {
        var homeproduct = await _context.Products.Include(c => c.Category).FirstOrDefaultAsync(x => x.Id == id);
        var newproduct = await _context.Products.ToListAsync();

        ProductVM productVM = new ()
        {
            Product = homeproduct,
            Products = newproduct,
        };
        return View(productVM);
    }
}
*/