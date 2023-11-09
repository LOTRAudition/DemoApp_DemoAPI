using Microsoft.AspNetCore.Identity;

namespace DemoApp_DemoAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
