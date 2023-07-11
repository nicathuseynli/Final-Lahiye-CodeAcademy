using Final_Lahiye.Areas.Admin.ViewModels.Blog;
using Final_Lahiye.Areas.Services.Interface;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Services.Implementations;
public class AuthorService : IAuthorService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly AppDbContext _context;

    public AuthorService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task CreateAsync(CreateAuthorVM createAuthorVM)
    {
        string filename = Guid.NewGuid().ToString() + "_" + createAuthorVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createAuthorVM.Photo.CopyToAsync(stream);
        Author author = new()
        {
            FullName = createAuthorVM.FullName,
            Proffesion = createAuthorVM.Proffession,
            Image = filename,
        };
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
        if(author == null) return false;
        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", author.Image);
        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Author> GetByIdAsync(int id)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
        if (author == null)  
            return null;
        return author;
    }

    public async Task<Author> UpdateAsync(UpdateAuthorVM updateAuthorVM)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == updateAuthorVM.Id);
        if (author == null) 
            return null;

        if (updateAuthorVM.Photo != null)
        {
            #region Create NewImage
            if (!updateAuthorVM.Photo.ContentType.Contains("image/"))
                return null;

            if (updateAuthorVM.Photo.Length / 1024 > 1000)
                return null;

            string filename = Guid.NewGuid().ToString() + " _ " + updateAuthorVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);
    
             await updateAuthorVM.Photo.CopyToAsync(stream);
            #endregion
    
            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", author.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            author.Image = filename;
            #endregion
        }
        author.FullName = updateAuthorVM.FullName;
        author.Proffesion = updateAuthorVM.Proffession;
        await _context.SaveChangesAsync();
        return author;
    }

}
