using System.ComponentModel.DataAnnotations;
using System.Data;

namespace retail.Models
{
    public class Order
    {

        public int OrderID { get; set; }

        public DateTime Date {  get; set; }

        public int CustomerID { get; set; }

        public Customer Customer { get; set; }

        public IList<OrderDetail> Details { get; set; } = new List<OrderDetail>();

        [Display(Name = "Payment Status")]
        public string PaymentStatus { get; set; }

        public decimal Paid { get; set; }

        public decimal getTotal()
        {
            decimal total = 0;
            foreach (var detail in Details)
            {
                total += detail.getPrice();
            }
            return total;
        }

    }
}
