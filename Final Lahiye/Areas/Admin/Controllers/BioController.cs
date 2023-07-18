using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.HeaderUp;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class BioController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<BioController> _logger;
    private readonly IBioService _bioService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public BioController(AppDbContext context, IWebHostEnvironment webHostEnvironment, 
        ILogger<BioController> logger,IBioService bioService)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
        _bioService = bioService;
    }
    public async Task<IActionResult> SocialMediaIndex()
    {
        var headerupSocial = await _context.Bios.ToListAsync();
        return View(headerupSocial);
    }

    [HttpGet]
    public IActionResult CreateSocialMedia()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSocialMedia(CreateBioVM createHeaderUpSocialMediaVM)
    {
        await _bioService.CreateAsync(createHeaderUpSocialMediaVM);
        return RedirectToAction(nameof(SocialMediaIndex));
    }

    [HttpGet]
    public async Task<IActionResult> DetailsSocialMedia (int id)
    {
        var headerUpSocialMedia = await _bioService.GetByIdAsync(id);
        return View(headerUpSocialMedia);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteSocialMedia(int id)
    {
        var bio = await _context.Bios.FirstOrDefaultAsync(x => x.Id == id);
        if (bio == null) return View();

        _context.Bios.Remove(bio);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(SocialMediaIndex));
    }

    [HttpGet]
    public async Task<IActionResult> UpdateSocialMedia(int id)
    {
        var bio = await _context.Bios.FirstOrDefaultAsync(x => x.Id == id);
        if (bio == null) return NotFound();

        var updateBioVM = new UpdateBioVM()
        {
            Id = bio.Id,
            FacebookLink = bio.FacebookLink,
            TwitterLink = bio.TwitterLink,
            InstagramLink = bio.InstagramLink,
            Text = bio.Text,
        };
        return View(updateBioVM);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateSocialMedia(UpdateBioVM updateBioVM)
    {
        var result = await _bioService.UpdateAsync(updateBioVM);
        if (result == null) return View();
        return RedirectToAction(nameof(SocialMediaIndex));
    }

    //=======================================SocialMedia Area===========================================//

}
