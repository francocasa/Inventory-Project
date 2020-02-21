using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class SoftwareEquipment
    {
        public string SoftwareCode { get; set; }
        public string ServiceTag { get; set; }

        public Equipment ServiceTagNavigation { get; set; }
        public Software SoftwareCodeNavigation { get; set; }
    }
}
