using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.HeaderUp;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class HeaderUpTextService : IHeaderUpTextService
{
    private readonly AppDbContext _context;

    public HeaderUpTextService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CreateHeaderUpTextVM createHeaderUpTextVM)
    {
        HeaderUpText headerupText = new()
        {
            Text = createHeaderUpTextVM.Text,
        };
        await _context.HeaderUpTexts.AddAsync(headerupText);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var headerupText = await _context.HeaderUpTexts.FirstOrDefaultAsync(x => x.Id == id);
        if (headerupText == null) return false;
        _context.HeaderUpTexts.Remove(headerupText);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<HeaderUpText> GetByIdAsync(int id)
    {
        var headerupText = await _context.HeaderUpTexts.FirstOrDefaultAsync(x => x.Id == id);
        if (headerupText == null) return null;
        return headerupText;
    }

    public async Task<HeaderUpText> UpdateAsync(UpdateHeaderUpTextVM updateHeaderUpTextVM)
    {
        var headerupText = await _context.HeaderUpTexts.FirstOrDefaultAsync(x => x.Id == updateHeaderUpTextVM.Id);
        if (headerupText == null) return null;

        headerupText.Text = updateHeaderUpTextVM.Text;
        await _context.SaveChangesAsync();
        return headerupText;
    }
}
