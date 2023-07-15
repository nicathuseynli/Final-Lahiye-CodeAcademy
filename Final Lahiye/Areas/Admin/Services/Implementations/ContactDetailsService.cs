using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Contact;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class ContactDetailsService : IContactDetailsService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ContactDetailsService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task CreateAsync(CreateContactDetailsVM createContactDetailsVM)
    {
        ContactDetails details = new()
        {
            ByAddress = createContactDetailsVM.ByAddress,
            ByEmail = createContactDetailsVM.ByEmail,
            ByPhone = createContactDetailsVM.ByPhone,
        };
        await _context.ContactDetailss.AddAsync(details);
        await _context.SaveChangesAsync();
    }

    //public async Task<bool> DeleteAsync(int id)
    //{

    //    return true;
    //}

    public async Task<ContactDetails> GetByIdAsync(int id)
    {
        var details = await _context.ContactDetailss.FirstOrDefaultAsync(x => x.Id == id);
        if (details == null) return null;
        return details;
    }

    public async Task<ContactDetails> UpdateAsync(UpdateContactDetailsVM updateContactDetailsVM)
    {
        var details = await _context.ContactDetailss.FirstOrDefaultAsync(x => x.Id == updateContactDetailsVM.Id);
        if (details == null) return null;

        details.ByAddress = updateContactDetailsVM.ByAddress;
        details.ByEmail = updateContactDetailsVM.ByEmail;
        details.ByPhone = updateContactDetailsVM.ByPhone;

        await _context.SaveChangesAsync();
        return details;
    }

}