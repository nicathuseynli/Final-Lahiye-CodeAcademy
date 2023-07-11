using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Models;
namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface IElementorService
{
    Task CreateAsync(CreateElementorWidgetVM createElementorWidgetVM);

    Task<Elementor> GetByIdAsync(int id);

    Task<bool> DeleteAsync(int id);
    
    Task<Elementor> UpdateAsync(UpdateElementorWidgetVM updateElementorWidgetVM);
}
