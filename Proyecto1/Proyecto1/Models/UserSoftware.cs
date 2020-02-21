using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class UserSoftware
    {
        public string UserName { get; set; }
        public string SoftwareCode { get; set; }

        public Software SoftwareCodeNavigation { get; set; }
        public User UserNameNavigation { get; set; }
    }
}
