using Final_Lahiye.Models;
using Final_Lahiye.Models.FormModel;

namespace Final_Lahiye.ViewModels;
public class ContactVM
{
    public ContactDetails ContactDetails { get; set; }
    public List<Contact> Contacts { get; set; }
    public Contact Contact { get; set; }
    public ContactFormModel contactFormModel { get; set; }
}   
