using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Models;
namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface IHeroService
{
    Task CreateAsync(CreateHeroVM createHeroVM);

    Task<Hero> GetByIdAsync(int id);

    //Task<bool> DeleteAsync(int id);

    Task<Hero> UpdateAsync(UpdateHeroVM updateHeroVM);
}
