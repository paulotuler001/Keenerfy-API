using Microsoft.AspNetCore.Identity;

namespace Keenerfy.Models;
    public class User : IdentityUser
    {
        public string Cpf { get; set; }
    }
