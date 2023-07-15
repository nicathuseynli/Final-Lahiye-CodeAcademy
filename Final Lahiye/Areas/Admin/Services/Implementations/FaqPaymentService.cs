using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.FAQ;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class FaqPaymentService:IFaqPaymentService
{
    private readonly AppDbContext _context;

    public FaqPaymentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CreateFaqPaymentVM createFaqPaymentVM)
    {
        FaqPayment faqPage = new()
        {
            Question = createFaqPaymentVM.Question,
            Answer = createFaqPaymentVM.Answer,
        };
        await _context.FaqPaymentPages.AddAsync(faqPage);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {

        return true;
    }

    public async Task<FaqPayment> GetByIdAsync(int id)
    {
        var faqPage = await _context.FaqPaymentPages.FirstOrDefaultAsync(x => x.Id == id);
        if (faqPage == null) return null;
        return faqPage;
    }

    public async Task<FaqPayment> UpdateAsync(UpdateFaqPaymentVM updateFaqPaymentVM)
    {
        var faqPage = await _context.FaqPaymentPages.FirstOrDefaultAsync(x => x.Id == updateFaqPaymentVM.Id);
        if (faqPage == null) return null;

        faqPage.Question = updateFaqPaymentVM.Question;
        faqPage.Answer = updateFaqPaymentVM.Answer;
        await _context.SaveChangesAsync();
        return faqPage;
    }
}
