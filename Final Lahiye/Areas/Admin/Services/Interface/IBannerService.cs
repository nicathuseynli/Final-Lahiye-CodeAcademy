using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Models;

namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface IBannerService
{
    Task CreateAsync(CreateBannerVM createbannerVM);

    Task<Banner> UpdateAsync(UpdateBannerVM updatebannerVM);

    Task<bool> DeleteAsync(int id);

    Task<Banner> GetByIdAsync(int id);

}
