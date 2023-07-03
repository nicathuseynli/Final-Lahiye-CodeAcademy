using System.Diagnostics.CodeAnalysis;

namespace Final_Lahiye.Areas.Admin.ViewModels.Home;
public class UpdateHeroVM
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public IFormFile HeroPhoto { get; set; }

    [AllowNull]
    public string Image { get; set; }
}
