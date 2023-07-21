using System.ComponentModel.DataAnnotations;

namespace Final_Lahiye.Models.FormModel
{
    public class ContactFormModel : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
