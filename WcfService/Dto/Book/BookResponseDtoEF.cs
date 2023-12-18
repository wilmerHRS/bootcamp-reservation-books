using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dto.Book
{
    public class BookResponseDtoEF
    {
        public int IdBook { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int? Status { get; set; }
        public bool? IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}