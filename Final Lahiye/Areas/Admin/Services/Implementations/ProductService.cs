using Final_Lahiye.Areas.Admin.Services.Interface;
using Final_Lahiye.Areas.Admin.ViewModels.Home;
using Final_Lahiye.Data;
using Final_Lahiye.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Lahiye.Areas.Admin.Services.Implementations;
public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task CreateAsync(CreateHomeProductVM createhomeproductVm)
    {
        string filename = Guid.NewGuid().ToString() + "_" + createhomeproductVm.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createhomeproductVm.Photo.CopyToAsync(stream);

        HomeProduct  homeproduct = new()
        {
            Name = createhomeproductVm.Name,
            LastPrice = createhomeproductVm.LastPrice,
            CurrentPrice = createhomeproductVm.CurrentPrice,
            SalePercent = createhomeproductVm.SalePercent,
            Header = createhomeproductVm.Header,
            Rating = createhomeproductVm.Rating,
            SKUCode = createhomeproductVm.SKUCode,
            DeliveryInfo = createhomeproductVm.DeliveryInfo,
            ShippingInfo = createhomeproductVm.ShippingInfo,
            Description = createhomeproductVm.Description,
            Style = createhomeproductVm.Style,
            RoomType = createhomeproductVm.RoomType,
            PackCount = createhomeproductVm.PackCount,
            IdealFor = createhomeproductVm.IdealFor,
            Capacity = createhomeproductVm.Capacity,
            Shape = createhomeproductVm.Shape,
            CategoryId = createhomeproductVm.CategoryId,
            ColourId = createhomeproductVm.ColourId,
            Count = createhomeproductVm.Count,
            Image = filename,
        };
        await _context.Products.AddAsync(homeproduct);
        await _context.SaveChangesAsync();
    }

    //public async Task<bool> DeleteAsync(int id)
    //{

    //    return true;
    //}

    public async Task<HomeProduct> GetByIdAsync(int id)
    {
        var homeproduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (homeproduct == null) return null;
        return homeproduct;
    }

    public async Task<HomeProduct> UpdateAsync(UpdateHomeProductVM updateHomeProductVM)
    {

        var homeproduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == updateHomeProductVM.Id);
        if (homeproduct == null) return null;

        if (updateHomeProductVM.Photo != null)
        {
            if (!updateHomeProductVM.Photo.ContentType.Contains("image/")) return null;

            if (updateHomeProductVM.Photo.Length / 1024 > 500) return null;


            string filename = Guid.NewGuid().ToString() + " _ " + updateHomeProductVM.Photo.FileName;

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateHomeProductVM.Photo.CopyToAsync(stream);

            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", homeproduct.Image);
            if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);

            homeproduct.Image = filename;
        }

        homeproduct.Name = updateHomeProductVM.Name;
        homeproduct.CurrentPrice = updateHomeProductVM.CurrentPrice;
        homeproduct.LastPrice = updateHomeProductVM.LastPrice;
        homeproduct.Header = updateHomeProductVM.Header;
        homeproduct.Rating = updateHomeProductVM.Rating;
        homeproduct.SKUCode = updateHomeProductVM.SKUCode;
        homeproduct.DeliveryInfo = updateHomeProductVM.DeliveryInfo;
        homeproduct.ShippingInfo = updateHomeProductVM.ShippingInfo;
        homeproduct.Description = updateHomeProductVM.Description;
        homeproduct.Style = updateHomeProductVM.Style;
        homeproduct.PackCount = updateHomeProductVM.PackCount;
        homeproduct.RoomType = updateHomeProductVM.RoomType;
        homeproduct.IdealFor = updateHomeProductVM.IdealFor;
        homeproduct.Capacity = updateHomeProductVM.Capacity;
        homeproduct.Shape = updateHomeProductVM.Shape;
        homeproduct.SalePercent = updateHomeProductVM.SalePercent;
        homeproduct.CategoryId = updateHomeProductVM.CategoryId;
        homeproduct.ColourId = updateHomeProductVM.ColourId;
        homeproduct.Count = updateHomeProductVM.Count;
        await _context.SaveChangesAsync();
        return homeproduct;
    }
}
