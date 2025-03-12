using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.IUserRole
{
    public class IUserRoleDto
    {
        public int NumberuserId { get; set; }    
        public string UserId { get; set; }    
        public string RoleId { get; set; }    
        public string? RoleName { get; set; }    
        public string? UserName { get; set; }    
        public string? PersianRoleName { get; set; }    
    }
}
