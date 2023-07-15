using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class BannerService:IBannerService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly AppDbContext _context;

    public BannerService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task CreateAsync(CreateBannerVM createbannerVM)
    {
        string filename = Guid.NewGuid().ToString() + "_" + createbannerVM.BannerPhoto.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createbannerVM.BannerPhoto.CopyToAsync(stream);

        Banner banner = new()
        {
            Title = createbannerVM.Title,
            BannerImage = filename
        };
        await _context.Banners.AddAsync(banner);
        await _context.SaveChangesAsync();
    }

    //public async Task<bool> DeleteAsync(int id)
    //{

    //    return true;
    //}

    public async Task<Banner> GetByIdAsync(int id)
    {
        var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == id);
        if (banner == null) return null;
        return banner;
    }

    public async Task<Banner> UpdateAsync(UpdateBannerVM updateBannerVM)
    {
        var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == updateBannerVM.Id);
        if (banner == null) return null;

        if (updateBannerVM.BannerPhoto != null)
        {
            #region Create NewImage
            if (!updateBannerVM.BannerPhoto.ContentType.Contains("image/"))  return null;

            if (updateBannerVM.BannerPhoto.Length / 1024 > 1000) return null;

            string filename = Guid.NewGuid().ToString() + " _ " + updateBannerVM.BannerPhoto.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateBannerVM.BannerPhoto.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", banner.BannerImage);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            banner.BannerImage = filename;
            #endregion
        }
        banner.Title = updateBannerVM.Title;
        await _context.SaveChangesAsync();
        return banner;
    }
}
