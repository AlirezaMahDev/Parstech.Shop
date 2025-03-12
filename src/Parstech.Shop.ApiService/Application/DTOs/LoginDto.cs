using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.Auth
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class ChangePasswordDto
    {
        public string old { get; set; }
        public string newPassword { get; set; }
        public string renewPassword { get; set; }
    }
}
