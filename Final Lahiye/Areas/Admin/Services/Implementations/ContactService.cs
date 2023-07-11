using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Contact;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class ContactService : IContactService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ContactService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task CreateAsync(CreateContactVM createContactVM)
    {
        string filename = Guid.NewGuid().ToString() + "_" + createContactVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createContactVM.Photo.CopyToAsync(stream);

        Contact contact = new()
        {
            Magazine = createContactVM.Magazine,
            City = createContactVM.City,
            Description = createContactVM.Description,
            Image = filename
        };
        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        if (contact == null) return false;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", contact.Image);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Contact> GetByIdAsync(int id)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        if (contact == null) return null;
        return contact;
    }

    public async Task<Contact> UpdateAsync(UpdateContactVM updateContactVM)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == updateContactVM.Id);
        if (contact == null) return null;

        if (updateContactVM.Photo != null)
        {
            #region Create NewImage
            if (!updateContactVM.Photo.ContentType.Contains("image/"))
                return null;

            if (updateContactVM.Photo.Length / 1024 > 1000)
                return null;

            string filename = Guid.NewGuid().ToString() + " _ " + updateContactVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateContactVM.Photo.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", contact.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            contact.Image = filename;
            #endregion
        }
        contact.Magazine = updateContactVM.Magazine;
        contact.City = updateContactVM.City;
        contact.Description = updateContactVM.Description;
        await _context.SaveChangesAsync();
        return contact;
    }
}
