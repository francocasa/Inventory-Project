using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class Phone
    {
        public Phone()
        {
            UserPhone = new HashSet<UserPhone>();
        }

        public string Mac { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string Location { get; set; }

        public LocationBuilding LocationNavigation { get; set; }
        public ICollection<UserPhone> UserPhone { get; set; }
    }
}
