using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dto.Book
{
    public class BookResponseDtoEF
    {
        public int IdBook { get; set; }
        public string VarTitle { get; set; }
        public string VarCode { get; set; }
        public int? IntStatus { get; set; }
        public DateTime DtimeCreatedAt { get; set; }
    }
}