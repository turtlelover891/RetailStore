using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using retail.Models;

namespace retail.Pages.ManagerView
{
    [Authorize]
    public class OrderProductModel : PageModel
    {
        private readonly retail.Data.retailContext _context;

        public OrderProductModel(retail.Data.retailContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int? productid { get; set; }
        public Product? Product { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _context.Product.ToListAsync();
            if (productid != null)
            {
                Product = Products.SingleOrDefault(p => p.ProductID == productid);
            }
        }

        public async Task<IActionResult> OnPostAsync(int amount)
        {
            Products = await _context.Product.ToListAsync();
            if (productid != null)
            {
                Product = Products.SingleOrDefault(p => p.ProductID == productid);
            }
            Product.Quantity += amount;
            await _context.SaveChangesAsync();
            return RedirectToPage(null);
        }

    }
}
