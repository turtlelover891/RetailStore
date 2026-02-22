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

namespace retail.Pages.CustomerView
{
    public class EditModel : CustomerNamePageModel
    {
        private readonly retail.Data.retailContext _context;

        public EditModel(retail.Data.retailContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default;
        public OrderDetail? Detail { get; set; } = default;
        public int? ProductID { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, int? productID)
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
            var detail = Customer.Cart.Details
                .Where(c => c.ProductID == productID)
                .FirstOrDefault();
            ProductID = productID;
            if (detail == null)
            {
                return Page();
            }
            Detail = detail;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id, int? productID, string action, int? amount)
        {
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
            ProductID = productID;
            if (productID == null)
            {
                return Page();
            }
            var detail = Customer.Cart.Details
                .Where(c => c.ProductID == productID)
                .FirstOrDefault();
            if (detail == null)
            {
                return Page();
            }
            Detail = detail;
            if (amount == null)
            {
                ErrorMessage = "Please enter a valid number";
                return Page();
            }
            if (amount > Detail.Product.Quantity)
            {
                ErrorMessage = "Cannot order more that stock has";
                return Page();
            }
            if (action == "change")
            {
                detail.Quantity = (int)amount;
                await _context.SaveChangesAsync();
                return Page();
            }
            else if (action == "delete")
            {
                Customer.Cart.Details.Remove(detail);
                await _context.SaveChangesAsync();
                return RedirectToPage(null, new { id = id });
            }
            return Page();
        }
    }
}
