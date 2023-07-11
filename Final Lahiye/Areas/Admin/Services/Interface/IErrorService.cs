using Final_Lahiye.Areas.Admin.ViewModels.Error;
using Final_Lahiye.Areas.Admin.ViewModels.ShopPage;
using Final_Lahiye.Models;
namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface IErrorService
{
    Task CreateAsync(CreateErrorPageVM createErrorPageVM);

    Task<ErrorPage> GetByIdAsync(int id);

    Task<bool> DeleteAsync(int id);

    Task<ErrorPage> UpdateAsync(UpdateErrorPageVM updateErrorPageVM);
}
