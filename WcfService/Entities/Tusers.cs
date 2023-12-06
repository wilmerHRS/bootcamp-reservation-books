using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WcfService.Entities
{
    public partial class Tusers
    {
        public Tusers()
        {
            Treservations = new HashSet<Treservations>();
        }

        public int IdUser { get; set; }
        public string VarFirstName { get; set; }
        public string VarLastName { get; set; }
        public string VarEmail { get; set; }
        public string VarPassword { get; set; }
        public int? IntStatus { get; set; }
        public DateTime DtimeCreatedAt { get; set; }
        public DateTime DtimeUpdatedAt { get; set; }
        public bool? BitIsDeleted { get; set; }

        public virtual ICollection<Treservations> Treservations { get; set; }
    }
}
