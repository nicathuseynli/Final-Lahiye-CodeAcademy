using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.Home;
public class CreateBannerVM
{
    [Required]
    public string Title { get; set; }

    public IFormFile BannerPhoto { get; set; }
}
