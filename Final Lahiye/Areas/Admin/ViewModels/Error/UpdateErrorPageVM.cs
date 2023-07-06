using System.Diagnostics.CodeAnalysis;

namespace Final_Lahiye.Areas.Admin.ViewModels.Error;
public class UpdateErrorPageVM
{
    public int Id { get; set; }

    public string Message { get; set; }

    public string Image { get; set; }

    [AllowNull]
    public IFormFile Photo { get; set; }
}
