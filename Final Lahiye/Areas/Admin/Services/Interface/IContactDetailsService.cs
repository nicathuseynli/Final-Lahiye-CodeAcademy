using Final_Lahiye.Areas.Admin.ViewModels.Contact;
using Final_Lahiye.Models;

namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface IContactDetailsService
{
    Task CreateAsync(CreateContactDetailsVM createContactDetailsVM);

    Task<ContactDetails> GetByIdAsync(int id);

    //Task<bool> DeleteAsync(int id);

    Task<ContactDetails> UpdateAsync(UpdateContactDetailsVM updateContactDetailsVM);
}
