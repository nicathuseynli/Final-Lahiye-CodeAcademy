using Final_Lahiye.Models;
using Final_Layihe.Models.FormModel;

namespace Final_Lahiye.ViewModels;
public class LoginRegisterVM
{
    public LoginPage LoginPage { get; set; }

    public RegisterPage RegisterPage { get; set; }
    
    public LoginFormModel LoginFormModel { get; set; }
    
    public RegisterFormModel RegisterFormModel { get; set; }
}
