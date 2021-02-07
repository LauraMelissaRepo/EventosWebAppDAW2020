using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventosWebApp.Controllers
{
    public class EventosClienteController : Controller
    {
        // GET: EventosClienteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EventosClienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventosClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventosClienteController/Create
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

        // GET: EventosClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventosClienteController/Edit/5
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

        // GET: EventosClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventosClienteController/Delete/5
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
