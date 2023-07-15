using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Models;
namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface IShortInfoService
{
    Task CreateAsync(CreateShortInfoVM createshortInfoVM);

    Task<ShortInformation> GetByIdAsync(int id);

    //Task<bool> DeleteAsync(int id);

    Task<ShortInformation> UpdateAsync(UpdateShortInfoVM updateshortInfoVM);
}

