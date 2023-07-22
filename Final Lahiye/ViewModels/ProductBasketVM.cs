using Final_Lahiye.Models;
using Final_Lahiye.Models.FormModel;

namespace Final_Lahiye.ViewModels
{
    public class ProductBasketVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }

        public HomeProduct Product { get; set; }

        public CheckoutFormModel checkOutFormModel { get; set; }

        public decimal TotalPrice
        {
            get { return Price * Count; }
        }

    }
}
