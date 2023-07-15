using Final_Lahiye.Areas.Admin.ViewModels.FAQ;
using Final_Lahiye.Models;
namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface IFaqPaymentService
{
    Task CreateAsync(CreateFaqPaymentVM createFaqPaymentVM);

    Task<FaqPayment> GetByIdAsync(int id);

    //Task<bool> DeleteAsync(int id);

    Task<FaqPayment> UpdateAsync(UpdateFaqPaymentVM updateFaqPaymentVM);
}

