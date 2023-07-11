using Final_Lahiye.Areas.Admin.ViewModels.Blog;
using Final_Lahiye.Models;

namespace Final_Lahiye.Areas.Services.Interface;
public interface IAuthorService
{
    Task CreateAsync(CreateAuthorVM createAuthorVM);

    Task<Author> GetByIdAsync(int id);

    Task<bool> DeleteAsync(int id);

    Task<Author> UpdateAsync(UpdateAuthorVM updateAuthorVM);
}
