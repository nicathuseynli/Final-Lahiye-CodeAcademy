using Final_Lahiye.Areas.Admin.ViewModels.RegisterPage;
using Final_Lahiye.Models;
namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface IRegisterPageService
{
    Task CreateAsync(CreateRegisterPageVM createRegisterPageVM);

    Task<RegisterPage> GetByIdAsync(int id);

    Task<bool> DeleteAsync(int id);

    Task<RegisterPage> UpdateAsync(UpdateRegisterPageVM updateRegisterVM);
}
