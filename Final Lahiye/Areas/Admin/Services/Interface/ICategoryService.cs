using Final_Lahiye.Areas.Admin.ViewModels.ShopPage;
using Final_Lahiye.Models;

namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface ICategoryService
{
    Task CreateAsync(CreateCategoryVM createCategoryVM);

    Task<Category> GetByIdAsync(int id);

    //Task<bool> DeleteAsync(int id);

    Task<Category> UpdateAsync(UpdateCategoryVM updateCategoryVM);
}
