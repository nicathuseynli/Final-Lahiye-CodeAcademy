using System.ComponentModel.DataAnnotations;

namespace Final_Layihe.Models.FormModel
{
    public class RegisterFormModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
