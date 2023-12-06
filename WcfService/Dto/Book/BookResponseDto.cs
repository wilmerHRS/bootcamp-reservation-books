using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Dto.Book
{
    public class BookResponseDto
    {
        public int IdBook { get; set; }
        public string VarTitle { get; set; }
        public string VarCode { get; set; }
        public int? IntStatus { get; set; }
        public bool IsReserved { get; set; }
        public DateTime? DtimeDateReservation { get; set; }
        public DateTime DtimeCreatedAt { get; set; }
    }
}