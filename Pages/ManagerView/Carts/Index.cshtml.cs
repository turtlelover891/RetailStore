using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using retail.Data;
using retail.Models;

namespace retail.Pages.ManagerView.Carts
{
    public class IndexModel : PageModel
    {
        private readonly retail.Data.retailContext _context;

        public IndexModel(retail.Data.retailContext context)
        {
            _context = context;
        }

        public IList<Cart> Carts { get;set; } = default!;
        public Cart? Cart { get; set; }
        public OrderDetail Detail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? detailID)
        {
            Carts = await _context.Cart
                .Include(c => c.Customer)
                .Include(c => c.Details)
                    .ThenInclude(d => d.Product)
                .ToListAsync();
            if (id == null)
            {
                return Page();
            }
            Cart = Carts.FirstOrDefault(c => c.CartID == id);
            if (detailID != null)
            {
                Detail = Cart.Details.SingleOrDefault(d => d.OrderDetailID == detailID);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, int? detailID)
        {
            Carts = await _context.Cart
                .Include(c => c.Customer).ToListAsync();
            if (id == null)
            {
                return Page();
            }
            Cart = Carts.FirstOrDefault(c => c.CartID == id);
            if (detailID != null)
            {
                Detail = Cart.Details.SingleOrDefault(d => d.OrderDetailID == detailID);
            }
            return Page();
        }

    }
}
