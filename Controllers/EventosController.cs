using EventosWebApp.Helper;
using EventosWebApp.Models.ModelsAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EventosWebApp.Controllers
{
    public class EventosController : Controller
    {
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
        public async Task<Uri> Create(Evento evento)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.PostAsJsonAsync( "api/Eventos", evento);
            response.EnsureSuccessStatusCode();

        
            return response.Headers.Location;
        }

        // GET: EventosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Evento> Edit(int id, Evento evento)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/Eventos/{id}", evento);
            response.EnsureSuccessStatusCode();

            evento = await response.Content.ReadFromJsonAsync<Evento>();

            return evento;
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
        public async Task<HttpStatusCode> Delete(int id, IFormCollection collection)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.DeleteAsync($"api/Eventos/{id}");
            return response.StatusCode;
        }

        public ActionResult Filter()
        {
            return View();
        }
    }
}
