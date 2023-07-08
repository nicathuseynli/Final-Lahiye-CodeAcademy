using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.ShopPage;
public class CreateColourVM
{
    [Required]
    public string Name { get; set; }
}
