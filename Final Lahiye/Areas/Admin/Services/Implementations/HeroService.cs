using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class HeroService:IHeroService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public HeroService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task CreateAsync(CreateHeroVM createHeroVM)
    {
        string filename = Guid.NewGuid().ToString() + "_" + createHeroVM.HeroPhoto.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createHeroVM.HeroPhoto.CopyToAsync(stream);

        Hero hero = new()
        {
            Title = createHeroVM.Title,
            Description = createHeroVM.Description,
            HeroImage = filename
        };
        await _context.Heros.AddAsync(hero);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var hero = await _context.Heros.FirstOrDefaultAsync(x => x.Id == id);
        if (hero == null) return false;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", hero.HeroImage);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.Heros.Remove(hero);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Hero> GetByIdAsync(int id)
    {
        var hero = await _context.Heros.FirstOrDefaultAsync(x => x.Id == id);
        if (hero == null) return null;
        return hero;
    }

    public async Task<Hero> UpdateAsync(UpdateHeroVM updateHeroVM)
    {
        var hero = await _context.Heros.FirstOrDefaultAsync(x => x.Id == updateHeroVM.Id);
        if (hero == null) return null;

        if (updateHeroVM.HeroPhoto != null)
        {
            #region Create NewImage
            if (!updateHeroVM.HeroPhoto.ContentType.Contains("image/"))
                return null;

            if (updateHeroVM.HeroPhoto.Length / 1024 > 1000)
                return null;

            string filename = Guid.NewGuid().ToString() + " _ " + updateHeroVM.HeroPhoto.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateHeroVM.HeroPhoto.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", hero.HeroImage);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            hero.HeroImage = filename;
            #endregion
        }
        hero.Description = updateHeroVM.Description;
        hero.Title = updateHeroVM.Title;
        await _context.SaveChangesAsync();
        return hero;
    }
}
