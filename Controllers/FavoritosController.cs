using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventosWebApp.Models;
using EventosWebApp.Models.ModelsAPI;
using System.Dynamic;

namespace EventosWebApp.Controllers
{
    public class FavoritosController : Controller
    {
        private readonly EventosWebAppContext _context;

        public FavoritosController(EventosWebAppContext context)
        {
            _context = context;
        }

        //GET: Favoritos
        public async Task<IActionResult> Index()
        {
            var fav = _context.Favoritos.Include(f => f.User);

            return View(await fav.ToListAsync());
        }


        // GET: Favoritos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favorito = await _context.Favoritos
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FavoritosId == id);
            if (favorito == null)
            {
                return NotFound();
            }

            return View(favorito);
        }

        // GET: Favoritos/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Favoritos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FavoritosId,EventosId,UserId")] Favorito favorito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favorito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", favorito.UserId);
            return View(favorito);
        }

        // GET: Favoritos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favorito = await _context.Favoritos.FindAsync(id);
            if (favorito == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", favorito.UserId);
            return View(favorito);
        }

        // POST: Favoritos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FavoritosId,EventosId,UserId")] Favorito favorito)
        {
            if (id != favorito.FavoritosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favorito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoritoExists(favorito.FavoritosId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", favorito.UserId);
            return View(favorito);
        }

        // GET: Favoritos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favorito = await _context.Favoritos
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FavoritosId == id);
            if (favorito == null)
            {
                return NotFound();
            }

            return View(favorito);
        }

        // POST: Favoritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favorito = await _context.Favoritos.FindAsync(id);
            _context.Favoritos.Remove(favorito);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool FavoritoExists(int id)
        {
            return _context.Favoritos.Any(e => e.FavoritosId == id);
        }
    }
}
