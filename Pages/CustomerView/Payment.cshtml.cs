using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using retail.Data;
using retail.Migrations;
using retail.Models;

namespace retail.Pages.CustomerView
{
    public class PaymentModel : CustomerNamePageModel
    {
        private readonly retail.Data.retailContext _context;

        public PaymentModel(retail.Data.retailContext context)
        {
            _context = context;
        }

        public Customer Customer { get; set; } = default!;
        public IList<Order>? Orders { get; set; }
        public int? OrderID { get; set; }
        public Order? Order { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? cartid, int? orderid)
        {
            if (id == null)
            {
                return RedirectToPage("./Index");
            }
            var customer = await _context.Customer
                .Include(c => c.Cart)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Details)
                    .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(c => c.CustomerID == id);
            if (customer == null)
            {
                return RedirectToPage("./Index");
            }
            Customer = customer;
            OrderID = orderid;
            if (Filter ==  null)
            {
                Filter = "all";
            }
            Console.WriteLine(Filter);
            Orders = Customer.Orders
                .OrderByDescending(o => o.Date)
                .Where(o => o.PaymentStatus == Filter)
                .ToList();
            if (Filter ==  "all")
            {
                Orders = Customer.Orders.OrderByDescending(o => o.Date).ToList();
            }

            Order = Orders
                .Where(o => o.OrderID == orderid)
                .FirstOrDefault(o => o.OrderID == orderid);
            await _context.SaveChangesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, int? orderid, decimal amount)
        {
            if (id == null) {
                return RedirectToPage("./Index");
            }
            var customer = await _context.Customer
                .Include(c => c.Cart)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Details)
                    .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(c => c.CustomerID == id);
            if (customer == null)
            {
                return RedirectToPage("./Index");
            }
            Customer = customer;
            Orders = Customer.Orders
                .OrderByDescending(o => o.Date)
                .Where(o => o.PaymentStatus == Filter)
                .ToList();
            if (Filter == "all")
            {
                Orders = Customer.Orders.OrderByDescending(o => o.Date).ToList();
            }

            OrderID = orderid;
            Order? order = Orders
                .FirstOrDefault(o => o.OrderID == orderid);
            if (orderid == null || order == null)
            {
                return RedirectToPage("./Payment", new { id = id });
            }
            Order = order;

            Order.Paid += amount;
            if (Order.Paid == Order.getTotal())
            {
                Order.PaymentStatus = "paid";
            }
            else if (amount > 0)
            {
                Order.PaymentStatus = "partial";
            }
            await _context.SaveChangesAsync();
            
            return RedirectToPage(null, new {id = id});
        }
    }
}
