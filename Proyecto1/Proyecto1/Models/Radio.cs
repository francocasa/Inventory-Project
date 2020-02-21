using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class Radio
    {
        public Radio()
        {
            UserRadio = new HashSet<UserRadio>();
        }

        public string ServiceTag { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public DateTime LatestUpdate { get; set; }
        public string Area { get; set; }
        public string Location { get; set; }
        public string Comment { get; set; }

        public LocationBuilding LocationNavigation { get; set; }
        public ICollection<UserRadio> UserRadio { get; set; }
    }
}
