using System.Diagnostics.CodeAnalysis;

namespace Final_Lahiye.Areas.Admin.ViewModels.Home;
public class UpdateHomeProductVM
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Header { get; set; }

    public int SalePercent { get; set; }

    public decimal LastPrice { get; set; }

    public bool InStock { get; set; }

    public decimal CurrentPrice { get; set; }

    public int Rating { get; set; }

    public string SKUCode { get; set; }

    public string DeliveryInfo { get; set; }

    public string ShippingInfo { get; set; }

    public string Description { get; set; }

    public string Style { get; set; }

    public string RoomType { get; set; }

    public int PackCount { get; set; }

    public string IdealFor { get; set; }

    public int Capacity { get; set; }

    public string Shape { get; set; }

    public int CategoryId { get; set; }

    public int ColourId { get; set; }

    [AllowNull]
    public IFormFile Photo { get; set; }

    public string Image { get; set; }
}
