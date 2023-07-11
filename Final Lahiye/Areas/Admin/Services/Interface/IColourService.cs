using Final_Lahiye.Areas.Admin.ViewModels.ShopPage;
using Final_Lahiye.Models;
namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface IColourService
{
    Task CreateAsync(CreateColourVM createColourVM);

    Task<Colour> GetByIdAsync(int id);

    Task<bool> DeleteAsync(int id);

    Task<Colour> UpdateAsync(UpdateColourVM updateColourVM);
}
