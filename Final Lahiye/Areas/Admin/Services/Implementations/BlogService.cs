using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Blog;
using Final_Lahiye.Data;
using Final_Lahiye.Models;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class BlogService : IBlogService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly AppDbContext _context;

    public BlogService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task CreateAsync(CreateBlogPageVM createBlogPageVM)
    {
        throw new NotImplementedException();

    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Blog> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UpdateBlogPageVM updateBlogPageVM)
    {
        throw new NotImplementedException();
    }
}
