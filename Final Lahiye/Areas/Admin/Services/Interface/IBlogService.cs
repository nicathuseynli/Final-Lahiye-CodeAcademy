using Final_Lahiye.Areas.Admin.ViewModels.Blog;
using Final_Lahiye.Models;

namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface IBlogService
{
    Task CreateAsync(CreateBlogPageVM createBlogPageVM);

    Task<Blog> GetByIdAsync(int id);

    Task DeleteAsync(int id);

    Task UpdateAsync(UpdateBlogPageVM updateBlogPageVM);
}
