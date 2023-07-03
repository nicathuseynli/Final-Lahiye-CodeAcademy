using Final_Lahiye.Data;
using Final_Lahiye.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        var headerUpTexts = await _context.HeaderUpTexts.FirstOrDefaultAsync();
        var headerUpSocialMedias = await _context.HeaderUpSocialMedias.FirstOrDefaultAsync();
        HeaderVM headerVM = new()
        { 
            HeaderUpTexts = headerUpTexts,
            HeaderUpSocialMedias = headerUpSocialMedias
        };
        return await Task.FromResult(View(headerVM));
    }
}
