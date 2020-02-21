using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class Screen
    {
        public Screen()
        {
            UserScreen = new HashSet<UserScreen>();
        }

        public string ServiceTag { get; set; }
        public string Model { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PartOfLeasing { get; set; }
        public string Location { get; set; }

        public LocationBuilding LocationNavigation { get; set; }
        public ICollection<UserScreen> UserScreen { get; set; }
    }
}
