using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BetterOwner.Services.Database
{
    public class User : IdentityUser<int>
    {
        public ICollection<Treasure> Treasures { get; set; }
    }

    public class Role : IdentityRole<int>
    {
    }
}
