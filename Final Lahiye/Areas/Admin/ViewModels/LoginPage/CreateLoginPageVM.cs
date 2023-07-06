using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.LoginPage;
public class CreateLoginPageVM
{
    [Required]
    public string Description { get; set; }

    public IFormFile Photo { get; set; }
}
