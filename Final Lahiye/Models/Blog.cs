namespace Final_Lahiye.Models;
public class Blog :BaseEntity<int>
{
    public string AuthorName { get; set; }

    public string Profession { get; set; }

    public string Image { get; set; }
}
