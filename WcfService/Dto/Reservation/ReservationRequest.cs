using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Entities;

namespace WcfService.Dto.Reservation
{
    public class ReservationRequest
    {
        public int IdUser { get; set; }
        public int IdBook { get; set; }
        public DateTime? DtimeDateReservation { get; set; }
    }
}