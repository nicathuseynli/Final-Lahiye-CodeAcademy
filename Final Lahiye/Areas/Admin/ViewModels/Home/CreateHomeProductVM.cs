using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Areas.Admin.ViewModels.Home;
public class CreateHomeProductVM
{
    [Required]
    public string Name { get; set; }

    public int SalePercent { get; set; }

    public decimal LastPrice { get; set; }

    public string Header { get; set; }

    [Required]
    public int Rating { get; set; }

    [Required]
    public string SKUCode { get; set; }

    [Required]
    public string DeliveryInfo { get; set; }

    [Required]
    public string ShippingInfo { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Style { get; set; }

    [Required]
    public string RoomType { get; set; }   
    
    [Required]
    public int PackCount { get; set; }

    [Required]
    public string IdealFor { get; set; }

    [Required]
    public int Capacity { get; set; }

    [Required]
    public string Shape { get; set; }   

    public bool InStock { get; set; }

    [Required]
    public decimal CurrentPrice { get; set; }

    public int CategoryId { get; set; }

    public int ColourId { get; set; }

    [Required]
    public IFormFile Photo { get; set; }

}
