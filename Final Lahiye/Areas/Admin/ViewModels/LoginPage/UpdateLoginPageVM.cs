using System.Diagnostics.CodeAnalysis;

namespace Final_Lahiye.Areas.Admin.ViewModels.LoginPage;
public class UpdateLoginPageVM
{
    public int Id { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    [AllowNull]
    public IFormFile Photo { get; set; }
}
