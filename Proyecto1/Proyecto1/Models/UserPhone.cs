using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class UserPhone
    {
        public string UserName { get; set; }
        public string Mac { get; set; }

        public User Mac1 { get; set; }
        public Phone MacNavigation { get; set; }
    }
}
