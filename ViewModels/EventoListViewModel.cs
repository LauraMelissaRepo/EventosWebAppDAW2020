using EventosWebApp.Models.ModelsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventosWebApp.ViewModels
{
    public class EventoListViewModel
    {
        public IEnumerable<Tipo> TiposEvento { get; set; }

        public IEnumerable<Local> Localidade { get; set; }

        public IEnumerable<Evento> Eventos { get; set; }

        public DateTime Data { get; set; }

    }
}
