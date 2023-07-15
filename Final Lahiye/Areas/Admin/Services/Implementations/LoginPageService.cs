using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.LoginPage;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class LoginPageService : ILoginPageService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public LoginPageService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task CreateAsync(CreateLoginPageVM createLoginVM)
    {
        string filename = Guid.NewGuid().ToString() + "_" + createLoginVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createLoginVM.Photo.CopyToAsync(stream);

        LoginPage loginPage = new()
        {
            Description = createLoginVM.Description,
            Image = filename
        };
        await _context.LoginPages.AddAsync(loginPage);
        await _context.SaveChangesAsync();
    }

    //public async Task<bool> DeleteAsync(int id)
    //{

    //    return true;
    //}

    public async Task<LoginPage> GetByIdAsync(int id)
    {
        var loginpage = await _context.LoginPages.FirstOrDefaultAsync(x => x.Id == id);
        if (loginpage == null) return null;
        return loginpage;
    }

    public async Task<LoginPage> UpdateAsync(UpdateLoginPageVM updateLoginVM)
    {

        var loginpage = await _context.LoginPages.FirstOrDefaultAsync(x => x.Id == updateLoginVM.Id);
        if (loginpage == null) return null;

        if (updateLoginVM.Photo != null)
        {
            #region Create NewImage
            if (!updateLoginVM.Photo.ContentType.Contains("image/"))
                return null;

            if (updateLoginVM.Photo.Length / 1024 > 1000)
                return null;

            string filename = Guid.NewGuid().ToString() + " _ " + updateLoginVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateLoginVM.Photo.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", loginpage.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            loginpage.Image = filename;
            #endregion
        }
        loginpage.Description = updateLoginVM.Description;
        await _context.SaveChangesAsync();
        return loginpage;
    }
}
