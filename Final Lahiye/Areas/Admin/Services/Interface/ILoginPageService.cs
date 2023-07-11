using Final_Lahiye.Areas.Admin.ViewModels.LoginPage;
using Final_Lahiye.Models;
namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface ILoginPageService
{
    Task CreateAsync(CreateLoginPageVM createLoginVM);

    Task<LoginPage> GetByIdAsync(int id);

    Task<bool> DeleteAsync(int id);

    Task<LoginPage> UpdateAsync(UpdateLoginPageVM updateLoginVM);
}
