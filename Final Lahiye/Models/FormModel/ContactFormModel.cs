namespace Final_Lahiye.Models.FormModel
{
    public class ContactFormModel : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
