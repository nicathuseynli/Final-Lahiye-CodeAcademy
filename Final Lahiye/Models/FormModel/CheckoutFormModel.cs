using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Models.FormModel;
public class CheckoutFormModel:BaseEntity<int>
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }

    public string CompanyName { get; set; }
    [Required]
    public string Address { get; set; } 
    [Required]
    public string Apartment { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public int ZipCode { get; set; } 
    [Required]
    public int PhoneNumber { get; set; }
    [Required]
    [EmailAddress]
    public int Email { get; set; }
    [Required]
    public string Message { get; set; }

}
