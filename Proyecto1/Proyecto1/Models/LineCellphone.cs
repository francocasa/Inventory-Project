using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class LineCellphone
    {
        public string Number { get; set; }
        public string Imei { get; set; }

        public Cellphone ImeiNavigation { get; set; }
        public Line NumberNavigation { get; set; }
    }
}
