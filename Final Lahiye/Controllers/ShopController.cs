using Final_Lahiye.Data;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Controllers;
public class ShopController : Controller
{
    private readonly AppDbContext _context;

    public ShopController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? categoryId, int? colourId)
    {
        var query = _context.Products.Include(x => x.Category).AsQueryable();

        if (categoryId.HasValue && categoryId.Value > 0 || colourId.HasValue && colourId.Value > 0)
        {
            query = query.Where(x => x.CategoryId == categoryId || x.ColourId == colourId);
        }
        var product = await query.ToListAsync();

        var category = await _context.Categories.Include(x => x.Products).ToListAsync();
        var colour = await _context.Colours.Include(x => x.Products).ToListAsync();
        var singCat = await _context.Categories.FirstOrDefaultAsync();
        var products= await _context.Products.ToListAsync();

        ShopVM shopVM = new()
        {
            Category = singCat,
            Categories = category,
            Colours = colour,
            Products = products,
        };
        return View(shopVM);
    }
}
