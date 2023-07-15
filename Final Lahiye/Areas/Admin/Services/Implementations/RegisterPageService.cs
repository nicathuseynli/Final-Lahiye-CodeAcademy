using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.RegisterPage;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class RegisterPageService : IRegisterPageService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public RegisterPageService(IWebHostEnvironment webHostEnvironment, AppDbContext context)
    {
        _webHostEnvironment = webHostEnvironment;
        _context = context;
    }

    public async Task CreateAsync(CreateRegisterPageVM createRegisterPageVM)
    {
        string filename = Guid.NewGuid().ToString() + "_" + createRegisterPageVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createRegisterPageVM.Photo.CopyToAsync(stream);

        RegisterPage registerPage = new()
        {
            Description = createRegisterPageVM.Description,
            Image = filename
        };
        await _context.RegisterPages.AddAsync(registerPage);
        await _context.SaveChangesAsync();
        
    }

    //public async Task<bool> DeleteAsync(int id)
    //{

    //    return true;
    //}

    public async Task<RegisterPage> GetByIdAsync(int id)
    {
        var registerPage = await _context.RegisterPages.FirstOrDefaultAsync(x => x.Id == id);
        if (registerPage == null) return null;
        return registerPage;
    }

    public async Task<RegisterPage> UpdateAsync(UpdateRegisterPageVM updateRegisterVM)
    {

        var registerPage = await _context.RegisterPages.FirstOrDefaultAsync(x => x.Id == updateRegisterVM.Id);
        if (registerPage == null) return null;

        if (updateRegisterVM.Photo != null)
        {
            #region Create NewImage
            if (!updateRegisterVM.Photo.ContentType.Contains("image/"))
                return null;

            if (updateRegisterVM.Photo.Length / 1024 > 1000)
                return null;

            string filename = Guid.NewGuid().ToString() + " _ " + updateRegisterVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateRegisterVM.Photo.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", registerPage.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            registerPage.Image = filename;
            #endregion
        }
        registerPage.Description = updateRegisterVM.Description;
        await _context.SaveChangesAsync();
        return registerPage;
    }
}
