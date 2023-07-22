using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Models.FormModel;
public class CheckoutFormModel : BaseEntity<int>
{
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }

    public string CompanyName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Apartment is required")]
    public string Apartment { get; set; }

    [Required(ErrorMessage = "City is required")]
    public string City { get; set; }

    [Required(ErrorMessage = "ZipCode is required")]
    public int ZipCode { get; set; }

    [Required(ErrorMessage = "Phone is required")]
    public int PhoneNumber { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    public string Message { get; set; }

}
