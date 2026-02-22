using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace retail.Models
{
    public class Customer
    {
        public int CustomerID{ get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [RegularExpression(@"[a-zA-Z0-9.]+@[a-zA-Z0-9.]+$")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression(@"[0-9]{3}-?[0-9]{3}-?[0-9]{4}$")]
        public string PhoneNumber { get; set; }

        public IList<Order> Orders { get; set; }

        public Cart Cart { get; set; }

        public string FullName()
        {
            return FirstName + " " + LastName;
        }

    }
}
