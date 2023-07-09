namespace Final_Lahiye.Models;
public class Colour:BaseEntity<int>
{
    public string Name { get; set; }
    //navigation
    public virtual ICollection<HomeProduct> Products { get; set; }
}
