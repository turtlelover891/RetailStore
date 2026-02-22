using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using retail.Data;
using retail.Models;

namespace retail.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly retail.Data.retailContext _context;

        public CreateModel(retail.Data.retailContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Cart Cart = new Cart();
            Cart.CustomerID = Customer.CustomerID;
            Cart.Customer = Customer;
            Customer.Cart = Cart;

            _context.Customer.Add(Customer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
