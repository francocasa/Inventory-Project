using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class Equipment
    {
        public Equipment()
        {
            SoftwareEquipment = new HashSet<SoftwareEquipment>();
            UserEquipment = new HashSet<UserEquipment>();
        }

        public string ServiceTag { get; set; }
        public string ComputerName { get; set; }
        public string PartOfLeasing { get; set; }
        public string NextRenewal { get; set; }
        public string Model { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Condition { get; set; }
        public string Location { get; set; }
        public string MainFunctionDevice { get; set; }
        public DateTime ValidatedOn { get; set; }
        public string AssignedBy { get; set; }
        public string ExUser { get; set; }
        public string Storage { get; set; }

        public LocationBuilding LocationNavigation { get; set; }
        public ICollection<SoftwareEquipment> SoftwareEquipment { get; set; }
        public ICollection<UserEquipment> UserEquipment { get; set; }
    }
}
