using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WcfService.Entities
{
    public partial class Tbooks
    {
        public Tbooks()
        {
            Treservations = new HashSet<Treservations>();
        }

        public int IdBook { get; set; }
        public string VarTitle { get; set; }
        public string VarCode { get; set; }
        public int? IntStatus { get; set; }
        public bool? BitIsAvailable { get; set; }
        public DateTime DtimeCreatedAt { get; set; }
        public DateTime DtimeUpdatedAt { get; set; }
        public bool? BitIsDeleted { get; set; }

        public virtual ICollection<Treservations> Treservations { get; set; }
    }
}
