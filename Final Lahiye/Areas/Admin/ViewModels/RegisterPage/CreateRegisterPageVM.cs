using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.RegisterPage;
public class CreateRegisterPageVM
{
    [Required]
    public string Description { get; set; }

    public IFormFile Photo { get; set; }
}
