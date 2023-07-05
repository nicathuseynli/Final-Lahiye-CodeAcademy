using System.Diagnostics.CodeAnalysis;

namespace Final_Lahiye.Areas.Admin.ViewModels.Home;
public class UpdateShortInfoVM
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Information { get; set; }

    public IFormFile IconPhoto { get; set; }

    [AllowNull]
    public string Icon { get; set; }
}
