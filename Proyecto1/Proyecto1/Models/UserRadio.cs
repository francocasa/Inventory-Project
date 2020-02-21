using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class UserRadio
    {
        public string UserName { get; set; }
        public string ServiceTag { get; set; }

        public Radio ServiceTagNavigation { get; set; }
        public User UserNameNavigation { get; set; }
    }
}
