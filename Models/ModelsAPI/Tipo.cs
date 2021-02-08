using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventosWebApp.Models.ModelsAPI
{
    public class Tipo
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tipo de evento")]
        [StringLength(80)]
        public string TipoEvento { get; set; }
    }
}
