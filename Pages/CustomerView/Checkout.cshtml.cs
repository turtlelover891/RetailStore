using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using retail.Data;
using retail.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace retail.Pages.CustomerView
{
    public class CheckoutModel : PageModel
    {

        private readonly retail.Data.retailContext _context;

        public CheckoutModel(retail.Data.retailContext context)
        {
            _context = context;
        }
        public Customer Customer { get; set; }
        public decimal Total = 0;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("./Index");
            }
            var customer = await _context.Customer
                .Include(c => c.Orders)
                .Include(c => c.Cart.Details)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null) 
            {
                return RedirectToPage("./Index");
            }
            Customer = customer;
            foreach (var detail in Customer.Cart.Details)
            {
                Total += detail.getPrice();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("./Index");
            }
            var customer = await _context.Customer
                .Include(c => c.Orders)
                .Include(c => c.Cart.Details)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return RedirectToPage("./Index");
            }
            Customer = customer;

            Order order = Customer.Cart.convertToOrder();
            order.PaymentStatus = "unpaid";
            order.Date = DateTime.Now;
            Customer.Orders.Add(order);
            foreach (var detail in Customer.Cart.Details)
            {
                _context.OrderDetail.Remove(detail);
            }
            Customer.Cart.Details.Clear();
            
            foreach(var detail in order.Details)
            {
                detail.Product.Quantity -= detail.Quantity;
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./Payment", new {id = Customer.CustomerID, orderid = order.OrderID});
        }
    }
}
