namespace retail.Models
{
    public class OrderDetail
    {

        public int OrderDetailID { get; set; }

        public int? OrderID { get; set; }

        public Order? Order { get; set; }

        public int? CartID { get; set; }

        public Cart? Cart { get; set; }

        public int ProductID { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal getPrice()
        {
            return Product.Price * Quantity;
        }

    }
}
