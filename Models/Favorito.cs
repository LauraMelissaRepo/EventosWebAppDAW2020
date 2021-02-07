﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EventosWebApp.Models
{
    public partial class Favorito
    {
        public int FavoritosId { get; set; }
        public string EventosId { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}
