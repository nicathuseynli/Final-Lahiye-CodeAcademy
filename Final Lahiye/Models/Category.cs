namespace Final_Lahiye.Models;
public class Category:BaseEntity<int>
{
    public string Name { get; set; }

    //navigation
    public virtual ICollection<HomeProduct> Products { get; set; }
}
