using Final_Lahiye.Areas.Admin.ViewModels.Contact;
using Final_Lahiye.Models;
namespace Final_Lahiye.Areas.Admin.Services.Interface;
public interface IContactService
{
    Task CreateAsync(CreateContactVM createContactVM);

    Task<Contact> GetByIdAsync(int id);

    Task<bool> DeleteAsync(int id);

    Task<Contact> UpdateAsync(UpdateContactVM updateContactVM);
}
