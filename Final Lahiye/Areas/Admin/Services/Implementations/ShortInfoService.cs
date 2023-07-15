using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class ShortInfoService :IShortInfoService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ShortInfoService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task CreateAsync(CreateShortInfoVM createshortInfoVM)
    {
        string filename = Guid.NewGuid().ToString() + "_" + createshortInfoVM.IconPhoto.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createshortInfoVM.IconPhoto.CopyToAsync(stream);

        ShortInformation shortInfo = new()
        {
            Title = createshortInfoVM.Title,
            Information = createshortInfoVM.Information,
            Icon = filename
        };
        await _context.ShortInformations.AddAsync(shortInfo);
        await _context.SaveChangesAsync();
    }

    //public async Task<bool> DeleteAsync(int id)
    //{

    //    return true;
    //}

    public async Task<ShortInformation> GetByIdAsync(int id)
    {
        var shortInfo = await _context.ShortInformations.FirstOrDefaultAsync(x => x.Id == id);
        if (shortInfo == null) return null;
        return shortInfo;
    }

    public async Task<ShortInformation> UpdateAsync(UpdateShortInfoVM updateshortInfoVM)
    {

        var shortInfo = await _context.ShortInformations.FirstOrDefaultAsync(x => x.Id == updateshortInfoVM.Id);
        if (shortInfo == null) return null;

        if (updateshortInfoVM.IconPhoto != null)
        {
            #region Create NewImage
            if (!updateshortInfoVM.IconPhoto.ContentType.Contains("image/"))
                return null;

            if (updateshortInfoVM.IconPhoto.Length / 1024 > 1000)
                return null;

            string filename = Guid.NewGuid().ToString() + " _ " + updateshortInfoVM.IconPhoto.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateshortInfoVM.IconPhoto.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", shortInfo.Icon);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            shortInfo.Icon = filename;
            #endregion
        }
        shortInfo.Information = updateshortInfoVM.Information;
        shortInfo.Title = updateshortInfoVM.Title;
        await _context.SaveChangesAsync();
        return shortInfo;
    }
}
