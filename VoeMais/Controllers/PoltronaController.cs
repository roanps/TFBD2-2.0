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
    public class PoltronaController : Controller
    {
        private readonly VoeMaisContext _context;

        public PoltronaController(VoeMaisContext context)
        {
            _context = context;
        }

        // GET: Poltrona
        public async Task<IActionResult> Index()
        {
            var voeMaisContext = _context.Poltronas.Include(p => p.Aviao);
            return View(await voeMaisContext.ToListAsync());
        }

        // GET: Poltrona/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poltrona = await _context.Poltronas
                .Include(p => p.Aviao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poltrona == null)
            {
                return NotFound();
            }

            return View(poltrona);
        }

        // GET: Poltrona/Create
        public IActionResult Create()
        {
            ViewData["AviaoId"] = new SelectList(_context.Avioes, "Id", "Id");
            return View();
        }

        // POST: Poltrona/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,AviaoId")] Poltrona poltrona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poltrona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AviaoId"] = new SelectList(_context.Avioes, "Id", "Id", poltrona.AviaoId);
            return View(poltrona);
        }

        // GET: Poltrona/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poltrona = await _context.Poltronas.FindAsync(id);
            if (poltrona == null)
            {
                return NotFound();
            }
            ViewData["AviaoId"] = new SelectList(_context.Avioes, "Id", "Id", poltrona.AviaoId);
            return View(poltrona);
        }

        // POST: Poltrona/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,AviaoId")] Poltrona poltrona)
        {
            if (id != poltrona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poltrona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoltronaExists(poltrona.Id))
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
            ViewData["AviaoId"] = new SelectList(_context.Avioes, "Id", "Id", poltrona.AviaoId);
            return View(poltrona);
        }

        // GET: Poltrona/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poltrona = await _context.Poltronas
                .Include(p => p.Aviao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poltrona == null)
            {
                return NotFound();
            }

            return View(poltrona);
        }

        // POST: Poltrona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poltrona = await _context.Poltronas.FindAsync(id);
            if (poltrona != null)
            {
                _context.Poltronas.Remove(poltrona);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoltronaExists(int id)
        {
            return _context.Poltronas.Any(e => e.Id == id);
        }
    }
}
