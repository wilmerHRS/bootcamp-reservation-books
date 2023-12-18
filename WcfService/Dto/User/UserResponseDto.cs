using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dto.User
{
    public class UserResponseDto
    {
        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}