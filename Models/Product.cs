using System.ComponentModel.DataAnnotations;

namespace retail.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Description")]
        public string ProductDescription { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

    }
}
