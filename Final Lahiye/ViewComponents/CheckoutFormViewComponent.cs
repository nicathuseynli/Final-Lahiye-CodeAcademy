using Final_Lahiye.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.ViewComponents;
public class CheckoutFormViewComponent :ViewComponent
{
    private readonly AppDbContext _context;

    public CheckoutFormViewComponent(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var checkoutForm = await _context.CheckOutFormModels.FirstOrDefaultAsync();

        return View(await Task.FromResult(checkoutForm));
    }
}
