using System;
using System.Collections.Generic;

namespace Proyecto1.Models
{
    public partial class Incidence
    {
        public string UserName { get; set; }
        public string IncidenceType { get; set; }
        public string Comment { get; set; }

        public User UserNameNavigation { get; set; }
    }
}
