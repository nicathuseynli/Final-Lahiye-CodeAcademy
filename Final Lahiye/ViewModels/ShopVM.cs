using Final_Lahiye.Models;
using Final_Lahiye.Utilities.Pagination;

namespace Final_Lahiye.ViewModels;
public class ShopVM
{
    public List<HomeProduct> Products { get; set; }

    public List<Category> Categories { get; set; }

    public List<Colour> Colours { get; set; }

    public HomeProduct Product { get; set; }

    public Category Category { get; set; }

    public Pagination<HomeProduct> Paginations { get; set; }
}
