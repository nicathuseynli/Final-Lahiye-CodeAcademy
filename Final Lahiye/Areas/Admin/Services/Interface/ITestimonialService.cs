using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Models;
namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface ITestimonialService
{
    Task CreateAsync(CreateTestimonialVM createTestimonialVM);

    Task<Testimonial> GetByIdAsync(int id);

    Task<bool> DeleteAsync(int id);

    Task<Testimonial> UpdateAsync(UpdateTestimonialVM updateTestimonialVM);
}
