using retail.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace retail.Pages.CustomerView;

public class CustomerNamePageModel : PageModel
{
    public SelectList CustomerNameSL {  get; set; }

    public void PopulateCustomerDropDownList(retail.Data.retailContext _context,
        object selectCustomer = null)
    {
        var categoryQuery = from c in _context.Customer
                            orderby c.FirstName
                            select new
                            {
                                CustomerID = c.CustomerID,
                                FullName = c.FirstName + " " + c.LastName
                            };

        if (selectCustomer != null)
        {
            CustomerNameSL = new SelectList(categoryQuery.AsNoTracking(),
                "CustomerID", "FullName", selectCustomer);
        }
        else
        {
            CustomerNameSL = new SelectList(categoryQuery.AsNoTracking(),
                "CustomerID", "FullName");
        }
    }
}
