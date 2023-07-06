using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.Contact;
public class CreateContactDetailsVM
{
    [Required]
    public string ByAddress { get; set; }

    [Required]
    public string ByEmail { get; set; }

    [Required]
    public string ByPhone { get; set; }
}
