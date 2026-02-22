using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using retail.Data;
using retail.Models;

namespace retail.Pages.CustomerView
{
    public class OrderPage : PageModel
    {

        private readonly retail.Data.retailContext _context;

        public OrderPage(retail.Data.retailContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; }
        public int? ProductID { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? productid)
        {
            if (id == null) {
                return NotFound();
            }
            Products = await _context.Product
                .ToListAsync();
            Customer = await _context.Customer
                .Include(c => c.Orders)
                .Include(c => c.Cart.Details)
                .SingleAsync(c => c.CustomerID == id);
            ProductID = productid;
            if (ProductID != null)
            {
                Product = Products.SingleOrDefault(p => p.ProductID == ProductID);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, int productid, int? amount)
        {
            if (amount == null)
            {
                return RedirectToPage(null, new {id = id, productid = productid });
            }
            Products = await _context.Product
                .ToListAsync();
            Customer = await _context.Customer
                .Include(c => c.Orders)
                .Include(c => c.Cart.Details)
                .SingleAsync(c => c.CustomerID == id);
            if (Customer.Cart == null)
            {
                Cart Cart = new Cart();
                Cart.CustomerID = Customer.CustomerID;
                Cart.Customer = Customer;
                Customer.Cart = Cart;
            }
            Product? product = Products.FirstOrDefault(p => p.ProductID == productid);
            bool exists = false;
            foreach(var detail in Customer.Cart.Details)
            {
                if (detail.ProductID == productid) {
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.CartID = Customer.Cart.CartID;
                orderDetail.Cart = Customer.Cart;
                orderDetail.ProductID = productid;
                orderDetail.Quantity = (int)amount;
                Customer.Cart.Details.Add(orderDetail);
                _context.OrderDetail.Add(orderDetail);
            }
            else
            {
                Customer.Cart.Details
                    .Where(d => d.ProductID == productid)
                    .Single()
                    .Quantity += (int)amount;
            }
            if (ProductID != null)
            {
                Product = Products.SingleOrDefault(p => p.ProductID == ProductID);
            }
            await _context.SaveChangesAsync();
            return RedirectToPage(null, new {id = id});
        }

    }
}
