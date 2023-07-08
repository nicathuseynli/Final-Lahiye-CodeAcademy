using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.Home;
public class CreateTestimonialVM
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Header { get; set; }

    [Required]
    public IFormFile Photo { get; set; }
}
