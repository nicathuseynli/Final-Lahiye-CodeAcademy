using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.HeaderUp;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class BioService :IBioService
{
    private readonly AppDbContext _context;

    public BioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CreateBioVM createBioVM)
    {
        Bio bio = new()
        {
            FacebookLink = createBioVM.FacebookLink,
            InstagramLink = createBioVM.InstagramLink,
            TwitterLink = createBioVM.TwitterLink,
            Text = createBioVM.Text,
        };
        await _context.Bios.AddAsync(bio);
        await _context.SaveChangesAsync();
    }

    //public async Task<bool> DeleteAsync(int id)
    //{

    //    return true;
    //}

    public async Task<Bio> GetByIdAsync(int id)
    {
        var headerUpSocialMedia = await _context.Bios.FirstOrDefaultAsync();
        if (headerUpSocialMedia == null) return null;
        return headerUpSocialMedia;
    }

    public async Task<Bio> UpdateAsync(UpdateBioVM updateBioVM)
    {
        var bio = await _context.Bios.FirstOrDefaultAsync(x => x.Id == updateBioVM.Id);
        if (bio == null) return null;

        bio.FacebookLink = updateBioVM.FacebookLink;
        bio.TwitterLink = updateBioVM.TwitterLink;
        bio.InstagramLink = updateBioVM.InstagramLink;
        bio.Text = updateBioVM.Text;

        await _context.SaveChangesAsync();
        return bio;
    }
}
