using System;
using System.Collections.Generic;

#nullable disable

namespace Entity_Framework.Models
{
    public partial class Regions
    {
        public Regions()
        {
            Territories = new HashSet<Territory>();
        }

        public int RegionId { get; set; }
        public string RegionDescription { get; set; }

        public virtual ICollection<Territory> Territories { get; set; }
    }
}
