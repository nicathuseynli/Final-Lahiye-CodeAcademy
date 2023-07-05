using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.Home;
public class CreateShortInfoVM
{
    [Required]
    public string Title { get; set; }

    [Required]
    public IFormFile IconPhoto { get; set; }

    [Required]
    public string Information { get; set; }
}
