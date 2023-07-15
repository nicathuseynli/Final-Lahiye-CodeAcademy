using Final_Lahiye.Areas.Admin.Services.Interface;
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
    private readonly ITestimonialService _testimonialService;

    public TestimonialController(AppDbContext context, ILogger<TestimonialController> logger, IWebHostEnvironment webHostEnvironment, ITestimonialService testimonialService)
    {
        _context = context;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
        _testimonialService = testimonialService;
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

        await _testimonialService.CreateAsync(createTestimonialVM);

        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var testimonial = await _testimonialService.GetByIdAsync(id);
        return View(testimonial);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var testimonial = await _context.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
        if (testimonial == null) return View();

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
    public async Task<IActionResult> Update(UpdateTestimonialVM updateTestimoniaVM)
    {
        var result = await _testimonialService.UpdateAsync(updateTestimoniaVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
