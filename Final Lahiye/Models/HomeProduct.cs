using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Lahiye.Models;
public class HomeProduct :BaseEntity<int>
{
    public string Header { get; set; }

    public string Name { get; set; }

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

    public int SalePercent { get; set; }

    public decimal LastPrice { get; set; }

    public decimal CurrentPrice { get; set; }

    public int Count { get; set; }

    public string Image { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }


    //Navigation
    public virtual Category Category { get; set; }

    public virtual Colour Colour { get; set; }

    public int CategoryId { get; set; }

    public int ColourId { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }

}
