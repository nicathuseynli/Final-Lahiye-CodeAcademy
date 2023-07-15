using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.ShopPage;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CreateCategoryVM createCategoryVM)
    {
        Category category = new()
        {
            Name = createCategoryVM.Name,
        };
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    //public async Task<bool> DeleteAsync(int id)
    //{

    //    return true;
    //}

    public async Task<Category> GetByIdAsync(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null) return null;
        return category;
    }

    public async Task<Category> UpdateAsync(UpdateCategoryVM updateCategoryVM)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == updateCategoryVM.Id);
        if (category == null) return null;

        category.Name = updateCategoryVM.Name;
        await _context.SaveChangesAsync();
        return category;
    }
}
