using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class Cellphone
    {
        public Cellphone()
        {
            LineCellphone = new HashSet<LineCellphone>();
            UserCellphone = new HashSet<UserCellphone>();
        }

        public string Imei { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Status { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModelAirwatch { get; set; }
        public string Observation { get; set; }
        public string ExUser { get; set; }

        public ICollection<LineCellphone> LineCellphone { get; set; }
        public ICollection<UserCellphone> UserCellphone { get; set; }
    }
}
