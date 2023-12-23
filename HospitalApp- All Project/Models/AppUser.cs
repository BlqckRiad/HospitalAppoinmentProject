using Microsoft.AspNetCore.Identity;

namespace HospitalApp.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }

    }
}