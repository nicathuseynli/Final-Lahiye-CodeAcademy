using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class TestimonialController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<TestimonialController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public TestimonialController(AppDbContext context, ILogger<TestimonialController> logger, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var testimonial = await _context.Testimonials.ToListAsync();
        return View(testimonial);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTestimonialVM createTestimonialVM)
    {
        if (!ModelState.IsValid) return View();

        if (!createTestimonialVM.Photo.ContentType.Contains("image/")) return View();

        if (createTestimonialVM.Photo.Length / 1024 > 500) return View();

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
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var testimonial = await _context.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
        if (testimonial == null)
            return NotFound();
        return View(testimonial);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var testimonial = await _context.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
        if (testimonial == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", testimonial.Image);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.Testimonials.Remove(testimonial);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var testimonial = await _context.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
        if (testimonial == null) return NotFound();

        var updateTestimonial = new UpdateTestimonialVM()
        {
            Id = testimonial.Id,
            Title = testimonial.Title,
            Header = testimonial.Header,
            Image = testimonial.Image,
        };
        return View(updateTestimonial);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateTestimonialVM updateTestimoniaVMl)
    {

        var testimonial = await _context.Testimonials.FirstOrDefaultAsync(x => x.Id == updateTestimoniaVMl.Id);
        if (testimonial == null) return NotFound();

        if (updateTestimoniaVMl.Photo != null)
        {
            #region Create NewImage
            if (!updateTestimoniaVMl.Photo.ContentType.Contains("image/"))
                return View();

            if (updateTestimoniaVMl.Photo.Length / 1024 > 1000)
                return View();

            string filename = Guid.NewGuid().ToString() + " _ " + updateTestimoniaVMl.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateTestimoniaVMl.Photo.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", testimonial.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            testimonial.Image = filename;
            #endregion
        }
        testimonial.Title = updateTestimoniaVMl.Title;
        testimonial.Header = updateTestimoniaVMl.Header;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
