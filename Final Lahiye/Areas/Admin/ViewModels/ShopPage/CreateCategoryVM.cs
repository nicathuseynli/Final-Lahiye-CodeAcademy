using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.ShopPage;
public class CreateCategoryVM
{
    [Required]
    public string Name { get; set; }
}
