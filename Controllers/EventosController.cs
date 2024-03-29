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
using Microsoft.AspNetCore.Authorization;


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

            return PartialView(eventos);
        }


        // GET: EventosController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventosController/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Evento evento)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Eventos", evento);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                 ViewData["evento_criado"] = "O evento foi criado com sucesso!";
            }

            return View(evento);
        }

        // GET: EventosController/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Evento evento)
        {
            HttpClient client = _api.Initial();
            System.Diagnostics.Debug.WriteLine("Nome : " + evento.Titulo + "data: " + evento.Data + "estado: " + evento.Estado + "tipo Id" + evento.TipoId + "tipo" + evento.Tipo + "LocalId " + evento.LocalId + " local " + evento.Local.NomeLocal );

            HttpResponseMessage response = await client.PutAsJsonAsync($"api/Eventos/{id}", evento);

            response.EnsureSuccessStatusCode();

            ViewData["evento_editado"] = "O evento foi editado com sucesso!";
            return View(evento);
        }

        // GET: EventosController/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.DeleteAsync($"api/Eventos/{id}");

            List<Favorito> listfavDelete =  db.Favoritos.Where(x => x.EventosId == id.ToString()).ToList();

            foreach(Favorito idfav in listfavDelete)
            {
                db.Favoritos.Remove(idfav);
                await db.SaveChangesAsync();
            }

            return View("ApagadoSucesso");
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

       
        public async Task<IActionResult> SearchFilters(DateTime data_evento, int tipoEvento, int localidade)
        {
            
            String ano = data_evento.Year.ToString();      
            String mes_evento = data_evento.Month.ToString();
            List<Evento> eventosFilter = new List<Evento>();
            HttpClient client = _api.Initial();

            if (ano == "1")
            {
                HttpResponseMessage res = await client.GetAsync($"api/Eventos/tipo/{tipoEvento}/local/{localidade}");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    eventosFilter = JsonConvert.DeserializeObject<List<Evento>>(result);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(res);
                }
            }
            else
            {
                HttpResponseMessage res = await client.GetAsync($"api/Eventos/tipo/{tipoEvento}/local/{localidade}/data/{mes_evento}");

                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    eventosFilter = JsonConvert.DeserializeObject<List<Evento>>(result);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(res);
                }

            }
            return PartialView(eventosFilter);
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


        public async Task<IActionResult> DeleteFavorito(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var idfav = db.Favoritos.Where(y => y.EventosId == id.ToString()).Where(x => x.UserId == userId).ToList();

            
            db.Favoritos.Remove(idfav[0]);
            await db.SaveChangesAsync();

            return RedirectToAction("returnEventosFavoritos");
        }
        

        public async Task<IActionResult> returnEventosFavoritos()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Favorito> favoritosUserId = db.Favoritos.Where(x => x.UserId == userId).ToList();      

            Evento evento = null;
            List<Evento> eventosUser = new List<Evento>();

            for (int i = 0 ; i < favoritosUserId.Count() ; i ++){

                HttpClient client = _api.Initial();
                var id = favoritosUserId[i].EventosId;

                HttpResponseMessage res = await client.GetAsync($"api/Eventos/{id}");

                var result = res.Content.ReadAsStringAsync().Result;

                if (res.IsSuccessStatusCode)
                {
                    evento = await res.Content.ReadFromJsonAsync<Evento>();
                    eventosUser.Add(evento);
                }
            } 
            return View(eventosUser);
        }


        public async Task<List<Tipo>> GetTypesofEvents()
        {
            
            List<Tipo> tipos = new List<Tipo>();

            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Tipos");
            if (res.IsSuccessStatusCode)
            {
                var readData = await res.Content.ReadFromJsonAsync<List<Tipo>>();
                tipos = readData;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nenhum tipo encontrado");
            }

            return tipos;
        }
    }
}
