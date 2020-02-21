using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class UserLine
    {
        public string UserName { get; set; }
        public string Number { get; set; }

        public Line NumberNavigation { get; set; }
        public User UserNameNavigation { get; set; }
    }
}
