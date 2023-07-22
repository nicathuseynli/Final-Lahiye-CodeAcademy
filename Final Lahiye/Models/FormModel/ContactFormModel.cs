using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Models.FormModel;
public class ContactFormModel : BaseEntity<int>
{
    [Required(ErrorMessage = "First Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone Number is required")]
    public int Phone { get; set; }

    [Required]
    public string Message { get; set; }
}
