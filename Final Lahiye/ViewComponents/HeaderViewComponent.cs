using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Final_Lahiye.ViewComponents;
public class HeaderViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public HeaderViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        //var headerUpTexts = await _context.HeaderUpTexts.FirstOrDefaultAsync();
        var headerUps = await _context.Bios.FirstOrDefaultAsync();
        //HeaderVM headerVM = new()
        //{ 
        //    //HeaderUpTexts = headerUpTexts,
        //    Bios = headerUps
        //};

        string basket = Request.Cookies["basket"];
        ViewBag.BasketCount = 0;
        ViewBag.BasketPrice = 0;
        if(basket != null)
        {
            List<ProductBasketVM> products = JsonConvert.DeserializeObject<List<ProductBasketVM>>(basket);
            ViewBag.BasketCount = products.Sum(p => p.Count);
        }


        Bio model = _context.Bios.FirstOrDefault();
        return View(await Task.FromResult(model));
    }
}
