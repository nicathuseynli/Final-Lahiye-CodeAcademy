using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Blog;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class BlogService : IBlogService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly AppDbContext _context;

    public BlogService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task CreateAsync(CreateBlogPageVM createBlogPageVM)
    {
        //string filename = Guid.NewGuid().ToString() + "_" + createBlogPageVM.Photo.FileName;

        //string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        //using FileStream stream = new FileStream(path, FileMode.Create);

        //await createBlogPageVM.Photo.CopyToAsync(stream);
        //Blog blog = new()
        //{
        //    Title = createBlogPageVM.Title ,
        //    Image = filename,
        //};
        //await _context.Blogs.AddAsync(blog);
        //await _context.SaveChangesAsync();
        //return false;

        //if (createBlogPageVM.Photo.Length / 1024 > 500)
        //    return false;

    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Blog> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(UpdateBlogPageVM updateBlogPageVM)
    {

        //var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == updateBlogPageVM.Id);
        //if (blog == null) 
        //    return false;

        //if (updateBlogPageVM.Photo != null)
        //{
        //    #region Create NewImage
        //    if (!updateBlogPageVM.Photo.ContentType.Contains("image/"))
        //        return false;

        //    if (updateBlogPageVM.Photo.Length / 1024 > 1000)
        //        return false;

        //    string filename = Guid.NewGuid().ToString() + " _ " + updateBlogPageVM.Photo.FileName;
        //    string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        //    using FileStream stream = new FileStream(path, FileMode.Create);

        //    await updateBlogPageVM.Photo.CopyToAsync(stream);
        //    #endregion

        //    #region DeleteOldImage
        //    string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", blog.Image);
        //    if (System.IO.File.Exists(oldPath))
        //        System.IO.File.Delete(oldPath);
        //    blog.Image = filename;
        //    #endregion
        //}
        //blog.Title = updateBlogPageVM.Title;
        //blog.AuthorId = updateBlogPageVM.AuthorId;
        //await _context.SaveChangesAsync();
        return true;
    }
}
