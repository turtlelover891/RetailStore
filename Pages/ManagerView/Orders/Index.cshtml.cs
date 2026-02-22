using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using retail.Data;
using retail.Models;

namespace retail.Pages.ManagerView.Orders
{
    public class IndexModel : PageModel
    {
        private readonly retail.Data.retailContext _context;

        public IndexModel(retail.Data.retailContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Filter {  get; set; }
        public IList<Order> Orders { get;set; } = default!;

        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (Filter  == null)
            {
                Filter = "all";
            }
            Orders = await _context.Order
                .Where(o => o.PaymentStatus == Filter)
                .Include(o => o.Customer)
                .Include(o => o.Details)
                    .ThenInclude(d => d.Product)
                .ToListAsync();
            if (Filter == "all")
            {
                Orders = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.Details)
                    .ThenInclude(d => d.Product)
                .ToListAsync();
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                Orders = Orders.Where(o => o.Customer.FullName().ToLower()
                .Contains(SearchString.ToLower()))
                .ToList();
            }

            if (id != null)
            {
                Order = Orders.SingleOrDefault(o => o.OrderID == id);
            }

            return Page();

        }
    }
}
