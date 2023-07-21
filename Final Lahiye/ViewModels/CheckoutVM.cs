using Final_Lahiye.Models;
using Final_Lahiye.Models.FormModel;

namespace Final_Lahiye.ViewModels;
public class CheckoutVM
{
    public List<ProductBasketVM> Products { get; set; }
    public HomeProduct Product { get; set; }
    public decimal TotalPrice { get; set; }
    public CheckoutFormModel checkOutFormModel { get; set; }
}
