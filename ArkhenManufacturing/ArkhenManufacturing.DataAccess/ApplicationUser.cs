using System;

using Microsoft.AspNetCore.Identity;

namespace ArkhenManufacturing.DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        public Guid UserId { get; set; }
    }
}
