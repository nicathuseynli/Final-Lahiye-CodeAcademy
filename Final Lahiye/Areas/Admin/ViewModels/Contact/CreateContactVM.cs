using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Final_Lahiye.Areas.Admin.ViewModels.Contact;
public class CreateContactVM
{
    [Required]
    public string ByAddress { get; set; }

    [Required]
    public string ByEmail { get; set; }

    [Required]
    public string ByPhone { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string Magazine { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public IFormFile Photo { get; set; }
}
