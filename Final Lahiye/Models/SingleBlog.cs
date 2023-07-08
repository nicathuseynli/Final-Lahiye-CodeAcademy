namespace Final_Lahiye.Models;
public class SingleBlog :BaseEntity<int>
{

    //navigation
    public Blog Blog { get; set; }

}
