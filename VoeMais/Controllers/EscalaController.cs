using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;

namespace VoeMais.Controllers
{
    public class EscalaController : Controller
    {
        private readonly VoeMaisContext _context;

        public EscalaController(VoeMaisContext context)
        {
            _context = context;
        }

        // GET: Escala
        public async Task<IActionResult> Index()
        {
            var voeMaisContext = _context.Escalas.Include(e => e.Aeroporto).Include(e => e.Voo);
            return View(await voeMaisContext.ToListAsync());
        }

        // GET: Escala/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escala = await _context.Escalas
                .Include(e => e.Aeroporto)
                .Include(e => e.Voo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (escala == null)
            {
                return NotFound();
            }

            return View(escala);
        }

        // GET: Escala/Create
        public IActionResult Create()
        {
            ViewData["AeroportoId"] = new SelectList(_context.Aeroportos, "Id", "Id");
            ViewData["VooId"] = new SelectList(_context.Voos, "Id", "Id");
            return View();
        }

        // POST: Escala/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VooId,AeroportoId,Chegada,Partida")] Escala escala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(escala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AeroportoId"] = new SelectList(_context.Aeroportos, "Id", "Id", escala.AeroportoId);
            ViewData["VooId"] = new SelectList(_context.Voos, "Id", "Id", escala.VooId);
            return View(escala);
        }

        // GET: Escala/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escala = await _context.Escalas.FindAsync(id);
            if (escala == null)
            {
                return NotFound();
            }
            ViewData["AeroportoId"] = new SelectList(_context.Aeroportos, "Id", "Id", escala.AeroportoId);
            ViewData["VooId"] = new SelectList(_context.Voos, "Id", "Id", escala.VooId);
            return View(escala);
        }

        // POST: Escala/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VooId,AeroportoId,Chegada,Partida")] Escala escala)
        {
            if (id != escala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscalaExists(escala.Id))
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
            ViewData["AeroportoId"] = new SelectList(_context.Aeroportos, "Id", "Id", escala.AeroportoId);
            ViewData["VooId"] = new SelectList(_context.Voos, "Id", "Id", escala.VooId);
            return View(escala);
        }

        // GET: Escala/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var escala = await _context.Escalas
                .Include(e => e.Aeroporto)
                .Include(e => e.Voo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (escala == null)
            {
                return NotFound();
            }

            return View(escala);
        }

        // POST: Escala/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var escala = await _context.Escalas.FindAsync(id);
            if (escala != null)
            {
                _context.Escalas.Remove(escala);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EscalaExists(int id)
        {
            return _context.Escalas.Any(e => e.Id == id);
        }
    }
}
