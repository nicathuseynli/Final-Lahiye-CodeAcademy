using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.HeaderUp;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class HeaderUpController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<HeaderUpController> _logger;
    private readonly IHeaderUpTextService _headerUpTextService;
    private readonly IHeaderUpSocialMediaService _headerUpSocialMediaService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public HeaderUpController(AppDbContext context, IWebHostEnvironment webHostEnvironment, 
        ILogger<HeaderUpController> logger, IHeaderUpTextService headerUpTextService,
        IHeaderUpSocialMediaService headerUpSocialMediaService)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;
        _headerUpTextService = headerUpTextService;
        _headerUpSocialMediaService = headerUpSocialMediaService;
    }
    //=======================================Text Area===========================================//
    public async Task<IActionResult> TextIndex()
    {
        var headerupText = await _context.HeaderUpTexts.ToListAsync();
        return View(headerupText);
    }
    [HttpGet]
    public async Task<IActionResult> CreateText()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateText(CreateHeaderUpTextVM createHeaderUpTextVM)
    {
        await _headerUpTextService.CreateAsync(createHeaderUpTextVM);
        return RedirectToAction(nameof(TextIndex));
    }
    [HttpGet]
    public async Task<IActionResult> DetailsText(int id)
    {
        var headerupText = await _headerUpTextService.GetByIdAsync(id);

        return View(headerupText);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteText(int id)
    {
        var headerupText = await _context.HeaderUpTexts.FirstOrDefaultAsync(x => x.Id == id);
        if (headerupText == null) return View();
        _context.HeaderUpTexts.Remove(headerupText);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(TextIndex));
    }
    [HttpGet]
    public async Task<IActionResult> UpdateText(int id)
    {
        var headerupText = await _context.HeaderUpTexts.FirstOrDefaultAsync(x => x.Id == id);
        if (headerupText == null) return NotFound();

        var updateHeaderUpTextVM = new UpdateHeaderUpTextVM()
        {
            Id = headerupText.Id,
            Text = headerupText.Text,
        };

        return View(updateHeaderUpTextVM);
    } 
    [HttpPost]
    public async Task<IActionResult> UpdateText(UpdateHeaderUpTextVM updateHeaderUpTextVM)
    {
        var result = await _headerUpTextService.UpdateAsync(updateHeaderUpTextVM);
        if (result == null) return View();
        return RedirectToAction(nameof(TextIndex));
    }

    //=======================================Text Area===========================================//




    //=======================================SocialMedia Area===========================================//


    public async Task<IActionResult> SocialMediaIndex()
    {
        var headerupSocial = await _context.HeaderUpSocialMedias.ToListAsync();
        return View(headerupSocial);
    }

    [HttpGet]
    public IActionResult CreateSocialMedia()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSocialMedia(CreateHeaderUpSocialMediaVM createHeaderUpSocialMediaVM)
    {
        await _headerUpSocialMediaService.CreateAsync(createHeaderUpSocialMediaVM);
        return RedirectToAction(nameof(SocialMediaIndex));
    }

    [HttpGet]
    public async Task<IActionResult> DetailsSocialMedia (int id)
    {
        var headerUpSocialMedia = await _headerUpSocialMediaService.GetByIdAsync(id);
        return View(headerUpSocialMedia);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteSocialMedia(int id)
    {
        var headerUpSocialMedia = await _context.HeaderUpSocialMedias.FirstOrDefaultAsync(x => x.Id == id);
        if (headerUpSocialMedia == null) return View();

        _context.HeaderUpSocialMedias.Remove(headerUpSocialMedia);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(SocialMediaIndex));
    }

    [HttpGet]
    public async Task<IActionResult> UpdateSocialMedia(int id)
    {
        var headerUpSocialMedia = await _context.HeaderUpSocialMedias.FirstOrDefaultAsync(x => x.Id == id);
        if (headerUpSocialMedia == null) return NotFound();

        var updateheaderUpSocialMediaVM = new UpdateHeaderUpSocialMediaVM()
        {
            Id = headerUpSocialMedia.Id,
            FacebookLink = headerUpSocialMedia.FacebookLink,
            TwitterLink = headerUpSocialMedia.TwitterLink,
            InstagramLink = headerUpSocialMedia.InstagramLink,
        };
        return View(updateheaderUpSocialMediaVM);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateSocialMedia(UpdateHeaderUpSocialMediaVM updateHeaderUpSocialMediaVM)
    {
        var result = await _headerUpSocialMediaService.UpdateAsync(updateHeaderUpSocialMediaVM);
        if (result == null) return View();
        return RedirectToAction(nameof(SocialMediaIndex));
    }

    //=======================================SocialMedia Area===========================================//

}
