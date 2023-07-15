using Final_Lahiye.Areas.Admin.ViewModels.HeaderUp;
using Final_Lahiye.Models;

namespace Final_Lahiye.Areas.Admin.Services.Interface;

public interface IHeaderUpSocialMediaService
{
    Task CreateAsync(CreateHeaderUpSocialMediaVM createHeaderUpSocialMediaVM);

    Task<HeaderUpSocialMedia> GetByIdAsync(int id);

    //Task<bool> DeleteAsync(int id);

    Task<HeaderUpSocialMedia> UpdateAsync(UpdateHeaderUpSocialMediaVM updateHeaderUpSocialMediaVM);
}
