using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.HeaderUp;
public class CreateHeaderUpTextVM
{
    [Required]
    public string Text { get; set; }
}
