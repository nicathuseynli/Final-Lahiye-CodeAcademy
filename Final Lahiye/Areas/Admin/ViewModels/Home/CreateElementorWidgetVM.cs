using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.Home;
public class CreateElementorWidgetVM
{
    [Required]
    public IFormFile ElementorUpPhoto { get; set; }

    [Required]
    public IFormFile ElementorDownPhoto { get; set; }
}
