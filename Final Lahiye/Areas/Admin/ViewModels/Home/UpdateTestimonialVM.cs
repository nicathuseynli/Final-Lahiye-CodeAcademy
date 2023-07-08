using System.Diagnostics.CodeAnalysis;

namespace Final_Lahiye.Areas.Admin.ViewModels.Home;
public class UpdateTestimonialVM
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Header { get; set; }

    public string Image { get; set; }

    [AllowNull]
    public IFormFile Photo { get; set; }
}
