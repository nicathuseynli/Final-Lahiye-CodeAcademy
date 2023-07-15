using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.ShopPage;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class ColourService:IColourService
{
    private readonly AppDbContext _context;

    public ColourService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CreateColourVM createColourVM)
    {

        Colour colour = new()
        {
            Name = createColourVM.Name,
        };
        await _context.Colours.AddAsync(colour);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {

        return true;
    }

    public async Task<Colour> GetByIdAsync(int id)
    {
        var colour = await _context.Colours.FirstOrDefaultAsync(x => x.Id == id);
        if (colour == null) return null;
        return colour;
    }

    public async Task<Colour> UpdateAsync(UpdateColourVM updateColourVM)
    {

        var colour = await _context.Colours.FirstOrDefaultAsync(x => x.Id == updateColourVM.Id);
        if (colour == null) return null;

        colour.Name = updateColourVM.Name;
        await _context.SaveChangesAsync();
        return colour;
    }
}
