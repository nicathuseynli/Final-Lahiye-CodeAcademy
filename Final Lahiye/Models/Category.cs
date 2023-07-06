namespace Final_Lahiye.Models;
public class Category:BaseEntity<int>
{
    public int Name { get; set; }

    //navigation
    public virtual ICollection<Product> Products { get; set; }
}
