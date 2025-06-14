using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using e_commerce_web.model.Models;

namespace e_commerce_web.model.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email{ get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public RoleDto Role { get; set; }
    }
}
