using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class TestimonialService : ITestimonialService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public TestimonialService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task CreateAsync(CreateTestimonialVM createTestimonialVM)
    {
        string filename = Guid.NewGuid().ToString() + "_" + createTestimonialVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createTestimonialVM.Photo.CopyToAsync(stream);

        Testimonial testimonial = new()
        {
            Title = createTestimonialVM.Title,
            Header = createTestimonialVM.Header,
            Image = filename
        };
        await _context.Testimonials.AddAsync(testimonial);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var testimonial = await _context.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
        if (testimonial == null) return false;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", testimonial.Image);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.Testimonials.Remove(testimonial);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Testimonial> GetByIdAsync(int id)
    {
        var testimonial = await _context.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
        if (testimonial == null) return null;
        return testimonial;
    }

    public async Task<Testimonial> UpdateAsync(UpdateTestimonialVM updateTestimonialVM)
    {
        var testimonial = await _context.Testimonials.FirstOrDefaultAsync(x => x.Id == updateTestimonialVM.Id);
        if (testimonial == null) return null;

        if (updateTestimonialVM.Photo != null)
        {
            #region Create NewImage
            if (!updateTestimonialVM.Photo.ContentType.Contains("image/"))
                return null;

            if (updateTestimonialVM.Photo.Length / 1024 > 1000)
                return null;

            string filename = Guid.NewGuid().ToString() + " _ " + updateTestimonialVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateTestimonialVM.Photo.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", testimonial.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            testimonial.Image = filename;
            #endregion
        }
        testimonial.Title = updateTestimonialVM.Title;
        testimonial.Header = updateTestimonialVM.Header;
        await _context.SaveChangesAsync();
        return testimonial;
    }
}
