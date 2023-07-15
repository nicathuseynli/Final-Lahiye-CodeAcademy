using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.FAQ;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class FAQPageController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<FAQPageController> _logger;
    private readonly IFaqService _faqService;

    public FAQPageController(AppDbContext context, ILogger<FAQPageController> logger, IFaqService faqService)
    {
        _context = context;
        _logger = logger;
        _faqService = faqService;
    }

    public async Task<IActionResult> Index()
    {
        var faqPage = await _context.FaqPages.ToListAsync();
        return View(faqPage);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateFaqPageVM createFaqPageVM)
    {
        if (!ModelState.IsValid) return View();

        await _faqService.CreateAsync(createFaqPageVM);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var faqPage = await _faqService.GetByIdAsync(id);

        return View(faqPage);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var faqPage = await _context.FaqPages.FirstOrDefaultAsync(x => x.Id == id);
        if (faqPage == null)
            return View();

        _context.FaqPages.Remove(faqPage);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var faqPage = await _context.FaqPages.FirstOrDefaultAsync(x => x.Id == id);
        if (faqPage == null) return NotFound();

        var updateFaqPageVM = new UpdateFaqPageVM()
        {
            Id = faqPage.Id,
            Question = faqPage.Question,
            Answer = faqPage.Answer,
        };
        return View(updateFaqPageVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateFaqPageVM updateFaqPageVM)
    {
        var result = await _faqService.UpdateAsync(updateFaqPageVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
