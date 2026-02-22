using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using retail.Data;
using retail.Models;

namespace retail.Pages.Customers
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly retail.Data.retailContext _context;

        public IndexModel(retail.Data.retailContext context)
        {
            _context = context;
        }

        public IList<Customer> Customers { get;set; } = default!;
        public Customer? Customer { get; set; }

        public bool ShowCart = false;

        public bool ShowOrders = false;

        public async Task<IActionResult> OnGetAsync(int? id, bool? showCart, bool? showOrders)
        {
            Customers = await _context.Customer
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Details)
                    .ThenInclude(d => d.Product)
                .Include(c => c.Cart)
                    .ThenInclude(c => c.Details)
                    .ThenInclude(d => d.Product)
                .ToListAsync();

            if (id != null)
            {
                Customer = Customers
                    .Where(i => i.CustomerID == id)
                    .Single();
            }
            if (showCart == true && showOrders == true)
            {
                return Page();
            }
            else if (showCart == true)
            {
                ShowCart = (bool)showCart;
            }
            else if (showOrders == true)
            {
                ShowOrders = (bool)showOrders;
            }
            return Page();
        }
    }
}
