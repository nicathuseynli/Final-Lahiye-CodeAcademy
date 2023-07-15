using Final_Lahiye.Areas.Admin.ViewModels.HeaderUp;
using Final_Lahiye.Models;
namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface IHeaderUpTextService
{
    Task CreateAsync(CreateHeaderUpTextVM createHeaderUpTextVM);

    Task<HeaderUpText> GetByIdAsync(int id);

    //Task<bool> DeleteAsync(int id);

    Task<HeaderUpText> UpdateAsync(UpdateHeaderUpTextVM updateHeaderUpTextVM);
}
