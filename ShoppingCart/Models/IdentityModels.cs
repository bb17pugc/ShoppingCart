using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public byte[] UserPhoto { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "date of birth is required")]
        [DataType(DataType.Date)]
        [Checkdate(ErrorMessage = "Invalid date of birth")]
        public string DateOfBirth { get; set; }
        [Required(ErrorMessage = "City is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Invalid city name")]
        [DataType(DataType.Text)]
        public string City { get; set; }
        [Required(ErrorMessage = "Country name is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Invalid country name")]
        [DataType(DataType.Text)]
        public string Country { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [RegularExpression("^[a-z A-Z]+$", ErrorMessage = "Invalid address")]
        [DataType(DataType.Text)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression("[0-9]{11}", ErrorMessage = "Not a valid phone number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ShoppingCart.Models.Product> Products { get; set; }
        public System.Data.Entity.DbSet<ShoppingCart.Models.SalesModel> Sales { get; set; }
        public System.Data.Entity.DbSet<ShoppingCart.Models.OrdersData> Orders { get; set; }

       }
}