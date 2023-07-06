using Final_Lahiye.Models;

namespace Final_Lahiye.ViewModels;
public class ShopVM
{
    public List<Product> Products { get; set; }

    public List<Category> Categories { get; set; }

    public List<Colour> Colours { get; set; }

    public List<StockStatus> StockStatuses { get; set; }

    public Product Product { get; set; }
}
