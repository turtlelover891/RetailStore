using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using retail.Data;
using retail.Models;

namespace retail.Pages.ManagerView.Carts
{
    public class DetailsModel : PageModel
    {
        private readonly retail.Data.retailContext _context;

        public DetailsModel(retail.Data.retailContext context)
        {
            _context = context;
        }

        public Cart Cart { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FirstOrDefaultAsync(m => m.CartID == id);
            if (cart == null)
            {
                return NotFound();
            }
            else
            {
                Cart = cart;
            }
            return Page();
        }
    }
}
