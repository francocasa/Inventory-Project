using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class User
    {
        public User()
        {
            UserAccessory = new HashSet<UserAccessory>();
            UserCellphone = new HashSet<UserCellphone>();
            UserEquipment = new HashSet<UserEquipment>();
            UserLine = new HashSet<UserLine>();
            UserPhone = new HashSet<UserPhone>();
            UserRadio = new HashSet<UserRadio>();
            UserScreen = new HashSet<UserScreen>();
            UserSoftware = new HashSet<UserSoftware>();
        }

        public string UserName { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Mail { get; set; }
        public string Deparment { get; set; }
        public string Position { get; set; }
        public string Supervisor { get; set; }
        public string Location { get; set; }
        public string Annex { get; set; }

        public Incidence Incidence { get; set; }
        public ICollection<UserAccessory> UserAccessory { get; set; }
        public ICollection<UserCellphone> UserCellphone { get; set; }
        public ICollection<UserEquipment> UserEquipment { get; set; }
        public ICollection<UserLine> UserLine { get; set; }
        public ICollection<UserPhone> UserPhone { get; set; }
        public ICollection<UserRadio> UserRadio { get; set; }
        public ICollection<UserScreen> UserScreen { get; set; }
        public ICollection<UserSoftware> UserSoftware { get; set; }
    }
}
