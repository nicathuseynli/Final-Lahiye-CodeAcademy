using System.Diagnostics.CodeAnalysis;

namespace Final_Lahiye.Areas.Admin.ViewModels.Home;
public class UpdateElementorWidgetVM
{
    public int Id { get; set; }

    public string ElementorUpImage { get; set; }

    [AllowNull]
    public IFormFile ElementorUpPhoto { get; set; }

    public string ElementorDownImage { get; set; }

    [AllowNull]
    public IFormFile ElementorDownPhoto { get; set; }

}
