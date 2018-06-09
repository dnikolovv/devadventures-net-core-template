using System;
using Microsoft.AspNetCore.Identity;

namespace MyProject.Data.Entities
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
