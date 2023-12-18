using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WcfService.Entities
{
    public partial class Treservations
    {
        public int IdResevation { get; set; }
        public int IdUser { get; set; }
        public int IdBook { get; set; }
        public string VarUserName { get; set; }
        public string VarBookName { get; set; }
        public DateTime? DtimeDateReservation { get; set; }
        public int? IntStatus { get; set; }
        public DateTime DtimeCreatedAt { get; set; }
        public DateTime DtimeUpdatedAt { get; set; }
        public bool? BitIsDeleted { get; set; }

        public virtual Tbooks IdBookNavigation { get; set; }
        public virtual Tusers IdUserNavigation { get; set; }
    }
}
