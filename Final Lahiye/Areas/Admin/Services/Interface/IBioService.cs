using Final_Lahiye.Areas.Admin.ViewModels.HeaderUp;
using Final_Lahiye.Models;

namespace Final_Lahiye.Areas.Admin.Services.Interface;

public interface IBioService
{
    Task CreateAsync(CreateBioVM createHeaderBioVM);

    Task<Bio> GetByIdAsync(int id);

    //Task<bool> DeleteAsync(int id);

    Task<Bio> UpdateAsync(UpdateBioVM updateHeaderUpBioVM);
}
