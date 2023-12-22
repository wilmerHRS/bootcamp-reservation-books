using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dto.Base
{
    public class ResponseDto<T>
    {
        public T data { get; set; }
        public string message { get; set; }
    }
}