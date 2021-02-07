using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventosWebApp.Controllers
{
    
    public class EventosAdminController : Controller
    {
        // GET: EventosAdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EventosAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        
        // GET: EventosAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventosAdminController/Create
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

        // GET: EventosAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventosAdminController/Edit/5
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

        // GET: EventosAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventosAdminController/Delete/5
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
