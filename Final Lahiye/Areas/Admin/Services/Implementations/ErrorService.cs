using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Error;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class ErrorService : IErrorService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ErrorService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task CreateAsync(CreateErrorPageVM createErrorPageVM)
    {
        string filename = Guid.NewGuid().ToString() + "_" + createErrorPageVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createErrorPageVM.Photo.CopyToAsync(stream);

        ErrorPage errorPage = new()
        {
            Message = createErrorPageVM.Message,
            Image = filename
        };
        await _context.ErrorPages.AddAsync(errorPage);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var errorPage = await _context.ErrorPages.FirstOrDefaultAsync(x => x.Id == id);
        if (errorPage == null)  return false;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", errorPage.Image);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.ErrorPages.Remove(errorPage);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ErrorPage> GetByIdAsync(int id)
    {
        var errorPage = await _context.ErrorPages.FirstOrDefaultAsync(x => x.Id == id);
        if (errorPage == null)  return null;
        return errorPage;
    }

    public async Task<ErrorPage> UpdateAsync(UpdateErrorPageVM updateErrorPageVM)
    {

        var errorPage = await _context.ErrorPages.FirstOrDefaultAsync(x => x.Id == updateErrorPageVM.Id);
        if (errorPage == null) return null;

        if (updateErrorPageVM.Photo != null)
        {
            #region Create NewImage
            if (!updateErrorPageVM.Photo.ContentType.Contains("image/"))
                return null;

            if (updateErrorPageVM.Photo.Length / 1024 > 1000)
                return null;

            string filename = Guid.NewGuid().ToString() + " _ " + updateErrorPageVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateErrorPageVM.Photo.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", errorPage.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            errorPage.Image = filename;
            #endregion
        }
        errorPage.Message = updateErrorPageVM.Message;
        await _context.SaveChangesAsync();
        return errorPage;
    }
}
