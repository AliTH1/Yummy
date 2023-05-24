using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Yummy.Models.Auth
{
    public class MyAppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}