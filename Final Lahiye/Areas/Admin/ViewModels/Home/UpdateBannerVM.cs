using System.Diagnostics.CodeAnalysis;

namespace Final_Lahiye.Areas.Admin.ViewModels.Home;
public class UpdateBannerVM
{
    public int Id { get; set; }

    public string Title { get; set; }

    public IFormFile BannerPhoto { get; set; }

    [AllowNull]
    public string Image { get; set; }
}
