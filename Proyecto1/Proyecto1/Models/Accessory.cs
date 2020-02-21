using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class Accessory
    {
        public Accessory()
        {
            UserAccessory = new HashSet<UserAccessory>();
        }

        public string Identification { get; set; }
        public string Equipment { get; set; }
        public string Comment { get; set; }

        public ICollection<UserAccessory> UserAccessory { get; set; }
    }
}
