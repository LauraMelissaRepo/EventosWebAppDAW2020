using EventosWebApp.Helper;
using EventosWebApp.Models.ModelsAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace EventosWebApp.Controllers
{
    public class TipoController : Controller
    {
        EventosWebAPI _api = new EventosWebAPI();

        // GET: TipoController
        public async Task<List<Tipo>> Index()
        {
            System.Diagnostics.Debug.WriteLine("CHEGAME AOS TIPOS IDNEX");
            List<Tipo> tipos = new List<Tipo>();

            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Tipos");
            if(res.IsSuccessStatusCode)
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

        // GET: TipoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TipoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TipoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
