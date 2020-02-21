using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class Software
    {
        public Software()
        {
            SoftwareEquipment = new HashSet<SoftwareEquipment>();
            UserSoftware = new HashSet<UserSoftware>();
        }

        public string SoftwareCode { get; set; }
        public string SoftwareName { get; set; }
        public string Licensed { get; set; }
        public DateTime? ContractStart { get; set; }
        public DateTime? ContractFinal { get; set; }

        public ICollection<SoftwareEquipment> SoftwareEquipment { get; set; }
        public ICollection<UserSoftware> UserSoftware { get; set; }
    }
}
