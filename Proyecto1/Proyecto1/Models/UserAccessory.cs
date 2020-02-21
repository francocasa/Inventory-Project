using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class UserAccessory
    {
        public string UserName { get; set; }
        public string Identification { get; set; }

        public Accessory IdentificationNavigation { get; set; }
        public User UserNameNavigation { get; set; }
    }
}
