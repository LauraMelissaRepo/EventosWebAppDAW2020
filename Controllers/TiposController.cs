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
        public async Task<IActionResult> Index()
        {
            
            IEnumerable<Tipo> tipos = null;

            HttpClient client = _api.Initial();
            var res = client.GetAsync("api/Tipos");
            res.Wait();

            var resultDisplay = res.Result;
            if(resultDisplay.IsSuccessStatusCode)
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

            return View(tipos);
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
