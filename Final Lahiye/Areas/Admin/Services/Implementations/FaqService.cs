using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.FAQ;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class FaqService : IFaqService
{
    private readonly AppDbContext _context;

    public FaqService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CreateFaqPageVM createFaqPageVM)
    {
        FaqPage faqPage = new()
        {
            Question = createFaqPageVM.Question,
            Answer = createFaqPageVM.Answer,
        };
        await _context.FaqPages.AddAsync(faqPage);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {

        return true;
    }

    public async Task<FaqPage> GetByIdAsync(int id)
    {
        var faqPage = await _context.FaqPages.FirstOrDefaultAsync(x => x.Id == id);
        if (faqPage == null)  return null;
        return faqPage;
    }

    public async Task<FaqPage> UpdateAsync(UpdateFaqPageVM updateFaqPageVM)
    {
        var faqPage = await _context.FaqPages.FirstOrDefaultAsync(x => x.Id == updateFaqPageVM.Id);
        if (faqPage == null) return null;

        faqPage.Question = updateFaqPageVM.Question;
        faqPage.Answer = updateFaqPageVM.Answer;
        await _context.SaveChangesAsync();
        return faqPage;
    }
}
