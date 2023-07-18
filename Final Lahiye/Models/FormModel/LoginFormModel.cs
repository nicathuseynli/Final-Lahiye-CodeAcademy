using System.ComponentModel.DataAnnotations;

namespace Final_Layihe.Models.FormModel;
public class LoginFormModel
{
     [Required]
      public string Email { get; set; }
     [Required]
     [DataType(DataType.Password)]
     public string Password { get; set; }
}
