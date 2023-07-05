using Final_Lahiye.Areas.Admin.ViewModels.Blog;
using Final_Lahiye.Models;

namespace Final_Lahiye.Areas.Services.Interface;
public interface IAuthorService
{
    Task CreateAsync(CreateAuthorVM createAuthorVM);

    Task<Author> GetByIdAsync(int id);

    Task DeleteAsync(int id);

    Task UpdateAsync(UpdateAuthorVM updateAuthorVM);
}
