using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.FAQ;
using Final_Lahiye.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Controllers;
[Area("Admin")]
public class FAQPaymentController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<FAQPaymentController> _logger;
    private readonly IFaqPaymentService _faqPaymentservice;

    public FAQPaymentController(AppDbContext context, ILogger<FAQPaymentController> logger, IFaqPaymentService faqPaymentservice)
    {
        _context = context;
        _logger = logger;
        _faqPaymentservice = faqPaymentservice;
    }

    public async Task<IActionResult> Index()
    {
        var faqPage = await _context.FaqPaymentPages.ToListAsync();
        return View(faqPage);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateFaqPaymentVM createFaqPageVM)
    {
        if (!ModelState.IsValid) return View();

        await _faqPaymentservice.CreateAsync(createFaqPageVM);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var faqPage = await _faqPaymentservice.GetByIdAsync(id);

        return View(faqPage);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var faqPage = await _context.FaqPaymentPages.FirstOrDefaultAsync(x => x.Id == id);
        if (faqPage == null)
            return View();

        _context.FaqPaymentPages.Remove(faqPage);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var faqPage = await _context.FaqPaymentPages.FirstOrDefaultAsync(x => x.Id == id);
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
    public async Task<IActionResult> Update(UpdateFaqPaymentVM updateFaqPaymentVM)
    {
        var result = await _faqPaymentservice.UpdateAsync(updateFaqPaymentVM);
        if (result == null) return View();
        return RedirectToAction(nameof(Index));
    }
}
