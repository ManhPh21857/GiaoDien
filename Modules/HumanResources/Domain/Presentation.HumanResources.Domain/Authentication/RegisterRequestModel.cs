using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.HumanResources.Domain.Authentication
{
    public class RegisterRequestModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? rePassword { get; set; }
    }
}
