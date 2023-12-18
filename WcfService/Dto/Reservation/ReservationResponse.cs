using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Entities;

namespace WcfService.Dto.Reservation
{
    public class ReservationResponse
    {
        public int IdResevation { get; set; }
        public int IdUser { get; set; }
        public int IdBook { get; set; }
        public string UserName { get; set; }
        public string BookName { get; set; }
        public DateTime? DateReservation { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}