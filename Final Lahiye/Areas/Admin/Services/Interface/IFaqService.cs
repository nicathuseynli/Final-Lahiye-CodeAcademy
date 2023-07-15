using Final_Lahiye.Areas.Admin.ViewModels.FAQ;
using Final_Lahiye.Models;
namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface IFaqService
{
    Task CreateAsync(CreateFaqPageVM createFaqPageVM);

    Task<FaqPage> GetByIdAsync(int id);

    //Task<bool> DeleteAsync(int id);

    Task<FaqPage> UpdateAsync(UpdateFaqPageVM updateFaqPageVM);
}
