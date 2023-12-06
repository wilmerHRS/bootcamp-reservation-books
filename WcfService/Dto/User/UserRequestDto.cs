using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dto.User
{
    public class UserRequestDto
    {
        public string VarFirstName { get; set; }
        public string VarLastName { get; set; }
        public string VarEmail { get; set; }
        public string VarPassword { get; set; }
    }
}