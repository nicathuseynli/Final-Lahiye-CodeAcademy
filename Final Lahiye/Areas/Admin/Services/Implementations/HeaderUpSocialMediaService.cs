using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.HeaderUp;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class HeaderUpSocialMediaService :IHeaderUpSocialMediaService
{
    private readonly AppDbContext _context;

    public HeaderUpSocialMediaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CreateHeaderUpSocialMediaVM createHeaderUpSocialMediaVM)
    {
        HeaderUpSocialMedia headerUpSocialMedia = new()
        {
            FacebookLink = createHeaderUpSocialMediaVM.FacebookLink,
            InstagramLink = createHeaderUpSocialMediaVM.InstagramLink,
            TwitterLink = createHeaderUpSocialMediaVM.TwitterLink,
        };
        await _context.HeaderUpSocialMedias.AddAsync(headerUpSocialMedia);
        await _context.SaveChangesAsync();
    }

    //public async Task<bool> DeleteAsync(int id)
    //{

    //    return true;
    //}

    public async Task<HeaderUpSocialMedia> GetByIdAsync(int id)
    {
        var headerUpSocialMedia = await _context.HeaderUpSocialMedias.FirstOrDefaultAsync();
        if (headerUpSocialMedia == null) return null;
        return headerUpSocialMedia;
    }

    public async Task<HeaderUpSocialMedia> UpdateAsync(UpdateHeaderUpSocialMediaVM updateHeaderUpSocialMediaVM)
    {
        var headerUpSocialMedia = await _context.HeaderUpSocialMedias.FirstOrDefaultAsync(x => x.Id == updateHeaderUpSocialMediaVM.Id);
        if (headerUpSocialMedia == null) return null;

        headerUpSocialMedia.FacebookLink = updateHeaderUpSocialMediaVM.FacebookLink;
        headerUpSocialMedia.TwitterLink = updateHeaderUpSocialMediaVM.TwitterLink;
        headerUpSocialMedia.InstagramLink = updateHeaderUpSocialMediaVM.InstagramLink;

        await _context.SaveChangesAsync();
        return headerUpSocialMedia;
    }
}
