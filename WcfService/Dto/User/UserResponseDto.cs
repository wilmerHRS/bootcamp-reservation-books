using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dto.User
{
    public class UserResponseDto
    {
        public int IdUser { get; set; }
        public string VarFirstName { get; set; }
        public string VarLastName { get; set; }
        public string VarEmail { get; set; }
        public int? IntStatus { get; set; }
        public DateTime DtimeCreatedAt { get; set; }
    }
}