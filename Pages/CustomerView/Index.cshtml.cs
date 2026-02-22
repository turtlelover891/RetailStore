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

namespace retail.Pages.CustomerView
{
    public class CustomerModel : CustomerNamePageModel
    {
        private readonly retail.Data.retailContext _context;

        public CustomerModel(retail.Data.retailContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int? CustomerID { get; set; } = default;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id != null)
            {
                CustomerID = id;
            }
            if (CustomerID != null)
            {
                PopulateCustomerDropDownList(_context, CustomerID);
            }
            else
            {
                PopulateCustomerDropDownList(_context);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (CustomerID != null)
            {
                PopulateCustomerDropDownList(_context, CustomerID);
            }
            else
            {
                PopulateCustomerDropDownList(_context);
            }
            return Page();
        }
    }
}
