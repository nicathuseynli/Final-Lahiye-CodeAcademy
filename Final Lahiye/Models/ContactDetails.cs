namespace Final_Lahiye.Models;
public class ContactDetails:BaseEntity<int>
{
    public string ByAddress { get; set; }
    public string ByEmail { get; set; }
    public string ByPhone { get; set; }
}
