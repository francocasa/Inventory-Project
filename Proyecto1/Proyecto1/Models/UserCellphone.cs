using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class UserCellphone
    {
        public string UserName { get; set; }
        public string Imei { get; set; }

        public Cellphone ImeiNavigation { get; set; }
        public User UserNameNavigation { get; set; }
    }
}
