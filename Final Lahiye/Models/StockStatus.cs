namespace Final_Lahiye.Models;
public class StockStatus:BaseEntity<int>
{
    public bool InStock { get; set; }
    //navigation
    public virtual ICollection<Product> Products { get; set; }
}
