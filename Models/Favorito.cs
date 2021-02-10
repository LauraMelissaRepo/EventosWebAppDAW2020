using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace EventosWebApp.Models
{
    public partial class Favorito
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FavoritosId { get; set; }
        public string EventosId { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}
