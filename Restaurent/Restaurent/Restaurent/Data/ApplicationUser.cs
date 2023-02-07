using Microsoft.AspNetCore.Identity;

namespace Restaurent.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
    }
}
