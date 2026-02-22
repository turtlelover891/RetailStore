namespace retail.Models
{
    public class Cart
    {

        public int CartID { get; set; }

        public int CustomerID { get; set; }

        public Customer Customer { get; set; }
        public IList<OrderDetail> Details { get; set; } = new List<OrderDetail>();

        public decimal getTotal()
        {
            decimal total = 0;
            foreach (var detail in Details)
            {
                total += detail.getPrice();
            }
            return total;
        }

        public Order convertToOrder()
        {
            var order = new Order
            {
                CustomerID = CustomerID,
                Customer = Customer,
                Details = Details.Select(d => new OrderDetail
                {
                    ProductID = d.ProductID,
                    Product = d.Product,
                    Quantity = d.Quantity,
                }).ToList()
            };

            return order;
        }

    }
}
