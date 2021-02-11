using EventosWebApp.Helper;
using EventosWebApp.Models;
using EventosWebApp.Models.ModelsAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventosWebApp.Controllers
{
    public class EventosController : Controller
    {

        private EventosWebAppContext db = new EventosWebAppContext();

        EventosWebAPI _api = new EventosWebAPI();

        // GET: EventosController
        public async Task<IActionResult> Index()
        {
            List<Evento> eventos = new List<Evento>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Eventos");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                eventos = JsonConvert.DeserializeObject<List<Evento>>(result);
            }

            return View(eventos);
        }

        [HttpPost]
        public ActionResult Filter(DateTime data_evento, int tipoEvento, int localidade)
        {
            throw new Exception("Evento: " + tipoEvento + ", data:" + data_evento + ", localidade:" + localidade);
            return View();
        }

        // GET: EventosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventosController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evento evento)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Eventos", evento);
            response.EnsureSuccessStatusCode();


            return RedirectToAction("Index");
        }

        // GET: EventosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            HttpClient client = _api.Initial();
            Evento evento = null;
            HttpResponseMessage res = await client.GetAsync($"api/Eventos/{id}");

            if (res.IsSuccessStatusCode)
            {
                evento = await res.Content.ReadFromJsonAsync<Evento>();
                return View(evento);
            }
            res.EnsureSuccessStatusCode();

            return View(evento);
        }

        // POST: EventosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Evento evento)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/Eventos/{id}", evento);

            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        // GET: EventosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = _api.Initial();
            Evento evento = null;
            HttpResponseMessage res = await client.GetAsync($"api/Eventos/{id}");

            if (res.IsSuccessStatusCode)
            {
                evento = await res.Content.ReadFromJsonAsync<Evento>();
            }
            return View(evento);
        }

        // POST: EventosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.DeleteAsync($"api/Eventos/{id}");
            return RedirectToAction("Index");
        }

        public ActionResult Filter()
        {
            #region get Tipos

            IEnumerable<Tipo> tipos = null;

            HttpClient client = _api.Initial();
            var res = client.GetAsync("api/Tipos");
            res.Wait();

            var resultDisplay = res.Result;
            if (resultDisplay.IsSuccessStatusCode)
            {
                var readData = resultDisplay.Content.ReadFromJsonAsync<List<Tipo>>();
                readData.Wait();
                tipos = readData.Result;
            }
            else
            {
                tipos = Enumerable.Empty<Tipo>();
                ModelState.AddModelError(string.Empty, "Nenhum tipo encontrado");
            }

            ViewBag.Tipos = new SelectList(tipos, "Id", "TipoEvento");
            #endregion

            #region get Localidade

            IEnumerable<Local> locais = null;

            res = client.GetAsync("api/Locais");
            res.Wait();

            resultDisplay = res.Result;
            if (resultDisplay.IsSuccessStatusCode)
            {
                var readData = resultDisplay.Content.ReadFromJsonAsync<List<Local>>();
                readData.Wait();
                locais = readData.Result;
            }
            else
            {
                locais = Enumerable.Empty<Local>();
                ModelState.AddModelError(string.Empty, "Nenhum tipo encontrado");
            }

            ViewBag.Localidades = new SelectList(locais, "Id", "Localidade");

            #endregion
            return View();
        }

        public async Task<IActionResult> AdicionadaAoFav()
        {
            return View();
        }


        public ActionResult addToFavorite(int id)
        {
            Favorito favorito = new Favorito();
            var idEvento = favorito.EventosId;
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //se não houver nenhum evento com o mesmo id, ele adiciona
            if (db.Favoritos.Where(x => x.EventosId == id.ToString()).Where(y => y.UserId == userId.ToString()).Count() == 0)
            {
                favorito.EventosId = id.ToString();
                favorito.UserId = userId;

                db.Favoritos.Add(favorito);
                db.SaveChanges();

                return RedirectToAction("AdicionadaAoFav");

            }
            return View();
        }

        //public async Task<IActionResult> returnEventosFavoritos()
        //{
        //    List<Evento> eventos = new List<Evento>();
        //    HttpClient client = _api.Initial();

        //    Favorito fav = new Favorito();
        //    var idevento = fav.EventosId;

        //    HttpResponseMessage res = await client.GetAsync($"api/Eventos/{idevento}");

        //    if (res.IsSuccessStatusCode)
        //    {
        //        var result = res.Content.ReadAsStringAsync().Result;
        //        eventos = JsonConvert.DeserializeObject<List<Evento>>(result);
        //    }

        //    return View(eventos);
        //}
    }
}
