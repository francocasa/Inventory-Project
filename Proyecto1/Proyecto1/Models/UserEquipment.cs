using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class UserEquipment
    {
        public string UserName { get; set; }
        public string ServiceTag { get; set; }

        public Equipment ServiceTagNavigation { get; set; }
        public User UserNameNavigation { get; set; }
    }
}
