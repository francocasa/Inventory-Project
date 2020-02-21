using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class Line
    {
        public Line()
        {
            LineCellphone = new HashSet<LineCellphone>();
            UserLine = new HashSet<UserLine>();
        }

        public string Number { get; set; }
        public string Supplier { get; set; }
        public string LinePlan { get; set; }
        public double FixedCharge { get; set; }
        public DateTime ContractExpiration { get; set; }
        public string BilledTo { get; set; }

        public ICollection<LineCellphone> LineCellphone { get; set; }
        public ICollection<UserLine> UserLine { get; set; }
    }
}
