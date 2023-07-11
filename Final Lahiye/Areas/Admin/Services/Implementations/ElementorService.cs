using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class ElementorService : IElementorService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ElementorService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task CreateAsync(CreateElementorWidgetVM createElementorWidgetVM)
    {
        string fileUpname = Guid.NewGuid().ToString() + "_" + createElementorWidgetVM.ElementorUpPhoto.FileName;

        string fileDownname = Guid.NewGuid().ToString() + "_" + createElementorWidgetVM.ElementorDownPhoto.FileName;

        string pathUp = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileUpname);

        string pathDown = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileDownname);

        using FileStream streamUp = new FileStream(pathUp, FileMode.Create);

        using FileStream streamDown = new FileStream(pathDown, FileMode.Create);

        await createElementorWidgetVM.ElementorUpPhoto.CopyToAsync(streamUp);

        await createElementorWidgetVM.ElementorDownPhoto.CopyToAsync(streamDown);

        Elementor elementor = new()
        {
            ElementorUpImage = fileUpname,
            ElementorDownImage = fileDownname,
        };
        await _context.Elementors.AddAsync(elementor);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var elementor = await _context.Elementors.FirstOrDefaultAsync(x => x.Id == id);
        if (elementor == null)
            return false;

        string pathUp = Path.Combine(_webHostEnvironment.WebRootPath, "images", elementor.ElementorUpImage);
        string pathDown = Path.Combine(_webHostEnvironment.WebRootPath, "images", elementor.ElementorDownImage);

        if (System.IO.File.Exists(pathUp)) System.IO.File.Delete(pathUp);
        if (System.IO.File.Exists(pathDown)) System.IO.File.Delete(pathDown);

        System.IO.File.Delete(pathUp);
        System.IO.File.Delete(pathDown);

        _context.Elementors.Remove(elementor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Elementor> GetByIdAsync(int id)
    {
        var elementor = await _context.Elementors.FirstOrDefaultAsync(x => x.Id == id);
        if (elementor == null) return null;
        return elementor;
    }

    public async Task<Elementor> UpdateAsync(UpdateElementorWidgetVM updateElementorWidgetVM)
    {

        var elementor = await _context.Elementors.FirstOrDefaultAsync(x => x.Id == updateElementorWidgetVM.Id);
        if (elementor == null) return null;

        if (updateElementorWidgetVM.ElementorUpPhoto != null && updateElementorWidgetVM.ElementorDownPhoto != null)
        {
            if (!updateElementorWidgetVM.ElementorUpPhoto.ContentType.Contains("image/") || !updateElementorWidgetVM.ElementorDownPhoto.ContentType.Contains("image/"))
                return null;

            if (updateElementorWidgetVM.ElementorUpPhoto.Length / 1024 > 1000 && updateElementorWidgetVM.ElementorDownPhoto.Length / 1024 > 1000)
                return null;

            string filenameUp = Guid.NewGuid().ToString() + " _ " + updateElementorWidgetVM.ElementorUpPhoto.FileName;
            string pathUp = Path.Combine(_webHostEnvironment.WebRootPath, "images", filenameUp);

            string filenameDown = Guid.NewGuid().ToString() + " _ " + updateElementorWidgetVM.ElementorDownPhoto.FileName;
            string pathDown = Path.Combine(_webHostEnvironment.WebRootPath, "images", filenameDown);

            using FileStream streamUp = new FileStream(pathUp, FileMode.Create);
            using FileStream streamDown = new FileStream(pathDown, FileMode.Create);

            await updateElementorWidgetVM.ElementorUpPhoto.CopyToAsync(streamUp);
            await updateElementorWidgetVM.ElementorDownPhoto.CopyToAsync(streamDown);

            string oldPathUp = Path.Combine(_webHostEnvironment.WebRootPath, "images", elementor.ElementorUpImage);
            string oldPathDown = Path.Combine(_webHostEnvironment.WebRootPath, "images", elementor.ElementorDownImage);
            if (System.IO.File.Exists(oldPathUp) && System.IO.File.Exists(oldPathDown))
                System.IO.File.Delete(oldPathUp);
            System.IO.File.Delete(oldPathDown);
            elementor.ElementorUpImage = filenameUp;
            elementor.ElementorDownImage = filenameDown;
        }
        await _context.SaveChangesAsync();
        return elementor;
    }
}
