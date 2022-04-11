using Microsoft.AspNetCore.Identity;

namespace BasePoject.Models
{
    public class AppUser: IdentityUser
    {
        public string PictureUrl { get; set; }

        public string Description { get; set; }
    }
}
