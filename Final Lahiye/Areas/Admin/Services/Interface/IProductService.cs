using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Models;

namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface IProductService
{
    Task CreateAsync(CreateHomeProductVM createhomeproductVm);
 
    Task<HomeProduct> GetByIdAsync(int id);

    Task<bool> DeleteAsync(int id);

    Task<HomeProduct> UpdateAsync(UpdateHomeProductVM updateHomeProductVM);
}
