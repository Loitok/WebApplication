using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.BLL.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public string Token { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
