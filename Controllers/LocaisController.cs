using EventosWebApp.Helper;
using EventosWebApp.Models.ModelsAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EventosWebApp.Controllers
{
    public class LocaisController : Controller
    {
        EventosWebAPI _api = new EventosWebAPI();

        // GET: LocaisController
        public async Task<IActionResult> Index()
        {
            IEnumerable<Local> locais = null;

            HttpClient client = _api.Initial();
            var res = client.GetAsync("api/Locais");
            res.Wait();

            var resultDisplay = res.Result;
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

            return View(locais);
        }

        // GET: LocaisController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LocaisController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocaisController/Create
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

        // GET: LocaisController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LocaisController/Edit/5
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

        // GET: LocaisController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LocaisController/Delete/5
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
