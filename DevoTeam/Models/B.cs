﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DevoTeam.Models
{
    public partial class B
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdA { get; set; }

        public virtual A IdANavigation { get; set; }
    }
}
