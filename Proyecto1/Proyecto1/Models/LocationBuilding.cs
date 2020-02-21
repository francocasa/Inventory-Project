using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class LocationBuilding
    {
        public LocationBuilding()
        {
            Equipment = new HashSet<Equipment>();
            Phone = new HashSet<Phone>();
            Projector = new HashSet<Projector>();
            Radio = new HashSet<Radio>();
            Screen = new HashSet<Screen>();
        }

        public string Location { get; set; }
        public string Building { get; set; }

        public ICollection<Equipment> Equipment { get; set; }
        public ICollection<Phone> Phone { get; set; }
        public ICollection<Projector> Projector { get; set; }
        public ICollection<Radio> Radio { get; set; }
        public ICollection<Screen> Screen { get; set; }
    }
}
