using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Lahiye.Models;
public class Product :BaseEntity<int>
{
    public string Name { get; set; }

    public int SalePercent { get; set; }

    public decimal LastPrice { get; set; }

    public decimal CurrentPrice { get; set; }

    public string Image { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; }


    //Navigation
    public virtual Category Category { get; set; }

    public virtual Colour Colour { get; set; }

    public virtual StockStatus StockStatus { get; set; }

    public int CategoryId { get; set; }

    public int ColourId { get; set; }

    public int StockStatusId { get; set; }
}
