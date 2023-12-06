using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dto.User
{
    public class CredentialRequestDto
    {
        public string VarEmail { get; set; }
        public string VarPassword { get; set; }
    }
}